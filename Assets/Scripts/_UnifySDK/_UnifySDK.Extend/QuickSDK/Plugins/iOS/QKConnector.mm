
//


#import "QKConnector.h"
#import <UIKit/UIKit.h>

#define TargetObject "U3d_Object"       /*自行设置需要通知的U3D对象*/
#define TargetMethod "objectMethod"     /*通知对象的方法*/

#ifndef TargetObject
#error 需要设置发送对象
#endif
#ifndef TargetMethod
#error 需要设置处理方法
#endif

#define Send_Message(methodName,msg)     UnitySendMessage(TargetObject, methodName, msg) /*向U3D发送消息*/

#if defined(__cplusplus)
extern "C" {
    extern NSString* CreateNSString (const char* string);
    extern void QKAlertView(const char* title,const char* message);
    extern void UnitySendMessage(const char *, const char *, const char *);
#endif
    
#pragma mark - SDK 平台接口调用

    void QKSetListener(const char *gameObjectName) {
        
        [[QKConnector shareInstance] setListener:[NSString stringWithUTF8String:gameObjectName]];
        
    }
        
    void QKInit(){
        
        //设置代理，监听用户退出事件
        [JySDKManager defaultManager].acountDelegate = [QKConnector shareInstance];
        
        
        // 初始化
        [JySDKManager initWithProductCode:@"59269277661049085595683944505461" completion:^(Status_CODE retCode) {///productCode到quickgame后台获取,参数自行设置
            if (retCode == kInitSuccess) {
                // 初始化成功
                //QKAlertView("初始化", "初始化成功");
                //Send_Message("OnInitSuccess","Init:Init Success");
                
                if ([QKConnector shareInstance].bU3dInited)
                {
                    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:@"Init Success", @"msg", nil];
                    [[QKConnector shareInstance] sendU3dMessage:@"onInitSuccess" :dic];
                }
                else
                {
                    [QKConnector shareInstance].initState = 1;
                }
                
            } else {
                NSLog(@"初始化失败，错误码：%d",retCode);
                if ([QKConnector shareInstance].bU3dInited)
                {
                    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:@"init failed", @"msg", nil];
                    [[QKConnector shareInstance] sendU3dMessage:@"onInitFailed" :dic];
                }
                else
                {
                    [QKConnector shareInstance].initState = 0;
                }
            }
        }];
    }
    
    //显示登陆界面
    void QKLogin(){
        [JySDKManager loginWithSuccBlock:^(NSDictionary *resultDic) {
            NSString *code = [resultDic objectForKey:@"code"];
            NSString *token = [JySDKManager userToken];
            switch (code.integerValue) {
                case kErrorNone:{
                    
                    NSString *userid = [resultDic objectForKey:@"userId"];
                    NSLog(@"登录成功:\n用户ID:%@,验证码:%@",userid,token);

                    //NSString *msg = [NSString stringWithFormat:@"%@:%@:%@", userid,token,[JySDKManager userAccount]];
                    //Send_Message("OnLoginSuccess",msg.UTF8String);
                    
                    
                    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:userid,@"userId", token, @"userToken", [JySDKManager userAccount], @"userName", @"", @"msg", nil];
                    [[QKConnector shareInstance] sendU3dMessage:@"onLoginSuccess" :dic];
                    
                    if([JySDKManager isGuest]){
                        NSLog(@"是游客登录");
                    }else{
                        NSLog(@"非游客登录");
                    }
                    // // 登录成功， 上传角色信息
                    // GameRole *role = [GameRole new];
                    // role.roleId = @"testRoleid";  /// 必传
                    // role.role_name = @"testRoleName";
                    // role.serverId = @"1";
                    // role.sv_name = @"testServer";
                    // role.role_level = @"testRoleLevel";
                    // role.vipLevel = @"testVipLevel";
                    // [JySDKManager updateRoleInfo:role];
                    // NSString * age = JySDKManager.getCurrentUserAge;
                    // NSLog(@"当前用户UID=%@，年龄=%@", JySDKManager.userId, age);
                }
                    break;
                    
                default:
                    break;
            }
        } failBlock:^(NSString *message) {
            NSLog(@"登录失败：%@",message);
            NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:message,@"msg", nil];
            [[QKConnector shareInstance] sendU3dMessage:@"onLoginFailed" :dic];
        }];
    }

    //静默登录
    void QKLoginAsGuest(){
        [JySDKManager loginAsGuestWithFloatMenuShow:YES successBlock:^(NSDictionary *resultDic) {
            NSLog(@"静默登录成功，返回用户信息：%@",resultDic);
            // 状态码 1000代表登录成功；各状态码含义请查看JySDKManager.h
            NSString *code = [resultDic objectForKey:@"code"];
            // 用户唯一id
            NSString *userId = [resultDic objectForKey:@"userId"];
            //userToken用户校验码
            NSString *token = [JySDKManager userToken];
            // 用户名
            NSString *account = [resultDic objectForKey:@"account"];
            // 登录方式
            NSString *loginType = [resultDic objectForKey:@"loginType"];
            // 登录成功附带消息
            NSString *message = [resultDic objectForKey:@"message"];
            
            //Send_Message("OnGuestLoginSuccess",[message UTF8String]);//向Unity发送静默登录成功通知
            
            NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:userId,@"userId", token, @"userToken", account, @"userName", loginType, @"loginType", @"", @"msg", nil];
            [[QKConnector shareInstance] sendU3dMessage:@"onLoginSuccess" :dic];
        } failBlock:^(NSString *message) {
            NSLog(@"静默登录失败，原因：%@",message);
            //Send_Message("OnGuestLoginFail",[message UTF8String]);//向Unity发送静默登录失败通知
            NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:message,@"msg", nil];
            [[QKConnector shareInstance] sendU3dMessage:@"onLoginFailed" :dic];
        }];
    }
    
    //注销登陆
    void QKLogout(){
        [JySDKManager logout:^{
            [[QKConnector shareInstance] sendU3dMessage:@"onLogoutSuccess" :nil];
        }];
        
    }
    
    //显示个人中心
    void QKCenter(){
        [JySDKManager showUserCenter];
    }
    
    //获取唯一UserID
    void QKUserID(){
        NSString *userId = [JySDKManager userId];
        NSString *msg    = [NSString stringWithFormat:@"UserID:%@",userId];
        
        //Send_Message("OnUserID",msg.UTF8String);
    }
    
    //获取UserToken
    void QKUserToken(){
        NSString *userToken = [JySDKManager userToken];
        NSString *msg    = [NSString stringWithFormat:@"UserToken:%@",userToken];
        
        //Send_Message("OnUserToken",msg.UTF8String);
    }
    
    //获取用户名
    void QKUserName(){
        NSString *userName = [JySDKManager userAccount];
        NSString *msg      = [NSString stringWithFormat:@"UserName:%@",userName];

        //Send_Message("OnUserName",msg.UTF8String);
    }

    void QKCongzi(const char *productId,const char *productName,float amount,const char *orderNo,const char *callBackUrl,const char *extrasParams){
        
 
        GoodParam *param = [GoodParam new];
        
        param.productId = CreateNSString(productId);    ///设置商品ID，苹果后台对应的productID
        param.productName = CreateNSString(productName);    ///商品名称 必填
        param.price = amount;                       ///商品总价 必填
        param.orderNo = CreateNSString(orderNo);       ///游戏方订单号 string[64] 接入QuickAd时必填、必须唯一
        param.url = CreateNSString(callBackUrl);       ///回调通知地址 string[200] 可选  客户端配置优先;  可传nil
        param.extras = CreateNSString(extrasParams);     ///透传参数  可选
    
        [JySDKManager getGoodWithParam:param completion:^(NSDictionary *resultDic) {
            NSLog(@"%@",resultDic);
            NSString *code = [resultDic objectForKey:@"code"];
            NSString *msg = [resultDic objectForKey:@"message"];
            NSString *productId; //商品id
            NSData *receipt;   //购买后的凭据
            NSString *tranactionId; //交易号
            switch (code.integerValue) {
                case KOrderSuccess:
                {
                    NSLog(@"购买成功，订单号:%@",msg);
                    //Send_Message("OnPaySuccess","pay Success");
                    
                    
                    //获取内购相关信息
                    productId = [resultDic objectForKey:@"productIdentifier"]; //内购商品id
                    receipt = [resultDic objectForKey:@"receipt"]; //内购交易凭据
                    tranactionId = [resultDic objectForKey:@"transactionId"]; //内购交易id
                    
                    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:productId,@"productIdentifier",code,@"code",msg,@"message",productId,@"productId",tranactionId,@"tranactionId", nil];
                    [[QKConnector shareInstance] sendU3dMessage:@"onPaySuccess" :dic];
                    
                }
                    break;
                case KOrderFail:
                {
                    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:productId,@"productIdentifier",code,@"code",msg,@"message",productId,@"productId", tranactionId,@"tranactionId",nil];
                    [[QKConnector shareInstance] sendU3dMessage:@"onPayFailed" :dic];
                }
                    break;
                case KOrderCancel:
                {
                    NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:productId,@"productIdentifier",code,@"code",msg,@"message",productId,@"productId",tranactionId,@"tranactionId", nil];
                    [[QKConnector shareInstance] sendU3dMessage:@"onPayCancel" :dic];
                    NSLog(@"失败原因：%@",msg);
                }
                    
                    break;
                case KOrderUnkown:
                    break;
                default:
                    break;
            }
        }];
    }
    
    void QKUpdateRole(const char *roleId,const char *role_name,const char *server_Id,const char *sv_name,const char *role_level,const char *vipLevel,const char *role_power){
        
        // 登录成功， 上传角色信息
        GameRole *role = [GameRole new];
        role.roleId = CreateNSString(roleId);  /// 必传
        role.role_name = CreateNSString(role_name);
        role.serverId = CreateNSString(server_Id);
        role.sv_name = CreateNSString(sv_name);
        role.role_level = CreateNSString(role_level);
        role.vipLevel = CreateNSString(vipLevel);
        role.role_power = CreateNSString(role_power);
        [JySDKManager updateRoleInfo:role];
    }

    //恢复购买：Restoring purchased non-consumable or auto-subscription items
    void QKRestoreNonConsumptionProducts(){
        [JySDKManager restoreNonConsumptionProducts:^(NSDictionary *resultDic) {
            NSLog(@"The callback dictionary contains the product id corresponding to the purchased non-consumables : %@",resultDic);
            
            //NSString *productidJsonStr = [[QKConnector shareInstance] jsonStrFromDictionary:resultDic];
            //Send_Message("getUserProductIds",productidJsonStr.UTF8String);
            
            [[QKConnector shareInstance] sendU3dMessage:@"onRestoreSuccess" :resultDic];
        }];
    }
    
    void QKAlertView(const char* title,const char* message)
    {
        UIAlertView *alertView = [[UIAlertView alloc] initWithTitle:[NSString stringWithFormat:@"\n%@\n",CreateNSString(title)] message:[NSString stringWithFormat:@"\n%@\n",CreateNSString(message)] delegate:nil cancelButtonTitle:@"确定" otherButtonTitles:nil, nil];
        [alertView show];
    }
    
    NSString* CreateNSString (const char* string)
    {
        if (string)
            return [NSString stringWithUTF8String: string];
        else
            return [NSString stringWithUTF8String: ""];
    }
#if defined(__cplusplus)
}
#endif

@interface QKConnector ()

@end

@implementation QKConnector

+(QKConnector *)shareInstance{
    
    static QKConnector *shareInstance = nil;
    static dispatch_once_t predicate;
    
    dispatch_once(&predicate, ^{
        shareInstance = [[QKConnector alloc] init];
    });
    return shareInstance;
    
}

- (instancetype)init {
    self = [super init];
    if (self) {
        self.initState = -1;
        self.bU3dInited = NO;
    }
    return self;
}

- (void)setListener:(NSString *)gameObjectName
{
    _gameObjectName = gameObjectName;
#if !__has_feature(objc_arc)
    _gameObjectName = [gameObjectName retain];
#endif
    [QKConnector shareInstance].bU3dInited = YES;
    //在此时发消息回去才能保证u3d加载好了，可以接收初始化消息了
    if ([QKConnector shareInstance].initState == -1)//未初始化
    {
        ;
    }
    else if ([QKConnector shareInstance].initState == 0)//初始化失败
    {
        NSDictionary *dic = [NSDictionary dictionaryWithObjectsAndKeys:@"init failed", @"msg", nil];
        [[QKConnector shareInstance] sendU3dMessage:@"onInitFailed" :dic];
        [QKConnector shareInstance].initState = -1;
    }
    else if ([QKConnector shareInstance].initState == 1)//初始化成功
    {
        [[QKConnector shareInstance] sendU3dMessage:@"onInitSuccess" :nil];
        [QKConnector shareInstance].initState = -1;
    }
    
}

-(void)sendU3dMessage:(NSString *)messageName :(NSDictionary *)dict
{
    if (dict != nil)
    {
        NSString *jsonString = [self jsonStrFromDictionary:dict];
        UnitySendMessage([_gameObjectName UTF8String], [messageName UTF8String], [jsonString UTF8String]);
    }
    else{
        UnitySendMessage([_gameObjectName UTF8String], [messageName UTF8String], "");
    }
}

- (NSString *)jsonStrFromDictionary:(NSDictionary *)dic {
    NSError *error = nil;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:0 error:&error];
    if ([error code]) {
        return @"";
    }
    return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}

#pragma mark -- KAcountDelegate
- (void)userLogout:(NSDictionary *)resultDic
{
    NSLog(@"用户从个人中心手动登出。\n%@",resultDic);
    //Send_Message("OnLogOut","Log out");
    [[QKConnector shareInstance] sendU3dMessage:@"onLogoutSuccess" :nil];
    //[JySDKManager login:nil];
}

@end
