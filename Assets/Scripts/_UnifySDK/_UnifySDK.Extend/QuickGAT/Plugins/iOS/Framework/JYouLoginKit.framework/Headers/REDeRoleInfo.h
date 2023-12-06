//
//

#import <Foundation/Foundation.h>

@interface REDeRoleInfo : NSObject
@property (copy,nonatomic) NSString *server_name;       //区服名称
@property (copy,nonatomic) NSString *server_id;         //区服ID 必填
@property (copy,nonatomic) NSString *vip_level;         //vip等级
@property (copy,nonatomic) NSString *game_role_name;    //角色名称 必填
@property (copy,nonatomic) NSString *game_role_id;      //角色id 必填
@property (copy,nonatomic) NSString *game_role_level;   //角色等级

- (void)setValuesFromRoleInfo:(REDeRoleInfo *)info;
@end
