//
//  UltraProject.h
//  UltraSDK
//
//  Created by 魏太山 on 2020/10/27.
//  Copyright © 2020年 Ultra. All rights reserved.
//
//  本类负责Ultra【初始化】相关操作

#import <Foundation/Foundation.h>
#import <UltraBaseSDK/HBMJExtension.h>
#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface UltraProject : NSObject

/** Ultra产品ID */
@property (nonatomic, strong) NSString *ultraProductId;
/** Ultra产品秘钥 */
@property (nonatomic, strong) NSString *ultraProductKey;
/** 镜像Id */
@property (nonatomic, strong) NSString *imgId;
/** 埋点Url */
@property (nonatomic, strong) NSString *trackUrl;
/** 灾备Url，默认url放第一个 */
@property (nonatomic, strong) NSArray<NSString *> *backupUrls;

@property (nonatomic, strong) UIApplication *application;

@property (nonatomic, strong) NSDictionary *launchOptions;

/** 屏幕支持方向，默认是 UIInterfaceOrientationMaskAll */
@property (nonatomic, assign) UIInterfaceOrientationMask supportedInterfaceOrientations;

/** 如果使用了市场插件，并且使用了 AppsFlyer
    初始化usdk时，传入 appsFlyerCustomerUserID ，会自动在 AppsFlyer 初始化之前调用 customerUserID*/
@property (nonatomic, strong) NSString *appsFlyerCustomerUserID;

/**
 渠道初始化方法，提供2种初始化的方式
 1. 获取UltraService-Info.plist文件，放入工程中即可初始化，不用调用此方法（建议）
 2. 自己写字典参数传入初始化，需要调用此方法
 @param parameters 对应的参数
 */
- (void)channelInitWithParameters:(NSDictionary *)parameters;

@end

NS_ASSUME_NONNULL_END
