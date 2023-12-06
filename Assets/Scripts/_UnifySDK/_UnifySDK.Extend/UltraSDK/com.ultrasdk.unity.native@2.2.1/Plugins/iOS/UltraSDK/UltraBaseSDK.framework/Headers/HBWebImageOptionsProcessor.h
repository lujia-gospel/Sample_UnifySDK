/*
 * This file is part of the UltraWebImage package.
 * (c) Olivier Poitrey <rs@dailymotion.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

#import <Foundation/Foundation.h>
#import <UltraBaseSDK/HBWebImageCompat.h>
#import <UltraBaseSDK/HBWebImageDefine.h>

@class UltraWebImageOptionsResult;

typedef UltraWebImageOptionsResult * _Nullable(^HBWebImageOptionsProcessorBlock)(NSURL * _Nullable url, UltraWebImageOptions options, UltraWebImageContext * _Nullable context);

/**
 The options result contains both options and context.
 */
@interface UltraWebImageOptionsResult : NSObject

/**
 WebCache options.
 */
@property (nonatomic, assign, readonly) UltraWebImageOptions options;

/**
 Context options.
 */
@property (nonatomic, copy, readonly, nullable) UltraWebImageContext *context;

/**
 Create a new options result.

 @param options options
 @param context context
 @return The options result contains both options and context.
 */
- (nonnull instancetype)initWithOptions:(UltraWebImageOptions)options context:(nullable UltraWebImageContext *)context;

@end

/**
 This is the protocol for options processor.
 Options processor can be used, to control the final result for individual image request's `UltraWebImageOptions` and `UltraWebImageContext`
 Implements the protocol to have a global control for each indivadual image request's option.
 */
@protocol HBWebImageOptionsProcessor <NSObject>

/**
 Return the processed options result for specify image URL, with its options and context

 @param url The URL to the image
 @param options A mask to specify options to use for this request
 @param context A context contains different options to perform specify changes or processes, see `UltraWebImageContextOption`. This hold the extra objects which `options` enum can not hold.
 @return The processed result, contains both options and context
 */
- (nullable UltraWebImageOptionsResult *)processedResultForURL:(nullable NSURL *)url
                                                    options:(UltraWebImageOptions)options
                                                    context:(nullable UltraWebImageContext *)context;

@end

/**
 A options processor class with block.
 */
@interface HBWebImageOptionsProcessor : NSObject<HBWebImageOptionsProcessor>

- (nonnull instancetype)initWithBlock:(nonnull HBWebImageOptionsProcessorBlock)block;
+ (nonnull instancetype)optionsProcessorWithBlock:(nonnull HBWebImageOptionsProcessorBlock)block;

@end
