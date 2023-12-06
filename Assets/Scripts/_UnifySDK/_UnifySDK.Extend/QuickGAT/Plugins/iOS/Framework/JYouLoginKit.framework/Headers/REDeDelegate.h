
//
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger,USERCENTER_TYPE){
    USERCENTER_TYPE_Guest = 0,        //guest
    USERCENTER_TYPE_EMAIl = 1,        //email
    USERCENTER_TYPE_Apple = 16,       //Apple
};
typedef NS_ENUM(NSInteger, LoginEvent){
    LoginEvent_login_start,
    LoginEvent_show_login_view,
    LoginEvent_login_end_with_cancel,
    LoginEvent_login_end_with_success,
    LoginEvent_login_end_with_failed,
    LoginEvent_guest_login_start,
    LoginEvent_guest_login_end_with_failed,
    LoginEvent_guest_login_end_with_success,
    LoginEvent_auto_login_start,
    LoginEvent_auto_login_failed,
    LoginEvent_auto_login_success,
    LoginEvent_auto_login_cancel,
    LoginEvent_show_register_view,
    LoginEvent_register_start,
    LoginEvent_register_failed,
    LoginEvent_register_success,
    LoginEvent_email_login_start,
    LoginEvent_email_login_end_with_failed,
    LoginEvent_email_login_end_with_success,
    LoginEvent_apple_login_start,
    LoginEvent_apple_login_end_with_cancel,
    LoginEvent_apple_login_end_with_failed,
    LoginEvent_apple_login_end_with_success,
    LoginEvent_apple_login_verify_start,
    LoginEvent_apple_login_verify_failed,
    LoginEvent_apple_login_verify_success,
};

@protocol REDeInitCallback <NSObject>
/* 初始化完成 */
- (void)qgSDKInitDone;
@end
@protocol REDeLoginCallback <NSObject>
/* 绑定回调 */
- (void)bindUid:(NSString *)uid userToken:(NSString *)token type:(USERCENTER_TYPE)type;
/* 解绑回调 */
- (void)unBindUid:(NSString *)uid userToken:(NSString *)token type:(USERCENTER_TYPE)type;
/* 在SDK的个人中心主动退出登录 */
- (void)userLogout;
@optional
/* 登录成功与带登录方式实现其一即可 */
- (void)loginUid:(NSString *)uid userToken:(NSString *)token;
/* 登录成功带登录方式与不带登录方式实现其一即可 */
- (void)loginUid:(NSString *)uid userToken:(NSString *)token type:(USERCENTER_TYPE)type;
/* 调用logout执行成功后执行回调,与用户在SDK的个人中心主动退出登录回调不同 */
- (void)gameLogoutSuccess;
/* 点击了个人中心的客服按钮时回调 */
- (void)onClickServiceCenter;
/** 玩家取消登录，主要用于单独调起某个三方登录方法
 isShow：YES:玩家在登录界面显示的情况下取消三方登录，NO:玩家在登录界面未显示的情况下取消三方登录
 */
- (void)userCancelLoginWithLoginPageShowing:(BOOL)isShow;
/** 玩家登录失败，主要用于单独调起某个三方登录方法
 isShow：YES:玩家在登录界面显示的情况下登录失败，NO:玩家在登录界面未显示的情况下登录失败
 message：失败原因
 */
- (void)userLoginFailWithLoginPageShowing:(BOOL)isShow message:(NSString *)message;
/** 登录流程事件回调，
 loginEvent：事件枚举值
 message: failed事件返回失败原因，其他返回枚举值字符串
 */
- (void)onEvent:(LoginEvent)loginEvent message:(NSString *)message;
@end
@protocol REDeBuyCallback <NSObject>
/* 这里的成功回调不能作为发货依据
   购买完成回调
 productId 内购商品Id
 orderNo SDK订单号
 gameOrderNo 游戏订单号，如果本地缓存被清理会返回空字符串
 如果本地缓存被清理会返回空字典
 receiptString 票据凭证
 */
- (void)purchaseDoneProductId:(NSString *)productId orderNo:(NSString *)orderNo gameOrderNo:(NSString *)gameOrderNo appStoreReceiptBase64EncodedString:(NSString *)receiptString;
//* 购买失败 */
- (void)purchaseFail;
@end

@protocol REDeRestoreCallback <NSObject>
/* 恢复非消耗商品成功，返回商品id信息 */
- (void)restoreSuccess:(NSArray *)products;
/*恢复失败*/
- (void)restoreFail:(NSString *)msg;

@end
@protocol REProductInfoCallback <NSObject>
/* 查找商品信息成功数组元素为REDeOrderInfo实例 */
- (void)findProductInfoSuccess:(NSArray *)products;
/* 查找商品信息失败 */
- (void)findProductInfoFail:(NSString *)msg;

@end
