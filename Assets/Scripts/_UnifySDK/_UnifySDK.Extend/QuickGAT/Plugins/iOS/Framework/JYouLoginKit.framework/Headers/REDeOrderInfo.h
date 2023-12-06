//
//

#import <Foundation/Foundation.h>

@interface REDeOrderInfo : NSObject
@property (copy,nonatomic) NSString *productId;     //必填 开发者后台的商品ID，productID
@property (copy,nonatomic) NSString *product_order_no;  //游戏产品订单号必传
@property (copy,nonatomic) NSString *subject;       //虚拟商品名称 必填
@property (copy,nonatomic) NSString *desc;          //商品描述 可选
@property (copy,nonatomic) NSString *price;           //商品单价 可选
@property (assign,nonatomic) unsigned int quantity;   //购买数量 可选 默认1
@property (copy,nonatomic) NSString *total;       //商品总价 必填，影响购买统计和后台回调
@property (copy,nonatomic) NSString *callback_url;      //回调通知地址 string[200] 可选，（可后台配置，后台配置了回调地址就以后台配置的为准）
@property (copy,nonatomic) NSString *extras_params;     //扩展参数 string[500] 可选，透传字段，注意：扩展字段请勿传特殊符号，如果无法避免建议先进行base64编码后再传

+ (instancetype)infoWithProductId:(NSString *)productId orderNo:(NSString *)orderNo subject:(NSString *)subject total:(NSString *)totalPrice;
- (void)setInfoWithParameter:(REDeOrderInfo *)param;

@end
