//  UltraAnalyticsSDK.h
//  UltraAnalyticsSDK
//
//  Created by AlbertWei on 15/7/1.
//  Copyright © Ultra All rights reserved.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <UIKit/UIApplication.h>

#import "UltraAnalyticsSDK+Public.h"
#import "HASecurityPolicy.h"
#import "HAConfigOptions.h"
#import "HAConstants.h"

#if __has_include("UltraAnalyticsSDK+HAAutoTrack.h")
#import "UltraAnalyticsSDK+HAAutoTrack.h"
#endif

#if __has_include("UltraAnalyticsSDK+WKWebView.h")
#import "UltraAnalyticsSDK+WKWebView.h"
#endif

#if __has_include("UltraAnalyticsSDK+WebView.h")
#import "UltraAnalyticsSDK+WebView.h"
#endif

#if __has_include("UltraAnalyticsSDK+Visualized.h")
#import "UltraAnalyticsSDK+Visualized.h"
#endif
