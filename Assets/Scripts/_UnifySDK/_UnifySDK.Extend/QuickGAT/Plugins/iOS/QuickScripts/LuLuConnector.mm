
//

#import "LuLuConnector.h"

#define TargetObject "QuickSDKCallBack"  /*自行设置需要通知的对象（U3D）*/
#define TargetMethod "QuickIOSCallBack" /*通知对象的具体方法*/

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
   extern void UnitySendMessage(const char *, const char *, const char *);
#endif

#pragma mark - SDK 平台接口调用

   void luLuInit(){
       
       //设置回调
       [REDeLoginKit setFunctionLoginCallback:[LuLuConnector shareInstance]];
       [REDeLoginKit setFunctionBuyCallback:[LuLuConnector shareInstance]];
       [REDeLoginKit restoreNonConsumptionProducts:[LuLuConnector shareInstance]];
       // 初始化
       [REDeLoginKit initSDKWithProductCode:@"60805069802178860281358919238025" callback:[LuLuConnector shareInstance]];///productCode到quickgame后台获取,参数自行设置
       /// AppsFlyer
       [REDeLoginKit setAppsFlyerDevKey:@"aKbMD3J2K5XHCQGaGEGFPM" appleAppID:@"1634609726" useReceiptValidationSandbox:NO currencyType:@"USD"];
       /// FireBase
       [REDeLoginKit configFireBase];
   }
   //显示登陆界面
   void luLuLogin(BOOL isShowMenu){
       [REDeLoginKit loginWithMenuShow:false];
   }
   //静默登录不显示登陆界面
   void luLuFastStartGame(){
       [REDeLoginKit fastlyStartGame];
   }
   //注销登陆
   void luLuLogout(){
       [REDeLoginKit logout];
   }
   //显示个人中心
   void luLuUserCenter(){
       [REDeLoginKit enterUserCenter];
   }
   //获取三方绑定信息
   const char * luLuGetUserBindInfo(){
       NSDictionary * resultDict = [REDeLoginKit getUserBindInfo];
       NSString *jsonStr = [[LuLuConnector shareInstance] dictionaryToJson:resultDict];
       if ([resultDict.allKeys containsObject:@"USERCENTER_TYPE_EMAIl"]) {
            if ([resultDict[@"USERCENTER_TYPE_EMAIl"] boolValue]) {
                  NSLog(@"绑定了邮箱");
                 jsonStr =@"绑定了邮箱";
                Send_Message("getUserBindInfo",jsonStr.UTF8String);
                return jsonStr.UTF8String;
              }
       }
       Send_Message("getUserBindInfo",jsonStr.UTF8String);
       return jsonStr.UTF8String;
   }
   //进入三方绑定界面
   void luLuBindAccount(){
       [REDeLoginKit bandAccount];
   }
   //绑定三方账号
   void luLuBindAccountWithType(int type){
       [REDeLoginKit bindAccountType:(USERCENTER_TYPE)type];
   }
   //解绑三方账号
   void luLuUnbindAccountWithType(int type){
       [REDeLoginKit unbindAccount:(USERCENTER_TYPE)type];
   }
   //删除当前登录账号，游客登录没有此功能
   void luLuAccountDeletion(){
       [REDeLoginKit accountDeletion];
   }
   //显示悬浮窗
   void luLuShowMenu(){
       //[REDeLoginKit showMenu:TopMenuButtonPositionLeft|TopMenuButtonPositionUpside];
   }
   //隐藏悬浮窗
   void luLuDismissMenu(){
       [REDeLoginKit dismissMenu];
   }
   void luluSetRoleInfo(const char *serverName,const char *serverId,const char *roleId,const char *roleName,const char *roleLevel,const char *roleVipLevel){
       REDeRoleInfo * role = [[REDeRoleInfo alloc]init];
       role.server_name = CreateNSString(serverName);
       role.server_id = CreateNSString(serverId);
       role.game_role_name = CreateNSString(roleName);
       role.game_role_id = CreateNSString(roleId);
       role.game_role_level = CreateNSString(roleLevel);
       role.vip_level = CreateNSString(roleVipLevel);
       [REDeLoginKit setGameRoleInfo:role];
   }

   void luLuCongzi(const char *productId,const char *productName,char * amount,const char *orderNo,const char *callBackUrl,const char *extrasParams){
       NSLog(@"luLuCongzi成功:\nproductId:%s,productName:%s,amount:%s,extrasParams:%s",productId,productName,amount,extrasParams);
       REDeOrderInfo *param = [[REDeOrderInfo alloc] init];
       param.productId = CreateNSString(productId);    ///设置商品ID，苹果后台对应的productID
       param.subject = CreateNSString(productName);    ///商品名称 必填
       param.total = CreateNSString(amount);                       ///商品总价 必填
       param.product_order_no = CreateNSString(orderNo);       ///游戏方订单号 string[64] 接入QuickAd时必填、必须唯一
       param.callback_url = CreateNSString(callBackUrl);       ///回调通知地址 string[200] 可选  客户端配置优先;  可传nil
       param.extras_params = CreateNSString(extrasParams);     ///透传参数  可选
       [REDeLoginKit IAPWithParameter:param];
   }
   void luluRestoreNonConsumptionProducts(){
       [REDeLoginKit restoreNonConsumptionProducts:[LuLuConnector shareInstance]];
   }
   void luluFindProductInfo(const char *productIds){
       NSString * productIdStr = CreateNSString(productIds);
       NSArray * productIdArr = [[LuLuConnector shareInstance] arrFromJsonStr:productIdStr];
       if (productIdArr.count) {
           [REDeLoginKit findProductInfoWithProductIds:productIdArr delegate:(id)[LuLuConnector shareInstance]];
       } else {
           Send_Message("findProductInfo","productids is not json string");
       }
   }
/// FaceBook 分享图片
   void luluFbSharePhoto(const char *imageName,const char *caption,const char *tag){
       
       NSString *imgPath = [[NSString alloc]initWithUTF8String:imageName];
       NSLog(@"_NativeFacebookShare local image path: %@", imgPath);
       UIImage *image = [UIImage imageWithContentsOfFile:imgPath];
       
       [REDeLoginKit fbSharePhoto:image caption:CreateNSString(caption) tag:CreateNSString(tag) completeWithResults:^(NSError *error, NSInteger status) {
           if (status == 2) {
               status = 0;
           }
           NSString *param = [NSString stringWithFormat:@"%@_%ld",error,(long)status];
           Send_Message("onFbSharePhoto",param.UTF8String);
           if (status == 0) {
               NSLog(@"分享成功");
           }else if(status == 1){
               NSLog(@"分享失败%@",error.description);
           }else{
               NSLog(@"用户取消了分享");
           }
       }];
   }
/// FaceBook 分享URl
   void luluFbShareUrl(const char *url,const char *quote,const char *tag){
       
       [REDeLoginKit fbShareUrl:CreateNSString(url)  quote:CreateNSString(quote) tag:CreateNSString(tag) completeWithResults:^(NSError *error, NSInteger status) {
           if (status == 2) {
               status = 0;
           }
           NSString *param = [NSString stringWithFormat:@"%@_%ld",error,(long)status];
           Send_Message("onFbShareUrl",param.UTF8String);
           if (status == 0) {
               NSLog(@"分享成功");
           }else if(status == 1){
               NSLog(@"分享失败%@",error.description);
           }else{
               NSLog(@"用户取消了分享");
           }
       }];
   }


    /// 获取游客登录
   bool luluIsUserGuest(){
      return [REDeLoginKit isUserGuest];
   }
    ///获取设备ID
    const char*  luLuGetDeviceID(){
        return [REDeLoginKit getDeviceID].UTF8String;
    }

    ///获取渠道号
    const char*  luLuGetchannelCode(){
      return  [REDeLoginKit channelCode].UTF8String;
    }

    ///获取国家码
    const char* luLuGetgetNationCode(){
        NSDictionary *dic = [REDeLoginKit getNationCode];
        if ([dic.allKeys containsObject:@"countryCode"]) {
             if ([dic[@"countryCode"] stringValue]) {
                 return  [dic[@"countryCode"] stringValue].UTF8String;
               }
        }
        return  "";
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

@implementation LuLuConnector

+(LuLuConnector *)shareInstance{
   
   static LuLuConnector *shareInstance = nil;
   static dispatch_once_t predicate;
   dispatch_once(&predicate, ^{
       shareInstance = [[LuLuConnector alloc] init];
   });
   return shareInstance;
}
//MARK: - 回调
// 初始化结果回调
- (void)qgSDKInitDone{
   // 初始化成功
   Send_Message("onInitSuccess","Init:Init Success");
}
// 登录回调
- (void)loginUid:(NSString *)uid userToken:(NSString *)token{
   NSLog(@"登录成功:\n用户ID:%@,验证码:%@",uid,token);
   NSString *param = [NSString stringWithFormat:@"%@_%@",uid,token];
   Send_Message("onLoginSuccess",param.UTF8String);
}
//绑定回调
- (void)bindUid:(NSString *)uid userToken:(NSString *)token type:(USERCENTER_TYPE)type
{
   NSString *param = [NSString stringWithFormat:@"%@_%@_%ld",uid,token,type];
   Send_Message("onBindSuccess",param.UTF8String);
}
//解绑回调
- (void)unBindUid:(NSString *)uid userToken:(NSString *)token type:(USERCENTER_TYPE)type
{
   NSString *param = [NSString stringWithFormat:@"%@_%@_%ld",uid,token,type];
   Send_Message("onUnBindSuccess",param.UTF8String);
}
//在SDK的个人中心主动退出登录
- (void)userLogout{
   NSLog(@"用户从个人中心手动登出。");
   Send_Message("onLogoutSuccess","Logout");
}
//支付结果的回调
//购买完成 内购商品Id SDK订单号
- (void)purchaseDoneProductId:(NSString *)productId orderNo:(NSString *)orderNo gameOrderNo:(NSString *)gameOrderNo appStoreReceiptBase64EncodedString:(NSString *)receiptString
{
   NSString *param = [NSString stringWithFormat:@"%@_%@_%@_%@",productId,orderNo,gameOrderNo,receiptString];
   Send_Message("onPurchaseSuc",param.UTF8String);
}
//购买失败
- (void)purchaseFail{
   Send_Message("onPurchaseFail","");
}
//恢复非消耗商品成功，返回商品id信息
- (void)restoreSuccess:(NSArray *)products
{
   NSString *jsonStr = [self jsonStrFromArr:products];
   Send_Message("onRestoreSuccess",jsonStr.UTF8String);
}
//恢复失败
- (void)restoreFail:(NSString *)msg
{
   Send_Message("onRestoreFail",msg.UTF8String);
}
//查找商品信息成功数组元素为REDeOrderInfo实例
- (void)findProductInfoSuccess:(NSArray *)products {
   NSMutableArray * arr = [[NSMutableArray alloc]init];
   for (REDeOrderInfo * info in products) {
//        NSLog(@"查询商品信息成功结果：商品id=%@,商品名称=%@，商品描述=%@，商品价格=%@，商品总价=%@，透传参数=%@",info.productId, info.subject, info.desc, info.price, info.total, info.extras_params);
       [arr addObject:@{@"productId":info.productId, @"productName":info.subject, @"productDesc":info.desc, @"price":info.price, @"total":info.total, @"extras_params":info.extras_params}];
   }
   NSString *jsonStr = [self jsonStrFromArr:arr];
   Send_Message("findProductInfo",jsonStr.UTF8String);
}
//查找商品信息失败
- (void)findProductInfoFail:(NSString *)msg {
   Send_Message("findProductInfo",msg.UTF8String);
}
- (NSString *)jsonStrFromArr:(NSArray *)arr {
   NSError *error = nil;
   NSData *jsonData = [NSJSONSerialization dataWithJSONObject:arr options:0 error:&error];
   if ([error code]) {
       return @"";
   }
   return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}
- (NSArray *)arrFromJsonStr:(NSString *)string {
   NSError *error = nil;
   NSData* data = [string dataUsingEncoding:NSUTF8StringEncoding];
   id jsonObject = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers|NSJSONReadingMutableLeaves error:nil];
   if (jsonObject) {
       return jsonObject;
   }
   return @[];
}
- (NSString*)dictionaryToJson:(NSDictionary *)dic
{
   NSError *parseError = nil;
   NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dic options:NSJSONWritingPrettyPrinted error:&parseError];
   return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
}
@end
