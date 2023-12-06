
//

#import <Foundation/Foundation.h>
#import <JySDK/JySDKManager.h>

@interface QKConnector : NSObject<KAcountDelegate>
{
    NSString *_gameObjectName;
}
@property (nonatomic, assign) int initState; //-1未初始化，0初始化失败，1初始化成功
@property (nonatomic, assign) BOOL bU3dInited; //setListener表明u3d初始化成功

+(QKConnector *)shareInstance;
- (NSString *)jsonStrFromDictionary:(NSDictionary *)dic;
-(void)sendU3dMessage:(NSString *)messageName :(NSDictionary *)dict;
- (void)setListener:(NSString *)gameObjectName;

@end
