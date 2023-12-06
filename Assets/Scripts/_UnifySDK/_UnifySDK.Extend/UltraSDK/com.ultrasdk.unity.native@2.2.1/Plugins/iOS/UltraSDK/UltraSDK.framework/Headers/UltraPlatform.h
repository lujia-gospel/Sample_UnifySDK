//
//  UltraPlatform.h
//  UltraSDK
//
//  Created by 魏太山 on 2020/10/26.
//  Copyright © 2020年 Ultra. All rights reserved.

#import <UltraSDK/UltraProject.h>
#import <UltraSDK/UltraPay.h>
#import <UltraSDK/UltraData.h>
#import <UltraSDK/UltraBloc.h>
#import <UltraSDK/UltraShare.h>
#import <UltraSDK/UltraProject.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

//usdk信息类型
typedef NS_ENUM(NSInteger, UltraInfoType){
    /**用户id*/
    UltraInfoTypeUserId,
    /**用户名*/
    UltraInfoTypeUserName,
    /**SDKId*/
    UltraInfoTypeSDKId,
    /**渠道Id*/
    UltraInfoTypeChannelId,
    /**渠道名称*/
    UltraInfoTypeChannelName,
    /**设备号*/
    UltraInfoTypeDeviceNum,

};

// 广告类型
typedef NS_ENUM(NSInteger, UltraAdType){
    /**未知广告*/
    UltraAdTypeUnKnow = -1,
    /**admob广告*/
    UltraAdTypeAdmob = 1,
};

@interface UltraPlatform : NSObject

#pragma mark - 初始化方法
/*
 *  获取单例实例对象
 */
+ (UltraPlatform *)sharedInstance;

/**
 Ultra初始化方法
 先初始化UltraProject对象
 e.g.
 UltraProject *project = [[UltraProject alloc] init];
 project.productId = @"UltraSDK服务器后台申请的产品id";
 project.productKey = @"UltraSDK服务器后台申请的产品key";
 [[UltraPlatform sharedInstance] initWithProject:project];
 @param project 项目参数对象
 */
- (void)initWithProject:(UltraProject *)project;

#pragma mark - =================== 登录相关API ===================

/**
 @brief  调用SDK默认的登陆界面
 @note   如果开发者想自定义用户登陆界面，请调用后序用户相关的接口方法
 */
- (void)enterLoginView;

/**
 @brief  注销、退出登陆
 */
- (void)logout;

/**
 @brief  注销、退出登陆
 */
- (void)logoutAndSowLoginView;

#pragma mark - =================== 支付相关API ===================
/**
 @brief 调用SDKIAP内购
 @param paymentParametersData iap支付参数配置
 @result 0表示方法调用成功
 */
- (int)iapPurchaseWithData:(GamePaymentParameters *)paymentParametersData;

/**兑换礼品卡或代码*/
- (void)redeemGiftCardOrCodeWithData:(GamePaymentParameters *)data
                             success:(void (^)(NSDictionary *dic))success
                             failure:(void (^)(NSString *error))failure;

/*
 *  主动查询小票，进行补单，有界面。全球渠道生效
 */
- (void)queryTicket;
#pragma mark - =================== 数据上报API ===================

/** 设置基础数据 */
- (void)setBaseRoleInfoWithData:(UltraHDCBaseGameRoleInfo *)data;

/**  角色登录  */
- (void)roleLoginWithGameRoleInfo:(UltraHDCGameRoleInfo *)gameRoleInfo;

/**  角色注册  */
- (void)roleRegisterWithGameRoleInfo:(UltraHDCGameRoleInfo *)gameRoleInfo;

/**  角色升级  */
- (void)roleLevelUpWithGameRoleInfo:(UltraHDCGameRoleInfo *)gameRoleInfo;

/** 上报闪屏结束  */
- (void)postSplashScreenEndSuccess:(void (^)(id obj))success
                             faild:(void (^)(id obj))faild;

#pragma mark - =================== 其他API ===================

/**
 @brief 获取IDFA
 1.获取IDFA需要添加AppTrackingTransparency.framework系统库，并且设置为Optional
 2.在Info.plist添加NSUserTrackingUsageDescription权限描述
 不添加，可能导致iOS 14以上无法获取IDFA
 * @param success 成功回调，成功后会返回idfa
 * @param failure 失败回调，失败后会返回具体失败原因
 */
- (void)getIDFAWithSuccess:(void (^)(NSString* idfa))success
                   failure:(void (^)(NSString* error))failure;

/**
 @brief 获取IDFV
 */
- (NSString *)getIDFV;

/**根据类型获取用户信息*/
- (NSString *)getInfoWithType:(UltraInfoType)type;

/*
 *  打开用户中心
 */
- (void)showUserCenter;

/*
 *  清空用户存储
 */
- (void)cleanUserEntities;

/**
 @brief 获取登录用户名
 @result 若用户没登录，则返回nil
 */
- (NSString *)getUserName;

/**
 @brief 获取登录用户ID
 @result 若用户没登录，则返回nil
 */
- (NSString *)getUserId;

/**
 @brief 获取ID（部分游戏使用@"id"字段作为唯一标示符）
 @result 若用户没登录，则返回nil
 */
- (NSString *)getSdkId;

/**
 @brief 获取渠道ID，必须在初始化后调用
 */
- (NSString *)getChannelId;

/**
 @brief 获取渠道名称，必须在初始化后调用
 */
- (NSString *)getChannelName;

/**
 @brief 获取渠道SDK的版本名，必须在初始化后调用
 */
- (NSString *)getChannelSdkVersionName;

/**
 @brief 获取渠道的ProjectId，必须在初始化后调用
 */
- (NSString *)getProjectId;

/**
 *  当前是否已经登录
 *
 *  @return YES 已经登录
 *          NO 没有登录
 */
- (BOOL)isLogined;

/*
 * 获取设备号(优先取的IDFA、没取到则取的UUID)
 */
- (NSString *)getDeviceNum;

/**
下线回传
@param result 结果
*/
- (void)cpKickOffCallBackWithResult:(NSDictionary *)result ;

/*
 * 获取当前登录用户的信息接口
 */
- (void)requestUserInfoSuccess:(void (^)(id obj))success
                             faild:(void (^)(id obj))faild ;


/**获取首次弹窗协议内容
{
       childAgrName = "个人信息保护政策";
       childAgrUrl = "";
       priAgrName = "个人信息保护政策";
       priAgrUrl = "";
       userAgrName = "用户协议";
       userAgrUrl = "";
       version = 1;
};
 */
- (NSDictionary *)getProtocolResult;

/**
 cp自绘制协议弹窗界面，点击同意设置接口
 */
- (void)setAgreeProtocol;

/**
 * @abstract
 * 调用 track 接口，追踪一个带有属性的 event
 *
 * @discussion
 * propertyDict 是一个 Map。
 * 其中的 key 是 Property 的名称，必须是 NSString
 * value 则是 Property 的内容，只支持 NSString、NSNumber、NSSet、NSArray、NSDate 这些类型
 * 特别的，NSSet 或者 NSArray 类型的 value 中目前只支持其中的元素是 NSString
 *
 * @param event             event的名称
 * @param propertyDict     event的属性
 */
- (void)track:(NSString *)event withProperties:(nullable NSDictionary *)propertyDict;


/**
 获取全埋点数据
 */
- (void)trackEventDataCallback:(void (^)(NSDictionary *data))callback;

/**将图片保存至相册
 * @param imageObj 支持 UIImage图片对象，NSData图片数据 和 NSString图片绝对路径 3种类型，传任意一种即可
 * @param albumName 相册名称，传@""或者nil则保存到默认相册
 */
- (void)addPicToAlbumWithImageObj:(id)imageObj
                        albumName:(NSString *)albumName
                          success:(void (^)(NSDictionary *result))success
                          failure:(void (^)(NSString *error))failure;
    
#pragma mark - =================== 全球渠道相关API ===================
/*
 *  账号删除
 *  调用此接口会弹出删除账号界面，删除账号后服务器也会删除，请谨慎操作
 */
- (void)accountCancellation;

/**
 *  解除【账号删除】状态，将账号恢复成正常账号
 */
- (void)accountUnCancellationWithSuccess:(void (^)(id obj))success
                                 failure:(void (^)(id obj))failure;

/**
 三方登录回调
 */
- (BOOL)ultra_application:(UIApplication *)application
      handleOpenURL:(NSURL *)url ;

/*
*  三方登录返回来的回调
*/
- (BOOL)handleOpenUniversalLink:(NSUserActivity *)userActivity ;

/*
 *  打开Xsolla商城支付
 *  @param HeroUXsollaModel  参数模型
 */
- (void)startXsollaPayWithXsollaModel:(UltraXsollaModel *)xsollaModel;

/*
 *  全球渠道绑定接口
 *  1  =   Facebook
 *  2  =   Google
 *  3  =   GameCenter
 *  4  =   WeChat
 *  5  =   Twitter
 *  7  =   Apple
 *  8  =   Line
 */
- (void)bindAccountWithType:(NSInteger)bindType
                  success:(void (^)(NSDictionary *result))success
                    failure:(void (^)(NSString *error))failure;

/*
 *  获取全球渠道用户绑定状态
 *  result数据  {
 "huaweiBind" : 0,
 "weChatBind" : 0,
 "gameWithBind" : 0,
 "twitterBind" : 0,
 "appleBind" : 0,
 "code" : 0,
 "fbBind" : 0,
 "lineBind" : 0,
 "oppoBind" : 0,
 "pwdStatus" : 0,
 "msg" : "success",
 "googleBind" : 0,
 "gcBind" : 0
}
 */
- (void)getBindStateWithSuccess:(void (^)(NSDictionary *result))success
                        failure:(void (^)(NSString *error))failure;

/** 绑定邮箱功能 */
- (void)bindEmailWithSuccess:(void (^)(NSDictionary *result))success
                     failure:(void (^)(NSString *error))failure;

#pragma mark - =================== 新分享相关API ===================

/*
 * 分享
 * @param shareModel 分享数据模型，包含分享链接、描述、分享类型等必要参数
 * @param success 成功回调
 * @param failure 失败回调
 */
- (void)ultraShareWithShareModel:(UltraShareModel *)shareModel
                         success:(void(^)(NSString *successMessage,UltraShareStatus status,UltraShareTaget target))success
                         failure:(void(^)(NSString *failureMessage,UltraShareStatus status,UltraShareTaget target))failure;


#pragma mark - =================== 浏览器 ===================

/*
 * 通过webId打开浏览器
 * @param webId   支撑平台配置的ID
 * @param success 成功回调
 * @param failure 失败回调
 */
 - (void)ultraOpenBrowserByWebId:(NSString *)webId
                         success:(void (^)(NSDictionary *dic))success
                         failure:(void (^)(NSString *error))failure;

/*
 * 通过url打开浏览器
 * @param url          http或者https链接
 * @param orientation  0为横屏，1为竖屏
 * @param bts          按钮数组，传nil，默认左上角有返回按钮
 * @param success      成功回调
 * @param failure      失败回调
 */
- (void)ultraOpenBrowserByUrl:(NSString *)url
                  orientation:(NSInteger)orientation
                          bts:(NSArray<NSDictionary *> *)bts
                      success:(void (^)(NSDictionary *dic))success
                      failure:(void (^)(NSString *error))failure;

/**
 * 获取所有浏览器对应webId的开关
 * 返回值为NSDictionary，key为webId；value，1为开启，0为关闭，如果没有key，则返回空字典
 *  {
        "test02" : 1,
        "test03" : 1
    }
 */
- (NSDictionary *)getWebSwitchCase;

#pragma mark - =================== 市场SDK插件 ===================

/**
 @brief 系统回调接口，必接
*/
- (void)applicationDidBecomeActive:(UIApplication *)application ;

/**
 @brief 自定义事件追踪接口，融合接口
 @param eventName  事件名称
 @param parameters 扩展字典，默认可以传nil
*/
- (void)logEvent:(NSString *)eventName
      parameters:(NSDictionary *)parameters;

/**
 @brief 购买事件追踪，融合接口
 @param orderId    订单Id
 @param amount     金额
 @param currency   货币
 @param parameters 扩展字典，默认可以传nil
 */
- (void)logPurchaseWithOrderId:(NSString *)orderId
                        amount:(double)amount
                      currency:(NSString *)currency
                    parameters:(NSDictionary *)parameters;

/**
 @brief fireBase_邮箱归因
 */
- (void)sendFirebaseEmailAddress:(NSString *)emailAddress ;

/**
 @brief 初始化设置appsFlyerCustomerUserID可能过早，还没有对应的值，可以选择在需要的时候调用此接口设置 appsFlyerCustomerUserID
 */
- (void)setAppsFlyerCustomerUserID:(NSString *)customerUserID ;

/**
 @brief 显示激励广告
 @param adId    激励视频广告位ID
 @param type    广告平台，暂时只支持admob
 @param successHandler    成功回调，视频播放完成，发送奖励会触发该回调
 @param failureHandler    失败回调，视频加载失败是会触发该回调
 @param closeHandler      关闭回调，关闭视频时会触发该回调

 */
- (void)showAdVideoWithAdId:(NSString *)adId
                       type:(UltraAdType)type
             successHandler:(void (^)(NSString *type, double amount))successHandler
             failureHandler:(void (^)(NSString *msg, NSInteger code))failureHandler
               closeHandler:(void (^)(void))closeHandler;

/**
 @brief 显示激励广告-新接口
 @param adId    激励视频广告位ID
 @param type    广告平台，暂时只支持admob
 @param successHandler    成功回调，视频播放完成，发送奖励会触发该回调
 @param failureHandler    失败回调，视频加载失败是会触发该回调
 @param showHandler       显示回调，视频刚开始要显示时会触发该回调
 @param closeHandler      关闭回调，关闭视频时会触发该回调
 */
- (void)showAdVideoWithAdId:(NSString *)adId
                       type:(UltraAdType)type
             successHandler:(void (^)(NSString *type, double amount))successHandler
             failureHandler:(void (^)(NSString *msg, NSInteger code))failureHandler
                showHandler:(void (^)(void))showHandler
               closeHandler:(void (^)(void))closeHandler;
@end

NS_ASSUME_NONNULL_END
