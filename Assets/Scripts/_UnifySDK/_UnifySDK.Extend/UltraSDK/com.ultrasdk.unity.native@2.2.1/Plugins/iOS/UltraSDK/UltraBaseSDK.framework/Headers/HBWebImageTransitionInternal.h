/*
* This file is part of the UltraWebImage package.
* (c) Olivier Poitrey <rs@dailymotion.com>
*
* For the full copyright and license information, please view the LICENSE
* file that was distributed with this source code.
*/

#import <UltraBaseSDK/HBWebImageCompat.h>

#if HB_MAC

#import <QuartzCore/QuartzCore.h>

/// Helper method for Core Animation transition
FOUNDATION_EXPORT CAMediaTimingFunction * _Nullable SDTimingFunctionFromAnimationOptions(UltraWebImageAnimationOptions options);
FOUNDATION_EXPORT CATransition * _Nullable UltraTransitionFromAnimationOptions(UltraWebImageAnimationOptions options);

#endif
