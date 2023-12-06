using System;
using System.Runtime.InteropServices;
using com.ultrasdk.unity.Entry;
using com.ultrasdk.unity.Handle;
using com.ultrasdk.unity.SimpleJSON;
using UnityEngine;

namespace com.ultrasdk.unity.Platform
{
    public class UltraiOSPlatform : MonoBehaviour
    {
	    #region debug

	    private bool isDebug = false;
	    public bool IsDebug => isDebug;
	    
	    /// <summary>
	    /// 调试开关
	    /// </summary>
	    /// <param name="debug"></param>
	    internal void SetDebug(bool debug)
	    {
		    isDebug = debug ;
	    }

	    #endregion

	    #region Extra

	    /// <summary>
	    /// 获取登录用户名
	    /// </summary>
	    /// <returns></returns>
	    public string GetUserName()
	    {
		    string userName = string.Empty;
		    
#if UNITY_IOS && !UNITY_EDITOR

			userName = game_getUserName();

#endif
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKGetUserName <=========");
			    Debug.Log(" ===========> userName :" + userName + " <=========");
		    }
		    return userName;
	    }
	    /// <summary>
	    /// 获取登录用户ID
	    /// </summary>
	    /// <returns></returns>
	    public string GetUserId()
	    {
		    string userId = String.Empty;
		    
#if UNITY_IOS && !UNITY_EDITOR

			userId = game_getUserId();

#endif
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKGetUserId <=========");
			    Debug.Log(" ===========> userId :" + userId + " <=========");
		    }
		    return userId;
	    }
	    /// <summary>
	    /// 获取SDK服务器返回的唯一ID
	    /// </summary>
	    /// <returns></returns>
	    public string GetSdkId()
	    {
		    string getSdkId = String.Empty;
		    
#if UNITY_IOS && !UNITY_EDITOR

			getSdkId = game_getSdkId();

#endif
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKGetSdkId <=========");
			    Debug.Log(" ===========> getSdkId :" + getSdkId + " <=========");
		    }
		    return getSdkId;
	    }
	    /// <summary>
	    /// 获取设备号(优先取的IDFA、没取到则取的UUID)
	    /// </summary>
	    /// <returns></returns>
	    public string GetDeviceNum()
	    {
		    string deviceNum = String.Empty;
		    
#if UNITY_IOS && !UNITY_EDITOR

			deviceNum = game_getDeviceNum();

#endif
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKGetDeviceNum <=========");
			    Debug.Log(" ===========> deviceNum :" + deviceNum + " <=========");
		    }
		    return deviceNum ;
	    }

	    /// <summary>
	    /// iOS【退出登陆并显示账号历史界面】
	    /// </summary>
	    public void LogoutAndSowLoginView()
	    {
		    if (isDebug)
		    {
			    Debug.Log(" ===========> LogoutAndSowLoginView <=========");
		    }
#if UNITY_IOS && !UNITY_EDITOR

			game_logoutAndSowLoginView();

#endif
		    
	    }
	    /// <summary>
	    /// iOS【打开国内SDK用户中心】
	    /// </summary>
	    public void ShowUserCenter()
	    {
		    if (isDebug)
		    {
			    Debug.Log(" ===========> ShowUserCenter <=========");
		    }
#if UNITY_IOS && !UNITY_EDITOR

			game_showUserCenter();

#endif
		    
	    }
	    /// <summary>
	    /// iOS上报闪屏
	    /// </summary>
	    public void PostSplashScreenEndSuccess()
	    {
		    if (isDebug)
		    {
			    Debug.Log(" ===========> PostSplashScreenEndSuccess <=========");
		    }
#if UNITY_IOS && !UNITY_EDITOR

			game_postSplashScreenEndSuccess();

#endif
		    
	    }
	    /// <summary>
	    /// 打开内置浏览器显示内容
	    /// </summary>
	    /// <param name="openID">中台配置URL地址的ID</param>
	    public void UltrasdkOpenBrowser(string openID)
	    {
#if UNITY_IOS && !UNITY_EDITOR

			game_openBrowser(openID);

#endif
	        
	    }

	    /// <summary>
	    /// 打开用户中心
	    /// </summary>
	    public void UltrasdkShowAccountCenter()
	    {
#if UNITY_IOS && !UNITY_EDITOR

			game_showAccountCenter();

#endif
	        
	    }

	    /// <summary>
	    /// 账号删除
	    /// 调用此接口会弹出删除账号界面，删除账号后服务器也会删除，请谨慎操作
	    /// </summary>
	    public void UltrasdkAccountCancellation()
	    {
	        
#if UNITY_IOS && !UNITY_EDITOR

			game_accountCancellation();

#endif
	    }

	    /// <summary>
	    /// 解除【账号删除】状态，将账号恢复成正常账号
	    /// </summary>
	    public void UltrasdkAccountUnCancellation()
	    {
	        
#if UNITY_IOS && !UNITY_EDITOR

			game_accountUnCancellation();

#endif
	    }

	    /// <summary>
	    /// 切换账号
	    /// </summary>
	    public void UltrasdkSwitchAccount()
	    {
		    
#if UNITY_IOS && !UNITY_EDITOR

			game_switchAccount();

#endif
	    }
	    
	    
	    #endregion
	    
        #region Interface

        
	    /// <summary>
	    /// 初始化
	    /// </summary>
	    /// <param name="ultraProductId"></param>
	    /// <param name="ultraProductKey"></param>
	    internal void UltrasdkInit(string ultraProductId,string ultraProductKey)
	    {
		    
		    
#if UNITY_IOS && !UNITY_EDITOR

			game_initWithUltraSDK(ultraProductId, ultraProductKey);

#endif
	    }
		
	    /// <summary>
	    /// 登录
	    /// </summary>
	    internal void UltrasdkEnterLoginView()
	    {
#if UNITY_IOS && !UNITY_EDITOR

		    game_enterLoginView();
		    
#endif
	    }

	    /// <summary>
	    /// 退出登录
	    /// </summary>
	    internal void UltrasdkLogout()
	    {
#if UNITY_IOS && !UNITY_EDITOR

		    game_logout();
		    
#endif
	    }
	    
	    /// <summary>
	    /// 内购
	    /// </summary>
	    /// <param name="paymentParametersData"></param>
	    internal void UltrasdkIapPurchaseWithData(UltraPaymentParameters paymentParametersData)
	    {
		    string goodsId = string.IsNullOrEmpty(paymentParametersData.goodsId) ? "" : paymentParametersData.goodsId;
		    string extraParams = string.IsNullOrEmpty(paymentParametersData.extraParams) ? "" : paymentParametersData.extraParams;
		    string cpOrder = string.IsNullOrEmpty(paymentParametersData.cpOrder) ? "" : paymentParametersData.cpOrder;
		    string callbackUrl = string.IsNullOrEmpty(paymentParametersData.callbackUrl) ? "" : paymentParametersData.callbackUrl;

		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKIapPurchaseWithData <=========");
			    Debug.Log(" ===========> goodsId :" + goodsId + " <=========");
			    Debug.Log(" ===========> extraParams :" + extraParams + " <=========");
			    Debug.Log(" ===========> cpOrder :" + cpOrder + " <=========");
			    Debug.Log(" ===========> callbackUrl :" + callbackUrl + " <=========");
		    }
#if UNITY_IOS && !UNITY_EDITOR

		    game_iapPurchaseWithData(goodsId,extraParams,cpOrder,callbackUrl);
#endif
	    }
	    

	    /// <summary>
	    /// 角色登录
	    /// </summary>
	    /// <param name="roleInfo"></param>
	    internal void UltrasdkRoleLoginWithGameRoleInfo(UltraGameRoleInfo roleInfo)
	    {
		    string channelUserId = string.IsNullOrEmpty(roleInfo.channelUserId) ? "" : roleInfo.channelUserId;
            string gameUserId = string.IsNullOrEmpty(roleInfo.gameUserId) ? "" : roleInfo.gameUserId;
            string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
            string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
            string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
            string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;
            string roleAvatar = string.IsNullOrEmpty(roleInfo.roleAvatar) ? "" : roleInfo.roleAvatar;

            string level = string.IsNullOrEmpty(roleInfo.level) ? "" : roleInfo.level;
            string vipLevel = string.IsNullOrEmpty(roleInfo.vipLevel) ? "" : roleInfo.vipLevel;
            string gold1 = string.IsNullOrEmpty(roleInfo.gold1) ? "" : roleInfo.gold1;
            string gold2 = string.IsNullOrEmpty(roleInfo.gold2) ? "" : roleInfo.gold2;
            string sumPay = string.IsNullOrEmpty(roleInfo.sumPay) ? "" : roleInfo.sumPay;
            string levelExp = string.IsNullOrEmpty(roleInfo.levelExp) ? "" : roleInfo.levelExp;
            string vipScore = string.IsNullOrEmpty(roleInfo.vipScore) ? "" : roleInfo.vipScore;
            string rankLevel = string.IsNullOrEmpty(roleInfo.rankLevel) ? "" : roleInfo.rankLevel;
            string rankExp = string.IsNullOrEmpty(roleInfo.rankExp) ? "" : roleInfo.rankExp;
            string rankLeve2 = string.IsNullOrEmpty(roleInfo.rankLeve2) ? "" : roleInfo.rankLeve2;
            string rankExp2 = string.IsNullOrEmpty(roleInfo.rankExp2) ? "" : roleInfo.rankExp2;
            string cupCount1 = string.IsNullOrEmpty(roleInfo.cupCount1) ? "" : roleInfo.cupCount1;
            string cupCount2 = string.IsNullOrEmpty(roleInfo.cupCount2) ? "" : roleInfo.cupCount2;
            string totalKill = string.IsNullOrEmpty(roleInfo.totalKill) ? "" : roleInfo.totalKill;
            string totalHead = string.IsNullOrEmpty(roleInfo.totalHead) ? "" : roleInfo.totalHead;
            string avgKD = string.IsNullOrEmpty(roleInfo.avgKD) ? "" : roleInfo.avgKD;
            string maxKD = string.IsNullOrEmpty(roleInfo.maxKD) ? "" : roleInfo.maxKD;
            string maxCK = string.IsNullOrEmpty(roleInfo.maxCK) ? "" : roleInfo.maxCK;
            string mainWeaponId = string.IsNullOrEmpty(roleInfo.mainWeaponId) ? "" : roleInfo.mainWeaponId;
            string viceWeaponId = string.IsNullOrEmpty(roleInfo.viceWeaponId) ? "" : roleInfo.viceWeaponId;
            string teamId = string.IsNullOrEmpty(roleInfo.teamId) ? "" : roleInfo.teamId;
            string teamName = string.IsNullOrEmpty(roleInfo.teamName) ? "" : roleInfo.teamName;
            
            if (isDebug)
            {
	            Debug.Log(" ===========> UltraSDKRoleLoginWithGameRoleInfo <=========");
	            Debug.Log(" ===========> channelUserId :" + channelUserId + " <=========");
	            Debug.Log(" ===========> gameUserId :" + gameUserId + " <=========");
	            Debug.Log(" ===========> serverId :" + serverId + " <=========");
	            Debug.Log(" ===========> serverName :" + serverName + " <=========");
	            Debug.Log(" ===========> roleId :" + roleId + " <=========");
	            Debug.Log(" ===========> roleName :" + roleName + " <=========");
	            Debug.Log(" ===========> roleAvatar :" + roleAvatar + " <=========");
	            
	            Debug.Log(" ===========> level :" + level + " <=========");
	            Debug.Log(" ===========> vipLevel :" + vipLevel + " <=========");
	            Debug.Log(" ===========> gold1 :" + gold1 + " <=========");
	            Debug.Log(" ===========> gold2 :" + gold2 + " <=========");
	            Debug.Log(" ===========> sumPay :" + sumPay + " <=========");
	            
            }
            
#if UNITY_IOS && !UNITY_EDITOR

            //设置基础参数
            game_setBaseRoleInfoWithData(channelUserId,gameUserId,serverId,serverName,roleId,roleName,roleAvatar);

            //上报角色登录
            game_roleLoginWithGameRoleInfo(level,vipLevel,gold1,gold2,sumPay,levelExp,
	            vipScore,rankLevel,rankExp,rankLeve2,rankExp2,cupCount1,
	            cupCount2,totalKill,totalHead,avgKD,maxKD,maxCK,mainWeaponId,
	            viceWeaponId,teamId,teamName,roleInfo.floatHidden);
		    
#endif
	    }

	    /// <summary>
	    /// 角色注册
	    /// </summary>
	    /// <param name="roleInfo"></param>
	    internal void UltrasdkRoleRegisterWithGameRoleInfo(UltraGameRoleInfo roleInfo)
	    {
		    string channelUserId = string.IsNullOrEmpty(roleInfo.channelUserId) ? "" : roleInfo.channelUserId;
            string gameUserId = string.IsNullOrEmpty(roleInfo.gameUserId) ? "" : roleInfo.gameUserId;
            string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
            string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
            string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
            string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;
            string roleAvatar = string.IsNullOrEmpty(roleInfo.roleAvatar) ? "" : roleInfo.roleAvatar;
            
            string level = string.IsNullOrEmpty(roleInfo.level) ? "" : roleInfo.level;
            string vipLevel = string.IsNullOrEmpty(roleInfo.vipLevel) ? "" : roleInfo.vipLevel;
            string gold1 = string.IsNullOrEmpty(roleInfo.gold1) ? "" : roleInfo.gold1;
            string gold2 = string.IsNullOrEmpty(roleInfo.gold2) ? "" : roleInfo.gold2;
            string sumPay = string.IsNullOrEmpty(roleInfo.sumPay) ? "" : roleInfo.sumPay;
            string levelExp = string.IsNullOrEmpty(roleInfo.levelExp) ? "" : roleInfo.levelExp;
            string vipScore = string.IsNullOrEmpty(roleInfo.vipScore) ? "" : roleInfo.vipScore;
            string rankLevel = string.IsNullOrEmpty(roleInfo.rankLevel) ? "" : roleInfo.rankLevel;
            string rankExp = string.IsNullOrEmpty(roleInfo.rankExp) ? "" : roleInfo.rankExp;
            string rankLeve2 = string.IsNullOrEmpty(roleInfo.rankLeve2) ? "" : roleInfo.rankLeve2;
            string rankExp2 = string.IsNullOrEmpty(roleInfo.rankExp2) ? "" : roleInfo.rankExp2;
            string cupCount1 = string.IsNullOrEmpty(roleInfo.cupCount1) ? "" : roleInfo.cupCount1;
            string cupCount2 = string.IsNullOrEmpty(roleInfo.cupCount2) ? "" : roleInfo.cupCount2;
            string totalKill = string.IsNullOrEmpty(roleInfo.totalKill) ? "" : roleInfo.totalKill;
            string totalHead = string.IsNullOrEmpty(roleInfo.totalHead) ? "" : roleInfo.totalHead;
            string avgKD = string.IsNullOrEmpty(roleInfo.avgKD) ? "" : roleInfo.avgKD;
            string maxKD = string.IsNullOrEmpty(roleInfo.maxKD) ? "" : roleInfo.maxKD;
            string maxCK = string.IsNullOrEmpty(roleInfo.maxCK) ? "" : roleInfo.maxCK;
            string mainWeaponId = string.IsNullOrEmpty(roleInfo.mainWeaponId) ? "" : roleInfo.mainWeaponId;
            string viceWeaponId = string.IsNullOrEmpty(roleInfo.viceWeaponId) ? "" : roleInfo.viceWeaponId;
            string teamId = string.IsNullOrEmpty(roleInfo.teamId) ? "" : roleInfo.teamId;
            string teamName = string.IsNullOrEmpty(roleInfo.teamName) ? "" : roleInfo.teamName;

            if (isDebug)
            {
	            Debug.Log(" ===========> UltraSDKRoleRegisterWithGameRoleInfo <=========");
	            Debug.Log(" ===========> channelUserId :" + channelUserId + " <=========");
	            Debug.Log(" ===========> gameUserId :" + gameUserId + " <=========");
	            Debug.Log(" ===========> serverId :" + serverId + " <=========");
	            Debug.Log(" ===========> serverName :" + serverName + " <=========");
	            Debug.Log(" ===========> roleId :" + roleId + " <=========");
	            Debug.Log(" ===========> roleName :" + roleName + " <=========");
	            Debug.Log(" ===========> roleAvatar :" + roleAvatar + " <=========");
	            
	            Debug.Log(" ===========> level :" + level + " <=========");
	            Debug.Log(" ===========> vipLevel :" + vipLevel + " <=========");
	            Debug.Log(" ===========> gold1 :" + gold1 + " <=========");
	            Debug.Log(" ===========> gold2 :" + gold2 + " <=========");
	            Debug.Log(" ===========> sumPay :" + sumPay + " <=========");
	            
            }
            
#if UNITY_IOS && !UNITY_EDITOR

            //设置基础数据
            game_setBaseRoleInfoWithData(channelUserId,gameUserId,serverId,serverName,roleId,roleName,roleAvatar);
            //上报角色登录
            game_roleRegisterWithGameRoleInfo(level,vipLevel,gold1,gold2,sumPay,levelExp,
	            vipScore,rankLevel,rankExp,rankLeve2,rankExp2,cupCount1,
	            cupCount2,totalKill,totalHead,avgKD,maxKD,maxCK,mainWeaponId,
	            viceWeaponId,teamId,teamName,roleInfo.floatHidden);
		    
#endif
	    }

	    /// <summary>
	    /// 角色升级
	    /// </summary>
	    /// <param name="roleInfo"></param>
	    internal void UltrasdkRoleLevelUpWithGameRoleInfo(UltraGameRoleInfo roleInfo)
	    {
		    string channelUserId = string.IsNullOrEmpty(roleInfo.channelUserId) ? "" : roleInfo.channelUserId;
            string gameUserId = string.IsNullOrEmpty(roleInfo.gameUserId) ? "" : roleInfo.gameUserId;
            string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
            string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
            string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
            string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;
            string roleAvatar = string.IsNullOrEmpty(roleInfo.roleAvatar) ? "" : roleInfo.roleAvatar;

            string level = string.IsNullOrEmpty(roleInfo.level) ? "" : roleInfo.level;
            string vipLevel = string.IsNullOrEmpty(roleInfo.vipLevel) ? "" : roleInfo.vipLevel;
            string gold1 = string.IsNullOrEmpty(roleInfo.gold1) ? "" : roleInfo.gold1;
            string gold2 = string.IsNullOrEmpty(roleInfo.gold2) ? "" : roleInfo.gold2;
            string sumPay = string.IsNullOrEmpty(roleInfo.sumPay) ? "" : roleInfo.sumPay;
            string levelExp = string.IsNullOrEmpty(roleInfo.levelExp) ? "" : roleInfo.levelExp;
            string vipScore = string.IsNullOrEmpty(roleInfo.vipScore) ? "" : roleInfo.vipScore;
            string rankLevel = string.IsNullOrEmpty(roleInfo.rankLevel) ? "" : roleInfo.rankLevel;
            string rankExp = string.IsNullOrEmpty(roleInfo.rankExp) ? "" : roleInfo.rankExp;
            string rankLeve2 = string.IsNullOrEmpty(roleInfo.rankLeve2) ? "" : roleInfo.rankLeve2;
            string rankExp2 = string.IsNullOrEmpty(roleInfo.rankExp2) ? "" : roleInfo.rankExp2;
            string cupCount1 = string.IsNullOrEmpty(roleInfo.cupCount1) ? "" : roleInfo.cupCount1;
            string cupCount2 = string.IsNullOrEmpty(roleInfo.cupCount2) ? "" : roleInfo.cupCount2;
            string totalKill = string.IsNullOrEmpty(roleInfo.totalKill) ? "" : roleInfo.totalKill;
            string totalHead = string.IsNullOrEmpty(roleInfo.totalHead) ? "" : roleInfo.totalHead;
            string avgKD = string.IsNullOrEmpty(roleInfo.avgKD) ? "" : roleInfo.avgKD;
            string maxKD = string.IsNullOrEmpty(roleInfo.maxKD) ? "" : roleInfo.maxKD;
            string maxCK = string.IsNullOrEmpty(roleInfo.maxCK) ? "" : roleInfo.maxCK;
            string mainWeaponId = string.IsNullOrEmpty(roleInfo.mainWeaponId) ? "" : roleInfo.mainWeaponId;
            string viceWeaponId = string.IsNullOrEmpty(roleInfo.viceWeaponId) ? "" : roleInfo.viceWeaponId;
            string teamId = string.IsNullOrEmpty(roleInfo.teamId) ? "" : roleInfo.teamId;
            string teamName = string.IsNullOrEmpty(roleInfo.teamName) ? "" : roleInfo.teamName;

            if (isDebug)
            {
	            Debug.Log(" ===========> UltraSDKRoleLevelUpWithGameRoleInfo <=========");
	            Debug.Log(" ===========> channelUserId :" + channelUserId + " <=========");
	            Debug.Log(" ===========> gameUserId :" + gameUserId + " <=========");
	            Debug.Log(" ===========> serverId :" + serverId + " <=========");
	            Debug.Log(" ===========> serverName :" + serverName + " <=========");
	            Debug.Log(" ===========> roleId :" + roleId + " <=========");
	            Debug.Log(" ===========> roleName :" + roleName + " <=========");
	            Debug.Log(" ===========> roleAvatar :" + roleAvatar + " <=========");
	            
	            Debug.Log(" ===========> level :" + level + " <=========");
	            Debug.Log(" ===========> vipLevel :" + vipLevel + " <=========");
	            Debug.Log(" ===========> gold1 :" + gold1 + " <=========");
	            Debug.Log(" ===========> gold2 :" + gold2 + " <=========");
	            Debug.Log(" ===========> sumPay :" + sumPay + " <=========");
	            
            }
#if UNITY_IOS && !UNITY_EDITOR

			//设置基础参数
            game_setBaseRoleInfoWithData(channelUserId,gameUserId,serverId,serverName,roleId,roleName,roleAvatar);

            //上报角色升级
            game_roleLevelUpWithGameRoleInfo(level,vipLevel,gold1,gold2,sumPay,levelExp,
	            vipScore,rankLevel,rankExp,rankLeve2,rankExp2,cupCount1,
	            cupCount2,totalKill,totalHead,avgKD,maxKD,maxCK,mainWeaponId,
	            viceWeaponId,teamId,teamName,roleInfo.floatHidden);
		    
#endif
	    }

	    /// <summary>
	    /// 分享
	    /// </summary>
	    /// <param name="shareInfo"></param>
	    internal void UltrasdkShare(UltraShareInfo shareInfo)
	    {
		    string title = string.IsNullOrEmpty(shareInfo.title) ? "" : shareInfo.title;
		    string content = string.IsNullOrEmpty(shareInfo.content) ? "" : shareInfo.content;
		    string imgPath = string.IsNullOrEmpty(shareInfo.imgPath) ? "" : shareInfo.imgPath;
		    string url = string.IsNullOrEmpty(shareInfo.url) ? "" : shareInfo.url;
		    string shareTo = "6";
		    if (shareInfo.shareTo == SharePlatform.QQ)
		    {
			    shareTo = "1";
		    }
		    if (shareInfo.shareTo == SharePlatform.QQ_ZONE)
		    {
			    shareTo = "2";
		    }
		    if (shareInfo.shareTo == SharePlatform.WECHAT)
		    {
			    shareTo = "3";
		    }
		    if (shareInfo.shareTo == SharePlatform.WECHAT_MENTS)
		    {
			    shareTo = "4";
		    }
		    if (shareInfo.shareTo == SharePlatform.SINA)
		    {
			    shareTo = "5";
		    }
		    
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKShare <=========");
			    Debug.Log(" ===========> title :" + title + " <=========");
			    Debug.Log(" ===========> content :" + content + " <=========");
			    Debug.Log(" ===========> imgPath :" + imgPath + " <=========");
			    Debug.Log(" ===========> url :" + url + " <=========");
			    Debug.Log(" ===========> shareTo :" + shareTo + " <=========");
		    }
#if UNITY_IOS && !UNITY_EDITOR

		    game_share(shareInfo.hasUi,title,content,imgPath,url,shareTo);
		    
#endif
	    }
	    

	    /// <summary>
	    /// CP上报踢人下线结果
	    /// </summary>
	    /// <param name="result"></param>
	    internal void UltrasdkCpKickOffCallBackWithResult(string result)
	    {
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKCpKickOffCallBackWithResult <=========");
			    Debug.Log(" ===========> exit :" + result + " <=========");
		    }
#if UNITY_IOS && !UNITY_EDITOR

		    game_cpKickOffCallBackWithResult( result);
		    
#endif
	    }

	    /// <summary>
	    /// 获取融合SDK协议内容
	    /// </summary>
	    /// <returns></returns>
	    internal string UltrasdkGetProtocolResult()
	    {
		    string result = String.Empty;
		    
#if UNITY_IOS && !UNITY_EDITOR

				result = game_getProtocolResult();
		    
#endif
		    if (isDebug)
		    {
			    Debug.Log(" ===========> UltraSDKGetProtocolResult <=========");
			    Debug.Log(" ===========> result :" + result + " <=========");
		    }
		    return result ;
	    }

	    /// <summary>
	    /// 设置同意协议
	    /// </summary>
	    internal void UltrasdkSetAgreeProtocol()
	    {
#if UNITY_IOS && !UNITY_EDITOR

		    game_setAgreeProtocol();

#endif
	    }

	    #endregion

        #region OCBrige

#if UNITY_IOS && !UNITY_EDITOR

		[DllImport("__Internal")]
        private static extern void game_initWithUltraSDK(string ultraProductId,string ultraProductKey) ;
        [DllImport("__Internal")]
        private static extern void game_enterLoginView();
        [DllImport("__Internal")]
        private static extern void game_logout();
        [DllImport("__Internal")]
        private static extern void game_logoutAndSowLoginView();
        [DllImport("__Internal")]
        private static extern void game_showUserCenter();
        [DllImport("__Internal")]
        private static extern void game_cleanUserEntities();
        [DllImport("__Internal")]
        private static extern string game_getUserName();
        [DllImport("__Internal")]
        private static extern string game_getUserId();
        [DllImport("__Internal")]
        private static extern string game_getSdkId();
        [DllImport("__Internal")]
        private static extern string game_getDeviceNum();
        [DllImport("__Internal")]
        private static extern void game_iapPurchaseWithData(string gamePropID,string gameRole,string cpOrder,string callbackUrl) ;
        [DllImport("__Internal")]
        private static extern void game_setBaseRoleInfoWithData(string channelUserId,string gameUserId,string serverId,
	        string serverName,string roleId,string roleName, string roleAvatar);
        [DllImport("__Internal")]
        private static extern void game_roleLoginWithGameRoleInfo(string level,string vipLevel,string gold1,
	        string gold2,string sumPay,string levelExp,
	        string vipScore,string rankLevel,string rankExp,
	        string rankLeve2,string rankExp2,string cupCount1,
	        string cupCount2,string totalKill,string totalHead,
	        string avgKD,string maxKD,string maxCK,string mainWeaponId,
	        string viceWeaponId,string teamId,
	        string teamName,bool floatHidden);
        [DllImport("__Internal")]
        private static extern void game_roleRegisterWithGameRoleInfo(string level,string vipLevel,string gold1,
	        string gold2,string sumPay,string levelExp,
	        string vipScore,string rankLevel,string rankExp,
	        string rankLeve2,string rankExp2,string cupCount1,
	        string cupCount2,string totalKill,string totalHead,
	        string avgKD,string maxKD,string maxCK,string mainWeaponId,
	        string viceWeaponId,string teamId,
	        string teamName,bool floatHidden);
        [DllImport("__Internal")]
        private static extern void game_roleLevelUpWithGameRoleInfo(string level,string vipLevel,string gold1,
	        string gold2,string sumPay,string levelExp,
	        string vipScore,string rankLevel,string rankExp,
	        string rankLeve2,string rankExp2,string cupCount1,
	        string cupCount2,string totalKill,string totalHead,
	        string avgKD,string maxKD,string maxCK,string mainWeaponId,
	        string viceWeaponId,string teamId,
	        string teamName,bool floatHidden);

        [DllImport("__Internal")]
        private static extern void game_share(bool hasUi,string title,string content,string imagePath,string url,string shareTo);

        [DllImport("__Internal")]                                        
        private static extern void game_postSplashScreenEndSuccess();
        [DllImport("__Internal")]
        private static extern void game_cpKickOffCallBackWithResult(string result) ;
        [DllImport("__Internal")]
        private static extern string game_getProtocolResult() ;
        [DllImport("__Internal")]
        private static extern void game_setAgreeProtocol() ;
        
        [DllImport("__Internal")]
        private static extern void game_openBrowser(string openID) ;
        
        [DllImport("__Internal")]
        private static extern void game_showAccountCenter() ;
        [DllImport("__Internal")]
        private static extern void game_accountCancellation() ;
        [DllImport("__Internal")]
        private static extern void game_accountUnCancellation() ;
        [DllImport("__Internal")]
        private static extern void game_switchAccount() ;

#endif
        

        #endregion

        #region CallBack

        /*
         * 登录成功
         * **/
        public void onLoginSuccess(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        UltraUserInfo userInfo = new UltraUserInfo();
	        userInfo.plat = "ios";
	        userInfo.accessCode = data["accessCode"].Value;
	        userInfo.accessToken = data["accessToken"].Value;
	        userInfo.sdkUserId = data["sdkuserid"].Value;
	        userInfo.userName = data["username"].Value;
	        //回调
	        UltraLoginDelegateProxy.ONLoginDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,userInfo,"登陆成功");
        }
        /*
         * 登录失败
         * **/
        public void onLoginFailed(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        var errorMsg = data["msg"].Value;
	        //回调
	        UltraLoginDelegateProxy.ONLoginDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed,null,errorMsg);
        }
	    /// <summary>
	    /// 登录失效
	    /// </summary>
	    /// <param name="msg"></param>
        public void onReLoginFromInvalid(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        var errMsg = data["msg"].Value;
	        //回调
	        UltraLogoutDelegateProxy.ONLoginInvalidDelegateProxy?.Invoke(UltraSDKLoginInvalidResult.UltraSDKLoginInvalidServer,errMsg);
        }
        /*
         * 登录失效
         * **/
        public void onLogonInvalid(string msg){
	        var data = JSONNode.Parse(msg);
	        var errMsg = data["msg"].Value;
	        //回调
	        UltraLogoutDelegateProxy.ONLoginInvalidDelegateProxy?.Invoke(UltraSDKLoginInvalidResult.UltraSDKLoginInvalidKick,errMsg);
        }
        /*
         * 切换账号成功
         * **/
        public void onSwitchAccountSuccess(string msg)
        {
	        UltraUserInfo userInfo = new UltraUserInfo();
	        userInfo.plat = "ios";
	        //回调
	        UltraSwitchAccDelegateProxy.ONSwitchAccDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, userInfo, null);
        }
        
        /*
         * 注销登录成功
         * **/
        public void onLogoutSuccess() {
	        //回调
	        UltraLogoutDelegateProxy.ONLogutDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,null);
        }
        /*
         * 支付成功
         * **/
        public void onPaySuccess(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        UltraPaymentOrder result = new UltraPaymentOrder();
	        result.plat = "ios";
	        result.sdkOrderId = data["orderId"].Value;
	        result.orderAmount = data["orderAmount"].Value;
	        result.currency = data["currency"].Value;
	        //回调
	        UltraPayDelegateProxy.ONPayDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,result,null);
        }
        /*
         * 支付失败
         * **/
        public void onPayFailed(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        var errorMsg = data["msg"].Value;
	        //回调
	        UltraPayDelegateProxy.ONPayDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed,null,errorMsg);
        }
        /*
         * 分享失败
         * **/
        public void onShareFailedAction(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        string tmsg = data["msg"].Value;
	        //回调
	        UltraShareDelegateProxy.ONShareDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed,null, tmsg);
        }
        /*
         * 分享成功
         * **/
        public void onShareSuccessdAction(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        string shareType = data["shareType"].Value;
	        //回调
	        UltraShareDelegateProxy.ONShareDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,shareType,"分享成功");
        }
        /*
         * 融合初始化成功
         * **/
        public void onInitSuccess(string msg)
        {
	        Debug.Log(" ===========> onInitSuccess <=========");
	        //回调
	        UltraInitDelegateProxy.ONInitDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,"初始化成功");
        }
        /*
         * 同意协议回调
         * **/
        public void onClickProtocol(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        //回调
	        UltraInitDelegateProxy.ONProtocolAgree?.Invoke();
        }

        /// <summary>
        /// 撤回注销成功回调
        /// </summary>
        /// <param name="msg"></param>
        public void onAccountUnCancellationSuccess(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        string tmsg = data["msg"].Value;
	        UltraAccountUnCancellationDelegateProxy.ONAccountUnCancellationDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,tmsg);
        }

        /// <summary>
        /// 撤回账号注销失败回调
        /// </summary>
        /// <param name="msg"></param>
        public void onAccountUnCancellationFail(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        string tmsg = data["msg"].Value; 
	        UltraAccountUnCancellationDelegateProxy.ONAccountUnCancellationDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed,tmsg);
        }

        /// <summary>
        /// 打开内置浏览器成功回调	
        /// </summary>
        /// <param name="msg"></param>
        public void onOpenBrowserSucccess(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        string tmsg = data["msg"].Value; 
	        UltraOpenBrowerDelegateProxy.ONOpenBrowerDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess,tmsg);
        }

        /// <summary>
        /// 打开内置浏览器失败
        /// </summary>
        /// <param name="msg"></param>
        public void onOpenBrowserFailed(string msg)
        {
	        var data = JSONNode.Parse(msg);
	        string tmsg = data["msg"].Value; 
	        UltraOpenBrowerDelegateProxy.ONOpenBrowerDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed,tmsg);
        }

        #endregion
        
    }
}