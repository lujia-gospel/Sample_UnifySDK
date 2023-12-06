//
//  UltraPay.h
//  UltraSDK
//
//  Created by 魏太山 on 2020/10/27.
//  Copyright © 2020年 Ultra. All rights reserved.
//
//  本类负责Ultra【支付】相关操作

#import <Foundation/Foundation.h>
#import "GamePaymentParameters.h"

NS_ASSUME_NONNULL_BEGIN

@interface UltraPay : NSObject

@end

@interface UltraXsollaModel : NSObject

// 服务器ID
@property (nonatomic, copy) NSString * serverId;
// 角色名称
@property (nonatomic, copy) NSString * roleName;
// 角色ID
@property (nonatomic, copy) NSString * roleId;
// 游戏用户iD
@property (nonatomic, copy) NSString * cpUid;
// 是否跳转外部浏览器，YES为外部浏览器，NO为内置浏览器，默认为NO
@property (nonatomic, assign) BOOL isJumpOutBrowser;

@end


NS_ASSUME_NONNULL_END
