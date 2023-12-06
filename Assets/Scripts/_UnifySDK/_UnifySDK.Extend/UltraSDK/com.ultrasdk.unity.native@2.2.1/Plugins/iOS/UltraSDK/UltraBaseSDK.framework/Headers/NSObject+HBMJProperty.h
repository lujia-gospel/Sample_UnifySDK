//
//  NSObject+HBMJProperty.h
//  HBMJExtensionExample
//
//  Created by MJ Lee on 15/4/17.
//  Copyright (c) 2015年 小码哥. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UltraBaseSDK/HBMJExtensionConst.h>

@class HBMJProperty;

/**
 *  遍历成员变量用的block
 *
 *  @param property 成员的包装对象
 *  @param stop   YES代表停止遍历，NO代表继续遍历
 */
typedef void (^MJPropertiesEnumeration)(HBMJProperty *property, BOOL *stop);

/** 将属性名换为其他key去字典中取值 */
typedef NSDictionary * (^MJReplacedKeyFromPropertyName)(void);
typedef id (^MJReplacedKeyFromPropertyName121)(NSString *propertyName);
/** 数组中需要转换的模型类 */
typedef NSDictionary * (^MJObjectClassInArray)(void);
/** 用于过滤字典中的值 */
typedef id (^MJNewValueFromOldValue)(id object, id oldValue, HBMJProperty *property);

/**
 * 成员属性相关的扩展
 */
@interface NSObject (HBMJProperty)
#pragma mark - 遍历
/**
 *  遍历所有的成员
 */
+ (void)hb_enumerateProperties:(MJPropertiesEnumeration)enumeration;

#pragma mark - 新值配置
/**
 *  用于过滤字典中的值
 *
 *  @param newValueFormOldValue 用于过滤字典中的值
 */
+ (void)hb_setupNewValueFromOldValue:(MJNewValueFromOldValue)newValueFormOldValue;
+ (id)hb_getNewValueFromObject:(__unsafe_unretained id)object oldValue:(__unsafe_unretained id)oldValue property:(__unsafe_unretained HBMJProperty *)property;

#pragma mark - key配置
/**
 *  将属性名换为其他key去字典中取值
 *
 *  @param replacedKeyFromPropertyName 将属性名换为其他key去字典中取值
 */
+ (void)hb_setupReplacedKeyFromPropertyName:(MJReplacedKeyFromPropertyName)replacedKeyFromPropertyName;
/**
 *  将属性名换为其他key去字典中取值
 *
 *  @param replacedKeyFromPropertyName121 将属性名换为其他key去字典中取值
 */
+ (void)hb_setupReplacedKeyFromPropertyName121:(MJReplacedKeyFromPropertyName121)replacedKeyFromPropertyName121;

#pragma mark - array model class配置
/**
 *  数组中需要转换的模型类
 *
 *  @param objectClassInArray          数组中需要转换的模型类
 */
+ (void)hb_setupObjectClassInArray:(MJObjectClassInArray)objectClassInArray;
@end
