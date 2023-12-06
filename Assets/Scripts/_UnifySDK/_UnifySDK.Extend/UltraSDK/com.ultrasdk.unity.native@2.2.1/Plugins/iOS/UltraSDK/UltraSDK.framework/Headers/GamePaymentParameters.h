//
//  GamePaymentParameters.h
//  HeroUPlatform
//
//  Created by 魏太山 on 2023/6/6.
//  Copyright © 2023 Hero. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface GamePaymentParameters : NSObject

/**
 *  设置本次充值金额, 如果是游戏内充值, 则不需要传, (单位:元)
 *
 *  @param paymentAmount 本次充值金额
 */
-(void)setPaymentAmount:(NSString *)paymentAmount;

/**
 *   本次购买道具名称, 如果是游戏内充值, 则不需要传
 *
 *  @param paymentItemName 游戏道具名称
 */
-(void)setPaymentItemName:(NSString *)paymentItemName;


//IAP参数
//游戏传过来的产品ID
@property (nonatomic, strong) NSString * gamePropID;
//产品传过来的角色名
@property (nonatomic, strong) NSString * gameRole;//国内参数
@property (nonatomic, copy  ) NSString * cpOrder;//CP订单号，可选参数
@property (nonatomic, copy  ) NSString * callbackUrl;//内购回调地址，可由运营配置，运营配置了此参数无效（固定回调可不设置，需要同时满足多个回调地址可使用）


@end

NS_ASSUME_NONNULL_END
