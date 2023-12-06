//
//  UltraPUSHService.h
//  CmgeChinaLegal
//
//  Created by 魏太山 on 2020/5/14.
//  Copyright © 2020 Cmge. All rights reserved.
//

#import <Foundation/Foundation.h>
@protocol UltraPUSHRegisterDelegate;
@class UNNotification;

typedef NS_OPTIONS(NSUInteger, UltraPAuthorizationOptions) {
    UltraPAuthorizationOptionNone    = 0,   // the application may not present any UI upon a notification being received
    UltraPAuthorizationOptionBadge   = (1 << 0),    // the application may badge its icon upon a notification being received
    UltraPAuthorizationOptionSound   = (1 << 1),    // the application may play a sound upon a notification being received
    UltraPAuthorizationOptionAlert   = (1 << 2),    // the application may display an alert upon a notification being received
    UltraPAuthorizationOptionCarPlay = (1 << 3),    // The ability to display notifications in a CarPlay environment.
    UltraPAuthorizationOptionCriticalAlert NS_AVAILABLE_IOS(12.0) = (1 << 4) ,   //The ability to play sounds for critical alerts.
    UltraPAuthorizationOptionProvidesAppNotificationSettings NS_AVAILABLE_IOS(12.0) = (1 << 5) ,      //An option indicating the system should display a button for in-app notification settings.
    UltraPAuthorizationOptionProvisional NS_AVAILABLE_IOS(12.0) = (1 << 6) ,     //The ability to post noninterrupting notifications provisionally to the Notification Center.
    UltraPAuthorizationOptionAnnouncement NS_AVAILABLE_IOS(13.0) = (1 << 7) , //The ability for Siri to automatically read out messages over AirPods.
};

typedef NS_ENUM(NSUInteger, UltraPAuthorizationStatus) {
    UltraPAuthorizationNotDetermined    = 0,   // The user has not yet made a choice regarding whether the application may post user notifications.
    UltraPAuthorizationStatusDenied,    // The application is not authorized to post user notifications.
    UltraPAuthorizationStatusAuthorized,    // The application is authorized to post user notifications.
    UltraPAuthorizationStatusProvisional NS_AVAILABLE_IOS(12.0),    // The application is authorized to post non-interruptive user notifications.
};


/*!
 * 通知注册实体类
 */
@interface UltraPUSHRegisterEntity : NSObject

/*!
 * 支持的类型
 * badge,sound,alert
 */
@property (nonatomic, assign) NSInteger types;
/*!
 * 注入的类别
 * iOS10 UNNotificationCategory
 * iOS8-iOS9 UIUserNotificationCategory
 */
@property (nonatomic, strong) NSSet *categories;
@end

@interface UltraPUSHService : NSObject

///----------------------------------------------------
/// @name Setup 启动相关
///----------------------------------------------------

/*!
 * @abstract 注册方法
 *
 * @param config 注册通知配置
 * @param delegate 代理
 *
 */
+ (void)registerForRemoteNotificationConfig:(UltraPUSHRegisterEntity *)config delegate:(id<UltraPUSHRegisterDelegate>)delegate;

/*!
 * @abstract 处理收到的 APNs 消息
 */
+ (void)handleRemoteNotification:(NSDictionary *)remoteInfo;

/*!
* @abstract 注册deviceToken
*/
+ (void)registerDeviceToken:(NSData *)deviceToken;
@end


@class UNUserNotificationCenter;
@class UNNotificationResponse;

@protocol UltraPUSHRegisterDelegate <NSObject>

/*
 * @brief handle UserNotifications.framework [willPresentNotification:withCompletionHandler:]
 * @param center [UNUserNotificationCenter currentNotificationCenter] 新特性用户通知中心
 * @param notification 前台得到的的通知对象
 * @param completionHandler 该callback中的options 请使用UNNotificationPresentationOptions
 */
- (void)ultraPushNotificationCenter:(UNUserNotificationCenter *)center willPresentNotification:(UNNotification *)notification withCompletionHandler:(void (^)(NSUInteger options))completionHandler API_AVAILABLE(ios(10.0));

/*
 * @brief handle UserNotifications.framework [didReceiveNotificationResponse:withCompletionHandler:]
 * @param center [UNUserNotificationCenter currentNotificationCenter] 新特性用户通知中心
 * @param response 通知响应对象
 * @param completionHandler
 */
- (void)ultraPushNotificationCenter:(UNUserNotificationCenter *)center didReceiveNotificationResponse:(UNNotificationResponse *)response withCompletionHandler:(void(^)(void))completionHandler API_AVAILABLE(ios(10.0));

/*
 * @brief handle UserNotifications.framework [openSettingsForNotification:]
 * @param center [UNUserNotificationCenter currentNotificationCenter] 新特性用户通知中心
 * @param notification 当前管理的通知对象
 */
- (void)ultraPushNotificationCenter:(UNUserNotificationCenter *)center openSettingsForNotification:(UNNotification *)notification API_AVAILABLE(ios(12.0));

/**
 * 监测通知授权状态返回的结果
 * @param status 授权通知状态，详见UltraPAuthorizationStatus
 * @param info 更多信息，预留参数
 */
- (void)ultraPushNotificationAuthorization:(UltraPAuthorizationStatus)status withInfo:(NSDictionary *)info;

@end

