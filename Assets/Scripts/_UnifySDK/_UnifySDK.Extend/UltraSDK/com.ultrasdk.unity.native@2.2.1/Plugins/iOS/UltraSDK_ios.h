//
//

#import <Foundation/Foundation.h>
#import <UltraSDK/UltraSDK.h>


@interface UltraSDK_ios : NSObject
{
    NSString *_gameObjectName;
}
+ (UltraSDK_ios *)shareInstance;


//融合、国内初始化
- (void)initWithUltraSDKKey:(NSString *)productKey
              productID:(NSString *)productID ;
//登录
- (void)enterLoginView ;
//退出登录
- (void)logout ;
//退出登录并显示账号历史界面
- (void)logoutAndSowLoginView ;
//用户中心
- (void)showUserCenter ;
//清除本地账号
- (void)cleanUserEntities ;
//用户名
- (NSString *)getUserName ;
//用户ID
- (NSString *)getUserId ;
//SDKID
- (NSString *)getSdkId ;
//获取设备号
- (NSString *)getDeviceNum ;
//内购
- (void)iapPurchaseWithData:(GamePaymentParameters *)parameters ;
//设置基础参数
- (void)setBaseRoleInfoWithData:(UltraHDCBaseGameRoleInfo *)baseInfo ;
//角色登录
- (void)roleLoginWithGameRoleInfo:(UltraHDCGameRoleInfo *)roleInfo ;
//角色注册
- (void)roleRegisterWithGameRoleInfo:(UltraHDCGameRoleInfo *)roleInfo ;
//角色升级
- (void)roleLevelUpWithGameRoleInfo:(UltraHDCGameRoleInfo *)roleInfo ;
//分享
- (void)share:(BOOL)hasUi title:(NSString *)title content:(NSString *)content imagePath:(NSString *)imagePath url:(NSString *)url shareTo:(NSString *)shareTo ;
//上报闪屏
- (void)postSplashScreenEndSuccess ;
//打开内置浏览器
- (void)openBrowser:(NSString *)openID ;
//打开用户中心
- (void)showAccountCenter ;
//注销账号
- (void)accountCancellation ;
//撤回注销 账号
- (void)accountUnCancellation ;
//切换账号
- (void)switchAccount ;
//收到踢下线结果回调SDK
- (void)cpKickOffCallBackWithResult:(NSString *)result ;
//获取协议内容
- (NSString *)getProtocolResult ;
//点击同意通知
- (void)setAgreeProtocol ;

@end
