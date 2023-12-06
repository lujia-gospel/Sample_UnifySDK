//
//  HAConstants.h
//  HeroAnalyticsSDK
//
//  Created by AlbertWei on 2018/8/9.
//  Copyright © Hero. All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <AVKit/AVKit.h>

#pragma mark - typedef
/**
 * @abstract
 * Debug 模式，用于检验数据导入是否正确。该模式下，事件会逐条实时发送到 HeroAnalytics，并根据返回值检查
 * 数据导入是否正确。
 *
 * @discussion
 * Debug 模式的具体使用方式，请参考:
 *  http://www.herodata.cn/manual/debug_mode.html
 *
 * Debug模式有三种选项:
 *   HAAnalyticsDebugOff - 关闭 DEBUG 模式
 *   HAAnalyticsDebugOnly - 打开 DEBUG 模式，但该模式下发送的数据仅用于调试，不进行数据导入
 *   HAAnalyticsDebugAndTrack - 打开 DEBUG 模式，并将数据导入到 HeroAnalytics 中
 */
typedef NS_ENUM(NSInteger, HAAnalyticsDebugMode) {
    HAAnalyticsDebugOff,
    HAAnalyticsDebugOnly,
    HAAnalyticsDebugAndTrack,
};

/**
 * @abstract
 * TrackTimer 接口的时间单位。调用该接口时，传入时间单位，可以设置 event_duration 属性的时间单位。
 *
 * @discuss
 * 时间单位有以下选项：
 *   HAAnalyticsTimeUnitMilliseconds - 毫秒
 *   HAAnalyticsTimeUnitSeconds - 秒
 *   HAAnalyticsTimeUnitMinutes - 分钟
 *   HAAnalyticsTimeUnitHours - 小时
 */
typedef NS_ENUM(NSInteger, HAAnalyticsTimeUnit) {
    HAAnalyticsTimeUnitMilliseconds,
    HAAnalyticsTimeUnitSeconds,
    HAAnalyticsTimeUnitMinutes,
    HAAnalyticsTimeUnitHours
};


/**
 * @abstract
 * AutoTrack 中的事件类型
 *
 * @discussion
 *   HAAnalyticsEventTypeAppStart - $AppStart
 *   HAAnalyticsEventTypeAppEnd - $AppEnd
 *   HAAnalyticsEventTypeAppClick - $AppClick
 *   HAAnalyticsEventTypeAppViewScreen - $AppViewScreen
 */
typedef NS_OPTIONS(NSInteger, HAAnalyticsAutoTrackEventType) {
    HAAnalyticsEventTypeNone      = 0,
    HAAnalyticsEventTypeAppStart      = 1 << 0,
    HAAnalyticsEventTypeAppEnd        = 1 << 1,
    HAAnalyticsEventTypeAppClick      = 1 << 2,
    HAAnalyticsEventTypeAppViewScreen = 1 << 3,
};

/**
 * @abstract
 * 网络类型
 *
 * @discussion
 *   HAAnalyticsNetworkTypeNONE - NULL
 *   HAAnalyticsNetworkType2G - 2G
 *   HAAnalyticsNetworkType3G - 3G
 *   HAAnalyticsNetworkType4G - 4G
 *   HAAnalyticsNetworkTypeWIFI - WIFI
 *   HAAnalyticsNetworkTypeALL - ALL
 *   HAAnalyticsNetworkType5G - 5G
 */
typedef NS_OPTIONS(NSInteger, HAAnalyticsNetworkType) {
    HAAnalyticsNetworkTypeNONE     = 0,
    HAAnalyticsNetworkType2G       = 1 << 0,
    HAAnalyticsNetworkType3G       = 1 << 1,
    HAAnalyticsNetworkType4G       = 1 << 2,
    HAAnalyticsNetworkTypeWIFI     = 1 << 3,
    HAAnalyticsNetworkTypeALL      = 0xFF,

#ifdef __IPHONE_14_1
    HAAnalyticsNetworkType5G = 1 << 4,
#endif
};

/**
 HAConfigOptions 实现
 私有 property
 */
//@interface HAConfigOptions()
//
///// 数据接收地址 serverURL
//@property(atomic, copy) NSString *serverURL;
//
///// App 启动的 launchOptions
//@property(nonatomic, strong) id launchOptions;
//
//@end

@protocol SAUIViewAutoTrackDelegate <NSObject>

//UITableView
@optional
- (NSDictionary *)heroAnalytics_tableView:(UITableView *)tableView autoTrackPropertiesAtIndexPath:(NSIndexPath *)indexPath;

//UICollectionView
@optional
- (NSDictionary *)heroAnalytics_collectionView:(UICollectionView *)collectionView autoTrackPropertiesAtIndexPath:(NSIndexPath *)indexPath;
@end

/**
 * @abstract
 * 自动追踪 (AutoTrack) 中，实现该 Protocal 的 Controller 对象可以通过接口向自动采集的事件中加入属性
 *
 * @discussion
 * 属性的约束请参考 track:withProperties:
 */
@protocol HAAutoTracker <NSObject>

@required
- (NSDictionary *)getTrackProperties;

@end

@protocol SAScreenAutoTracker <HAAutoTracker>

@required
- (NSString *)getScreenUrl;

@end
