/*
 * This file is part of the UltraWebImage package.
 * (c) Olivier Poitrey <rs@dailymotion.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

#import <Foundation/Foundation.h>
#import <UltraBaseSDK/HBImageIOAnimatedCoder.h>

/**
 Built in coder using ImageIO that supports APNG encoding/decoding
 */
@interface HBImageAPNGCoder : HBImageIOAnimatedCoder <UltraProgressiveImageCoder, HBAnimatedImageCoder>

@property (nonatomic, class, readonly, nonnull) HBImageAPNGCoder *sharedCoder;

@end
