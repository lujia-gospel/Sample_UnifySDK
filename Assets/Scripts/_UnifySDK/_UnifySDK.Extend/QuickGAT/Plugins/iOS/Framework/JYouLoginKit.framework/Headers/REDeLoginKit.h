//
//

#define SDK_OVERSEAS_VERSION @"2.0.3.0"
#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "REDeRoleInfo.h"
#import "REDeOrderInfo.h"
#import "REDeLoginKit.h"
#import "REDeDelegate.h"

@interface REDeLoginKit : NSObject

#pragma mark - init
// 初始化 设置产品,最先需要调用的接口(必接)
+ (void)initSDKWithProductCode:(NSString *)productCode callback:(id<REDeInitCallback>)initDelegate;
// 是否已经初始化成功了
+ (BOOL)isInitialized;
// 设置登录回调监听对象(必接)
+ (void)setFunctionLoginCallback:(id<REDeLoginCallback>)loginDelegate;
// 设置支付回调监听对象(必接)
+ (void)setFunctionBuyCallback:(id<REDeBuyCallback>)buyDelegate;
#pragma mark - 用户服务
/**进入用户登录页面。收到切换用户通知或者主动注销用户后调用(必接)
@method    isShowMenu      YES:登录自动显示浮标   NO：登录不自动显示浮标
*/
+ (void)loginWithMenuShow:(BOOL)isShowMenu;
// 自动注册和自动登录流程,玩家首次游戏无需注册,达到快速游戏的目的,适合于没有登录按键的游戏在启动后调用
+ (void)fastlyStartGame;
// 主动调用三方方式登录账号，无SDK界面
+ (void)loginAccountType:(USERCENTER_TYPE)type;
// 主动调用绑定账号
+ (void)bindAccountType:(USERCENTER_TYPE)type;
// 主动调用解绑三方账号
+ (void)unbindAccount:(USERCENTER_TYPE)type;
//获取渠道绑定信息
+ (NSDictionary *)getUserBindInfo;
// 购买接口(必接)
+ (void)IAPWithParameter:(REDeOrderInfo *)param;
// 获取已购买的非消耗商品或者订阅商品,商品信息通过回调返回(通常游戏自己也能获取到这些商品信息，支持购买自动订阅商品或者非消耗商品时选接)
+ (void)restoreNonConsumptionProducts:(id<REDeRestoreCallback>)restoreDelegate;
//根据传入的商品id列表获取商品信息，结果通过回调返回
+ (void)findProductInfoWithProductIds:(NSArray *)productArr delegate:(id<REProductInfoCallback>)productDelegate;
//通过SDK后台配置的商品id列表获取商品信息，结果通过回调返回
+ (void)findProductInfoWithDelegate:(id<REProductInfoCallback>)productDelegate;
//@method    logout 退出登录。
+ (void)logout;
/** 显示浮标，isLeft 浮标是否居左 originalY 浮标纵向位置起始点 */
+ (void)showFloatButtonIsLeft:(BOOL)isLeft buttonOriginalY:(CGFloat)originalY;
// 隐藏浮动菜单
+ (void)dismissMenu;
// 进入用户中心
+ (void)enterUserCenter;
/** 设置销毁账户提示文案，不设置将使用默认文案 */
+ (void)configAccountDeletionTipContent:(NSString *)tipContent;
/** 销毁账户 */
+ (void)accountDeletion;

/**
 @method    userID
 @rerurn 返回用户id。如未登录，返回空。
 //通常在登录成功回调中调用
 */
+ (NSString *)userID;
// 获取用户登录账号。如未登录，返回空。
+ (NSString *)getUserAccount;
// 用户校验码，用于到服务器端验证用户真实性。
+ (NSString *)getUserToken;
// 判断是否是新用户,YES为新用户，NO为已经注册过的用户
+ (BOOL)isNewUser;
//设置角色信息 选择角色进入游戏时需要设置 必接
+ (void)setGameRoleInfo:(REDeRoleInfo *)roleInfo;
//应用OpenUrl回调 必接
+ (void)application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary *)options;
+ (void)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)source annotation:(id)annotation;
// 用户是否是游客,请先判断用户是否有登录
+ (BOOL)isUserGuest;
// 绑定第3方账号页面
+ (void)bandAccount;
/**
 @method    u_stpping
 @rerurn 返回0表示可以进入，1表示不能进入。
 //通常在登录成功回调中调用
 */
+ (BOOL)userStopping;
// 获取设备ID
+ (NSString *)getDeviceID;
// 获取渠道号
+ (NSString *)channelCode;
// 获取国家码
+ (NSDictionary *)getNationCode;
/** 是否启用游客绑定提示 YES:禁用 NO启用 默认NO  */
+ (void)guestBingTipDisable:(BOOL)yesOrNo;
/** 设置是否自动登录，默认自动登录YES*/
+ (void)setNeedAutoLogin:(BOOL)autoLogin;
/** 设置个人中心界面是否显示uid，默认不显示NO*/
+ (void)setNeedShowUid:(BOOL)show;
#pragma mark- FB分享相关接口
/**
 * 自定义FB活动分享
 * urlString:游戏自定义的活动分享地址
 */
+ (void)fbshareUrl:(NSString *)urlString;
/**
 * 服务端配置的FB分享
 */
+ (void)shareWith:(NSString *)serverId roleId:(NSString *)roleId;
/**
 * FB原生图片分享
 * image  分享的图片
 * caption 分享的照片创建标题,如果传nil则没有标题，FB平台不允许预填写分享内容这里填写了也不会在FB里展示
 * tag 话题标签,可为空，会显示在分享对话框中，因此用户在发布之前可决定是否将它删除
 * completeResults 分享回调
     分享成功 status=0，error = nil
     分享失败 status=1，error != nil
     取消分享 status=2，error = nil
 * tip:
 1.照片大小必须小于 12MB
 2.用户需要安装版本 7.0 或以上的原生 iOS 版 Facebook 应用
 */

+ (void)fbSharePhoto:(UIImage *)image caption:(NSString *)caption tag:(NSString *)tag completeWithResults:(void(^)(NSError *error,NSInteger status))completeResults;
/**
 * FB原生链接图片分享
 /// Convenience method to build a new photo object with an imageURL.
 /// This method should only be used when adding photo content to open graph stories.
 /// For example, if you’re trying to share a photo from the web by itself, download the image and use
 /// <code>init(image:isUserGenerated:)</code> instead.
  * imageURL The URL to the photo
  * caption 分享的照片创建标题,如果传nil则没有标题，FB平台不允许预填写分享内容这里填写了也不会在FB里展示
 * completeResults 分享回调
     分享成功 status=0，error = nil
     分享失败 status=1，error != nil
     取消分享 status=2，error = nil
 * tip:
 1.用户需要安装版本 7.0 或以上的原生 iOS 版 Facebook 应用
 */

+ (void)fbShareUrlPhoto:(NSString *)imageUrl caption:(NSString *)caption tag:(NSString *)tag completeWithResults:(void(^)(NSError *error,NSInteger status))completeResults;
/**
 * FB链接分享
 * urlString  分享链接地址
 * quote 高亮文本，与链接一同显示，如文章中的醒目引文,可为空
 * tag 话题标签,可为空，会显示在分享对话框中，因此用户在发布之前可决定是否将它删除
 * completeResults 分享回调
     分享成功 status=0，error = nil
     分享失败 status=1，error != nil
     取消分享 status=2，error = nil
 */
+ (void)fbShareUrl:(NSString *)urlString quote:(NSString *)quote tag:(NSString *)tag completeWithResults:(void(^)(NSError *error,NSInteger status))completeResults;
#pragma mark- FB统计相关接口
/**
 * 完成教程的统计
 * contentData  特点内容
 * contentId 特点内容id
 * success 是否完成教程
 * tip:
    fb的标准打点事件，事件名：Complete Tutorial
 */
+ (void)logCompleteTutorialEvent:(NSString *)contentData
                       contentId:(NSString *)contentId
                         success:(BOOL)success;
/**
 * 角色升级的统计
 * level  角色等级
 * tip:
    fb的标准打点事件，事件名：Achieve Level
 */
+ (void)logAchieveLevelEvent:(NSString *)level;
/**
 * 完成内购的统计
 * purchaseAmount  商品价格
 * currency  货币类型
 * parameters  额外携带参数，value值应该是NSString或NSNumber，@{@"order":@“orderid”}
 * tip:
    fb的标准打点事件，事件名：Purchase FB默认已经接入可不接
 */
+ (void)logPurchase:(double)purchaseAmount
           currency:(NSString *)currency
         parameters:(NSDictionary<NSString *, id> *)parameters;
/**
 * 成就解锁
 * description  成就解锁的描述
 * type  成就解锁类型
 * tip:
    fb的标准打点事件，事件名：Unlock Achievement
 */
+ (void)logUnlockAchievementEvent:(NSString *)description type:(NSString *)type;
/**
 * 发起结账
 * contentData  商品名称，可以是json对象列表如[{\"id\": \"1234\", \"quantity\": 2}, {\"id\": \"5678\", \"quantity\": 1}]
 * contentId 商品id
 * contentType  商品类型 product or product_group
 * numItems 商品数量
 * currency  货币类型
 * totalPrice  总价
 * tip:
    fb的标准打点事件，事件名：Initiate Checkout
 */
+ (void)logInitiateCheckoutEvent:(NSString *)contentData
                       contentId:(NSString *)contentId
                     contentType:(NSString *)contentType
                        numItems:(NSInteger)numItems
            paymentInfoAvailable:(BOOL)paymentInfoAvailable
                        currency:(NSString *)currency
                      valueToSum:(double)totalPrice;
/**
 * 自定义事件
 * eventName  事件名
 * valueToSum 开启后系统会根据这个事件关联一个值，并按这个事件发生的全部情况求出总和，便于查看平均值,当为0默认不开启
 * parameters  附带参数，可以为该事件添加参数，参数key自定义如：@{@"order":orderid}
 * tip:
    fb的自定义事件
 */
+ (void)logEvent:(NSString *)eventName valueToSum:(double)valueToSum parameters:(NSDictionary<NSString *, id> *)parameters;

#pragma mark- Appsflyer相关接口
/**
 * 初始化appsflyer
 * @param devKey AppsFlyer devKey
 * @param appID itunes connect分配的appID
 * @param Sandbox 支付验证是否用沙盒模式，不传默认为NO(正式模式), 注意：出正式提审包时一定要设置为NO
 * * @param currency 设置AF统计货币类型，可传nil或空字符串则使用默认的USD
*/
+ (void)setAppsFlyerDevKey:(NSString *)devKey appleAppID:(NSString *)appID useReceiptValidationSandbox:(BOOL)Sandbox currencyType:(NSString *)currency;

/**
 *AppsFlyer自定义事件接口（选接）
 *
 @param eventName 自定义事件名从AppsFlyerLib.framework的`AppsFlyerLib.h`的预定义常量中获取
 @param values 参数字典（可传空），key值从`AppsFlyerLib.h`的预定义常量中获取
 调用事例：
 [REDeLoginKit afTrackEvent:AFEventPurchase
        withValues: @{AFEventParamRevenue  : @200,
                      AFEventParamCurrency : @"USD",
                      AFEventParamQuantity : @2,
                      AFEventParamContentId: @"092",
                      AFEventParamReceiptId: @"9277"}];
 */
+ (void)afTrackEvent:(NSString *)eventName withValues:(NSDictionary * _Nullable)values;
/** 配置appsflyer是否需要上报SDK内登录流程事件 */
+ (void)configNeedAppsFlyerLoginEvent:(BOOL)need;
/** 开始初始化 */
+ (void)configFireBase;
#pragma mark- firebase推送相关接口
/*
 需要firebase推送必接
 需要在- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken 中添加这个代码上传deviceToken
 */
+ (void)applicationRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;
/*
 需要firebase推送必接
需要在- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error添加，当配置不全的时候会导致通知失败不走初始化
*/
+ (void)applicationdidFailToRegisterForRemoteNotifications;
#pragma mark- firebaseAnalytics相关接口
/** 统计任意事件eventname:事件名 paramDict:统计参数 */
+ (void)logEventWithName:(NSString *)eventName
             andWithParam:(NSDictionary *)paramDict;
/** 统计虚拟货币收入 */
+ (void)logGetVirualCurrencyWithCurrencyName:(NSString *)currencyName
                                andWithValue:(NSString *)value;
/** 统计加入群组 */
+ (void)logJoinGroupWithGroupId:(NSString *)groupId;
/** 统计角色升级 */
+ (void)logLevelUpWithLevel:(NSString *)level
           andWithCharacter:(NSString *)character;
/** 统计虚拟货币支出 */
+ (void)logSpendVirtualCurrencyWithItemName:(NSString *)itemName
                 andWithVirtualCurrencyName:(NSString *)currencyName
                               andWithValue:(NSString *)value;
/** 统计开始学习 */
+ (void)logTutorialBegin;
/** 统计学习结束 */
+ (void)logTurorialComplete;

@end
