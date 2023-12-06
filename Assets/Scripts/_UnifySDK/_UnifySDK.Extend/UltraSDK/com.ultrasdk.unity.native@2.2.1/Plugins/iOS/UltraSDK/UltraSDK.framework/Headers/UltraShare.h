//
//  UltraShare.h
//  UltraSDK
//
//  Created by 魏太山 on 2020/10/27.
//  Copyright © 2020 Ultra. All rights reserved.
//
//  本类负责Ultra【分享插件】相关操作

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, UltraSharePlatform) {
    
    /**QQ好友*/
    Ultra_Share_Platform_QQ = 1,
    /**QQ空间*/
    Ultra_Share_Platform_QQ_Space,
    /**微信好友*/
    Ultra_Share_Platform_WeChat,
    /**微信朋友圈*/
    Ultra_Share_Platform_WXTimeLine,
    /**微博*/
    Ultra_Share_Platform_Weibo,
    /**有界面分享*/
    Ultra_Share_Platform_All,
    
    //以下仅限全球渠道使用   且需要游戏自绘按钮
    /**twitter*/
    Ultra_Share_Platform_Twitter,
    /**facebook*/
    Ultra_Share_Platform_Facebbok,
};

typedef NS_ENUM(NSInteger, UltraShareType) {
    UltraShareType_image = 0, //分享图片
    UltraShareType_link  = 1, //分享链接
    UltraShareType_text  = 2, //分享文本
};

typedef NS_ENUM(NSInteger, UltraShareStatus) {
    UltraShareStatus_success = 0, //分享成功
    UltraShareStatus_failed  = 1, //分享失败
    UltraShareStatus_cancel  = 2, //分享取消
};

typedef NS_ENUM(NSInteger, UltraShareTaget) {
    UltraShareTaget_wechat      = 1, //分享到微信
    UltraShareTaget_wechatLine  = 2, //分享到朋友圈
    UltraShareTaget_QQ          = 3, //分享到QQ
    UltraShareTaget_QQSpace     = 4, //分享到QQ空间
    UltraShareTaget_Weibo       = 6, //分享到微博
    UltraShareTaget_Twitter     = 7, //分享到twitter
    UltraShareTaget_Facebbok    = 8, //分享到facebbok
};

NS_ASSUME_NONNULL_BEGIN

@interface UltraShare : NSObject

@end

@interface UltraShareModel : NSObject
@property (nonatomic,assign) UltraShareType      shareType; //分享类型 （必填）
@property (nonatomic,assign) UltraSharePlatform  sharePlatform ; //分享平台（必填）
//分享图片需要填写的参数
@property (nonatomic,strong) NSData            *shareImage;//分享图片（分享图片与链接2选一）
@property (nonatomic,copy  ) NSString          *imageLink;//分享图片链接（分享图片与链接2选一）
//分享链接需要填写的参数
@property (nonatomic,copy  ) NSString          *shareLink;//分享链接 （分享链接必填）
@property (nonatomic,copy  ) NSString          *shareLinkTitle;//分享链接标题（分享链接必填）
@property (nonatomic,copy  ) NSString          *shareLinkDescription;//分享链接描述（分享链接必填）
//分享文本需要填写的参数
@property (nonatomic,copy  ) NSString          *shareText;//分享纯文本文字 （分享纯文本必填）
@end

NS_ASSUME_NONNULL_END




