//
// UltraAnalyticsSDK+HAAutoTrack.h
// UltraAnalyticsSDK
//
// Created by AlbertWei on 2021/4/2.
// Copyright © Ultra All rights reserved.
//
//

#import "UltraAnalyticsSDK.h"
#import "HAConstants.h"

NS_ASSUME_NONNULL_BEGIN

@interface UIImage (UltraAnalytics)
@property (nonatomic, copy) NSString* haAnalyticsImageName;
@end

@interface UIView (UltraAnalytics)
- (nullable UIViewController *)haAnalyticsViewController __attribute__((deprecated("已过时")));

/// viewID
@property (nonatomic, copy) NSString* haAnalyticsViewID;

/// AutoTrack 时，是否忽略该 View
@property (nonatomic, assign) BOOL haAnalyticsIgnoreView;

/// AutoTrack 发生在 SendAction 之前还是之后，默认是 SendAction 之前
@property (nonatomic, assign) BOOL haAnalyticsAutoTrackAfterSendAction;

/// AutoTrack 时，View 的扩展属性
@property (nonatomic, strong) NSDictionary* haAnalyticsViewProperties;

@property (nonatomic, weak, nullable) id<SAUIViewAutoTrackDelegate> haAnalyticsDelegate;
@end

#pragma mark -

@interface UltraAnalyticsSDK (HAAutoTrack)

- (UIViewController *_Nullable)currentViewController;

/**
 * @abstract
 * 是否开启 AutoTrack
 *
 * @return YES: 开启 AutoTrack; NO: 关闭 AutoTrack
 */
- (BOOL)isAutoTrackEnabled;

#pragma mark - Ignore

/**
 * @abstract
 * 判断某个 AutoTrack 事件类型是否被忽略
 *
 * @param eventType HAAnalyticsAutoTrackEventType 要判断的 AutoTrack 事件类型
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isAutoTrackEventTypeIgnored:(HAAnalyticsAutoTrackEventType)eventType;

/**
 * @abstract
 * 忽略某一类型的 View
 *
 * @param aClass View 对应的 Class
 */
- (void)ignoreViewType:(Class)aClass;

/**
 * @abstract
 * 判断某个 View 类型是否被忽略
 *
 * @param aClass Class View 对应的 Class
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isViewTypeIgnored:(Class)aClass;

/**
 * @abstract
 * 在 AutoTrack 时，用户可以设置哪些 controllers 不被 AutoTrack
 *
 * @param controllers   controller ‘字符串’数组
 */
- (void)ignoreAutoTrackViewControllers:(NSArray<NSString *> *)controllers;

/**
 * @abstract
 * 判断某个 ViewController 是否被忽略
 *
 * @param viewController UIViewController
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isViewControllerIgnored:(UIViewController *)viewController;

#pragma mark - Track

/**
 * @abstract
 * 通过代码触发 UIView 的 $AppClick 事件
 *
 * @param view UIView
 */
- (void)trackViewAppClick:(nonnull UIView *)view;

/**
 * @abstract
 * 通过代码触发 UIView 的 $AppClick 事件
 *
 * @param view UIView
 * @param properties 自定义属性
 */
- (void)trackViewAppClick:(nonnull UIView *)view withProperties:(nullable NSDictionary *)properties;

/**
 * @abstract
 * 通过代码触发 UIViewController 的 $AppViewScreen 事件
 *
 * @param viewController 当前的 UIViewController
 */
- (void)trackViewScreen:(UIViewController *)viewController;
- (void)trackViewScreen:(UIViewController *)viewController properties:(nullable NSDictionary<NSString *,id> *)properties;

#pragma mark - Deprecated

/**
 * @property
 *
 * @abstract
 * 打开 SDK 自动追踪,默认只追踪App 启动 / 关闭、进入页面
 *
 * @discussion
 * 该功能自动追踪 App 的一些行为，例如 SDK 初始化、App 启动 / 关闭、进入页面 等等
 * 该功能默认关闭
 */
- (void)enableAutoTrack __attribute__((deprecated("已过时，请参考 HAConfigOptions 类的 autoTrackEventType")));

/**
 * @property
 *
 * @abstract
 * 打开 SDK 自动追踪,默认只追踪App 启动 / 关闭、进入页面、元素点击
 * @discussion
 * 该功能自动追踪 App 的一些行为，例如 SDK 初始化、App 启动 / 关闭、进入页面 等等
 * 该功能默认关闭
 */
- (void)enableAutoTrack:(HAAnalyticsAutoTrackEventType)eventType __attribute__((deprecated("已过时，请参考 HAConfigOptions 类的 autoTrackEventType")));

/**
 * @abstract
 * 过滤掉 AutoTrack 的某个事件类型
 *
 * @param eventType HAAnalyticsAutoTrackEventType 要忽略的 AutoTrack 事件类型
 */
- (void)ignoreAutoTrackEventType:(HAAnalyticsAutoTrackEventType)eventType __attribute__((deprecated("已过时，请参考enableAutoTrack:(HAAnalyticsAutoTrackEventType)eventType")));

/**
 * @abstract
 * 判断某个 ViewController 是否被忽略
 *
 * @param viewControllerClassName UIViewController 类名
 *
 * @return YES:被忽略; NO:没有被忽略
 */
- (BOOL)isViewControllerStringIgnored:(NSString *)viewControllerClassName __attribute__((deprecated("已过时，请参考 -(BOOL)isViewControllerIgnored:(UIViewController *)viewController")));

/**
 * @abstract
 * Track $AppViewScreen事件
 *
 * @param url 当前页面url
 * @param properties 用户扩展属性
 */
- (void)trackViewScreen:(NSString *)url withProperties:(NSDictionary *)properties __attribute__((deprecated("已过时，请参考 trackViewScreen: properties:")));

@end

NS_ASSUME_NONNULL_END
