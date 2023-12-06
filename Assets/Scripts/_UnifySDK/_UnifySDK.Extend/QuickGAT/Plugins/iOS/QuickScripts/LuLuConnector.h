
//

#import <Foundation/Foundation.h>
#import <JYouLoginKit/REDeLoginKit.h>

@interface LuLuConnector : NSObject <REDeInitCallback, REDeLoginCallback,REDeBuyCallback,REDeRestoreCallback>

+(LuLuConnector *)shareInstance;
- (NSArray *)arrFromJsonStr:(NSString *)string;
- (NSString*)dictionaryToJson:(NSDictionary *)dic;
@end
