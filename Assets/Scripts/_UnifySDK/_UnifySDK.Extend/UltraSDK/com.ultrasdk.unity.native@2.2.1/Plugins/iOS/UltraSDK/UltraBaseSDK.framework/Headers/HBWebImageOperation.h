/*
 * This file is part of the UltraWebImage package.
 * (c) Olivier Poitrey <rs@dailymotion.com>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 */

#import <Foundation/Foundation.h>

/// A protocol represents cancelable operation.
@protocol HBWebImageOperation <NSObject>

- (void)cancel;

@end

/// NSOperation conform to `HBWebImageOperation`.
@interface NSOperation (HBWebImageOperation) <HBWebImageOperation>

@end
