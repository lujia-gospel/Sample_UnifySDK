//
//

#import "UltraSDK_ios.h"
#import "UltraSDKInterfaceUnity.h"


#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
#if defined(__cplusplus)
}
#endif

@implementation UltraSDK_ios

static UltraSDK_ios * __singleton__;
+ (UltraSDK_ios *)shareInstance {
    static dispatch_once_t predicate;
    dispatch_once( &predicate, ^{ __singleton__ = [[[self class] alloc] init]; } );
    return __singleton__;
}

- (instancetype)init {
    self = [super init];
    if (self) {
        [self addNotifications];
        _gameObjectName = @"UltraiOSPlatform" ;
    }
    return self;
}

-(void)addNotifications
{
    static BOOL isAdded = NO;
    if (isAdded) { return ; }
    isAdded = YES;
    //添加初始化通知
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(initNotification:) name:GAME_PUBLIC_NOTIFICATION_NAME_INIT object:nil];
    //监听用户登陆成功的通知 单机版游戏不用监听
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(loginNotification:) name:GAME_PUBLIC_NOTIFICATION_NAME_LOGIN object:nil];
    //监听点击切换账号
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(changeAccountNotification:) name:GAME_PUBLIC_NOTIFICATION_NAME_CHANGE_ACCOUNT  object:nil];
    //内购通知
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(iapNotification:) name:GAME_PUBLIC_NOTIFICATION_NAME_IAPPURCHASE_FINISH object:nil];
    //踢下线
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(loginValidNotification:) name:GAME_PUBLIC_NOTIFICATION_NAME_LOGONINVALID object:nil];
    //同意协议通知
    [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(clickProtocol) name:GAME_PUBLIC_NOTIFICATION_NAME_PROTOCOL object:nil];
}
//初始化通知
-(void)initNotification:(NSNotification *)notification
{
    NSMutableDictionary *paramter = [[NSMutableDictionary alloc] init];
    [self sendU3dMessage:@"onInitSuccess" params:paramter];
}
//登录通知
-(void)loginNotification:(NSNotification *)notification
{
    BOOL isLoginSuccess = [notification.object[GAME_PUBLIC_TAG_LOGIN_IS_SUCCESS] boolValue];
    if (isLoginSuccess) {
        NSString * accessCode = notification.object[GAME_PUBLIC_TAG_ACCESS_CODE];
        NSString * accessToken = notification.object[GAME_PUBLIC_TAG_ULTRA_ACCESS_TOKEN];
        NSString * sdkuserid = [[UltraPlatform sharedInstance] getUserId] ;
        NSString * username = [[UltraPlatform sharedInstance] getUserName] ;
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        if (accessCode != nil ) {
            params[@"accessCode"] = accessCode ;
        }
        if (accessToken != nil ) {
            params[@"accessToken"] = accessToken ;
        }
        if (sdkuserid != nil ) {
            params[@"sdkuserid"] = sdkuserid ;
        }
        if (username != nil ) {
            params[@"username"] = username ;
        }
        [self sendU3dMessage:@"onLoginSuccess" params:params];
    } else {
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        params[@"msg"] = @"用户登陆失败!" ;
        [self sendU3dMessage:@"onLoginFailed" params:params];
    }
}
//切换账号通知
-(void)changeAccountNotification:(NSNotification *)notification
{
    NSMutableDictionary *params = [[NSMutableDictionary alloc] init] ;
    params[@"msg"] = @"切换账号成功!" ;
    [self sendU3dMessage:@"onSwitchAccountSuccess" params:params];
}
//内购通知
-(void)iapNotification:(NSNotification *)notification
{
    BOOL isIapPurchaseSucess = [notification.object[GAME_PUBLIC_TAG_IAPPURCHASE_IS_SUCCESS] boolValue];
    if (isIapPurchaseSucess) {
        UltraPaymentOrder * paymentOrder = notification.object[GAME_PUBLIC_TAG_PAYMENT_ORDER];
        NSMutableDictionary  *params = [[NSMutableDictionary alloc] init];
        if (paymentOrder.orderId != nil) {
            params[@"orderId"] = paymentOrder.orderId ;
        }
        if (paymentOrder.orderAmount != nil) {
            params[@"orderAmount"] = paymentOrder.orderAmount ;
        }
        if (paymentOrder.currency != nil) {
            params[@"currency"] = paymentOrder.currency ;
        }
        [self sendU3dMessage:@"onPaySuccess" params:params];
    } else {
        UltraPaymentOrder * paymentOrder = notification.object[GAME_PUBLIC_TAG_PAYMENT_ORDER];
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        params[@"code"] = [NSString stringWithFormat:@"%ld",(long)paymentOrder.errorCode] ;
        if (paymentOrder.errorDescription != nil) {
            params[@"msg"] = paymentOrder.errorDescription ;
        }
        [self sendU3dMessage:@"onPayFailed" params:params];
    }
}
//登录失效、踢下线通知
-(void)loginValidNotification:(NSNotification *)notification
{
    NSString *reason = notification.userInfo[GAME_PUBLIC_CP_KICKOFF_REASON] ;
    if (reason != nil) {
        //被踢下线
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        params[@"msg"] = reason ;
        
        [self sendU3dMessage:@"onLogonInvalid" params:params];
        
    } else {
        //登录失效
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        params[@"msg"] = @"登录失效,请尝试重新登录" ;
        
        [self sendU3dMessage:@"onReLoginFromInvalid" params:params];
    }
}
//这里处理同意协议后的逻辑
-(void)clickProtocol
{
    NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
    [self sendU3dMessage:@"onClickProtocol" params:params];
}
- (NSString *)jsonStrFromDictionary:(NSDictionary *)dic {
    NSError *error = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:0 error:&error];
    if ([error code]) {
        return @"";
    }
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

-(void)sendU3dMessage:(NSString *)messageName params:(NSDictionary *)dict
{
    if (dict != nil)
    {
        NSString *jsonString = [self jsonStrFromDictionary:dict];
        UnitySendMessage([_gameObjectName UTF8String], [messageName UTF8String], [jsonString UTF8String]);
    } else {
        UnitySendMessage([_gameObjectName UTF8String], [messageName UTF8String], "");
    }
}

- (void)initWithUltraSDKKey:(NSString *)productKey
              productID:(NSString *)productID
{
    UltraProject *uProject = [[UltraProject alloc] init];
    //原UltraProductId
    uProject.ultraProductId = productID;
    //原UltraProductKey
    uProject.ultraProductKey = productKey ;
    [[UltraPlatform sharedInstance] initWithProject:uProject];
}
//登录
- (void)enterLoginView
{
    [[UltraPlatform sharedInstance] enterLoginView] ;
}
//退出登录
- (void)logout
{
    [[UltraPlatform sharedInstance] logout] ;
    NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
    params[@"msg"] = @"退出登录成功";
    [self sendU3dMessage:@"onLogoutSuccess" params:params];
}
//退出登录并显示账号历史界面
- (void)logoutAndSowLoginView
{
    [[UltraPlatform sharedInstance] logoutAndSowLoginView] ;
    NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
    params[@"msg"] = @"退出登录成功";
    [self sendU3dMessage:@"onLogoutSuccess" params:params];
}
//用户中心
- (void)showUserCenter
{
    [[UltraPlatform sharedInstance] showUserCenter] ;
}
//清除本地账号
- (void)cleanUserEntities
{
    [[UltraPlatform sharedInstance] cleanUserEntities] ;
}
//用户名
- (NSString *)getUserName
{
    return [[UltraPlatform sharedInstance] getUserName] ;
}
//用户ID
- (NSString *)getUserId
{
    return [[UltraPlatform sharedInstance] getUserId] ;
}
//SDKID
- (NSString *)getSdkId
{
    return [[UltraPlatform sharedInstance] getSdkId] ;
}
//获取设备号
- (NSString *)getDeviceNum
{
    return [[UltraPlatform sharedInstance] getDeviceNum] ;
}
//内购
- (void)iapPurchaseWithData:(GamePaymentParameters *)parameters
{
    [[UltraPlatform sharedInstance] iapPurchaseWithData:parameters] ;
}
//设置基础参数
- (void)setBaseRoleInfoWithData:(UltraHDCBaseGameRoleInfo *)baseInfo
{
    [[UltraPlatform sharedInstance] setBaseRoleInfoWithData:baseInfo] ;
}
//角色登录
- (void)roleLoginWithGameRoleInfo:(UltraHDCGameRoleInfo *)roleInfo
{
    [[UltraPlatform sharedInstance] roleLoginWithGameRoleInfo:roleInfo] ;
}
//角色注册
- (void)roleRegisterWithGameRoleInfo:(UltraHDCGameRoleInfo *)roleInfo
{
    [[UltraPlatform sharedInstance] roleRegisterWithGameRoleInfo:roleInfo] ;
}
//角色升级
- (void)roleLevelUpWithGameRoleInfo:(UltraHDCGameRoleInfo *)roleInfo
{
    [[UltraPlatform sharedInstance] roleLevelUpWithGameRoleInfo:roleInfo] ;
}
//分享
- (void)share:(BOOL)hasUi title:(NSString *)title content:(NSString *)content imagePath:(NSString *)imagePath url:(NSString *)url shareTo:(NSString *)shareTo
{
    UltraSharePlatform sharePlatForm = Ultra_Share_Platform_All ;
    if ([shareTo intValue] == 1) {
        sharePlatForm = Ultra_Share_Platform_QQ ;
    } else if ([shareTo intValue] == 2) {
        sharePlatForm = Ultra_Share_Platform_QQ_Space ;
    } else if ([shareTo intValue] == 3) {
        sharePlatForm =  Ultra_Share_Platform_WeChat;
    } else if ([shareTo intValue] == 4) {
        sharePlatForm =  Ultra_Share_Platform_WXTimeLine ;
    } else if ([shareTo intValue] == 5) {
        sharePlatForm =  Ultra_Share_Platform_Weibo ;
    }
    if (hasUi) {
        UltraShareModel *shareModel = [[UltraShareModel alloc] init];
        shareModel.sharePlatform = Ultra_Share_Platform_All ;
        if (url.length > 0) {
            shareModel.shareType = UltraShareType_link ;
            shareModel.shareLink = url ;
            shareModel.shareLinkTitle = title ;
            shareModel.shareLinkDescription = content ;
        } else {
            shareModel.shareType = UltraShareType_image ;
            shareModel.shareImage = [NSData dataWithContentsOfURL:[NSURL fileURLWithPath:imagePath]] ;
        }
        [[UltraPlatform sharedInstance] ultraShareWithShareModel:shareModel success:^(NSString *successMessage, UltraShareStatus status, UltraShareTaget target) {
            [self sendU3dMessage:@"onShareSuccessdAction" params:@{@"shareType":@(target)}];
        } failure:^(NSString *failureMessage, UltraShareStatus status, UltraShareTaget target) {
            [self sendU3dMessage:@"onShareFailedAction" params:@{@"shareType":@(target),@"msg":failureMessage}];
        }];
    } else {
        UltraShareModel *shareModel = [[UltraShareModel alloc] init];
        shareModel.sharePlatform = sharePlatForm ;
        if (url.length > 0) {
            shareModel.shareType = UltraShareType_link ;
            shareModel.shareLink = url ;
            shareModel.shareLinkTitle = title ;
            shareModel.shareLinkDescription = content ;
        } else {
            shareModel.shareType = UltraShareType_image ;
            shareModel.shareImage = [NSData dataWithContentsOfURL:[NSURL fileURLWithPath:imagePath]] ;
        }
        [[UltraPlatform sharedInstance] ultraShareWithShareModel:shareModel success:^(NSString *successMessage, UltraShareStatus status, UltraShareTaget target) {
            [self sendU3dMessage:@"onShareSuccessdAction" params:@{@"shareType":@(target)}];
        } failure:^(NSString *failureMessage, UltraShareStatus status, UltraShareTaget target) {
            [self sendU3dMessage:@"onShareFailedAction" params:@{@"shareType":@(target),@"msg":failureMessage}];
        }] ;
    }
}
//上报闪屏
- (void)postSplashScreenEndSuccess
{
    [[UltraPlatform sharedInstance] postSplashScreenEndSuccess:^(id obj) {
        NSLog(@"--------------> iOS闪屏上报成功 <-------------") ;
    } faild:^(id obj) {
        NSLog(@"--------------> iOS闪屏上报失败 <------------") ;
    }] ;
}
//打开内置浏览器
- (void)openBrowser:(NSString *)openID
{
    [[UltraPlatform sharedInstance] ultraOpenBrowserByWebId:openID success:^(NSDictionary * _Nonnull dic) {
           NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
           params[@"msg"] = @"通过webId获取信息成功" ;
           [self sendU3dMessage:@"onOpenBrowserSucccess" params:params];
     } failure:^(NSString * _Nonnull error) {
           NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
           params[@"msg"] = error ;
           [self sendU3dMessage:@"onOpenBrowserFailed" params:params];
     }];
}
//打开用户中心
- (void)showAccountCenter
{
    [[UltraPlatform sharedInstance] showUserCenter];
}
//注销账号
- (void)accountCancellation
{
    [[UltraPlatform sharedInstance] accountCancellation];
}
//撤回注销 账号
- (void)accountUnCancellation
{
    [[UltraPlatform sharedInstance] accountUnCancellationWithSuccess:^(id  _Nonnull obj) {
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        params[@"msg"] = @"Success" ;
        [self sendU3dMessage:@"onAccountUnCancellationSuccess" params:params];
    } failure:^(id  _Nonnull obj) {
        NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
        params[@"msg"] = obj ;
        [self sendU3dMessage:@"onAccountUnCancellationFail" params:params];
    }];
}

//切换账号
- (void)switchAccount
{
    [[UltraPlatform sharedInstance] logoutAndSowLoginView];
}
//收到踢下线结果回调SDK
- (void)cpKickOffCallBackWithResult:(NSString *)result
{
    NSMutableDictionary *params = [[NSMutableDictionary alloc] init];
    if (result.length > 0) {
        params[GAME_PUBLIC_CP_KICKOFF_RESULT] = result ;
    }
    [[UltraPlatform sharedInstance] cpKickOffCallBackWithResult:params];
}

//获取协议内容
- (NSString *)getProtocolResult
{
    NSDictionary* resultDic = [[UltraPlatform sharedInstance] getProtocolResult];
    if (resultDic.allValues.count > 0) {
        NSData *josnData = [NSJSONSerialization dataWithJSONObject:resultDic options:NSJSONWritingPrettyPrinted error:nil] ;
        if (josnData != nil) {
            NSString *jsonString = [[NSString alloc] initWithData:josnData encoding:NSUTF8StringEncoding] ;
            return jsonString ;
        }
    }
    return nil ;
}
//点击同意通知
- (void)setAgreeProtocol
{
    [[UltraPlatform sharedInstance] setAgreeProtocol] ;
}

@end
