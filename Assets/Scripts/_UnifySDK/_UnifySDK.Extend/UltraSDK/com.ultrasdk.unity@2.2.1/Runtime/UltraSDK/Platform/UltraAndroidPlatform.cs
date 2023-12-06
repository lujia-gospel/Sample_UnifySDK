using System;
using com.ultrasdk.unity.Entry;
using com.ultrasdk.unity.Handle;
using com.ultrasdk.unity.SimpleJSON;
using UnityEngine;

namespace com.ultrasdk.unity.Platform
{
    public class UltraAndroidPlatform : MonoBehaviour
    {
        private AndroidJavaObject _androidJavaObject;
        private static UltraAndroidPlatform instance;


        #region debug

        private bool isDebug = false;
        public bool IsDebug => isDebug;

        internal void SetDebug(bool debug)
        {
            isDebug = debug;
        }

        #endregion


        public void OnInitAndroidClzz()
        {
            AndroidJavaClass ac = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _androidJavaObject = ac.GetStatic<AndroidJavaObject>("currentActivity");
        }

        #region Extra

        /// <summary>
        /// ![必接]监听退出游戏回调观察[添加]
        /// </summary>
        /// <param name="onExitDelegateProxy"></param>
        public void AddExitListener(UltraExitDelegateProxy.ExitDelegateProxy onExitDelegateProxy)
        {
            UltraExitDelegateProxy.ONExitDelegateProxy = onExitDelegateProxy;
        }

        /// <summary>
        /// ![必接]监听退出游戏回调观察[移除]
        /// </summary>
        /// <param name="onExitDelegateProxy"></param>
        public void RemoveExitListener(UltraExitDelegateProxy.ExitDelegateProxy onExitDelegateProxy)
        {
            UltraExitDelegateProxy.ONExitDelegateProxy = null;
        }

        /// <summary>
        /// 渠道ID
        /// </summary>
        /// <returns></returns>
        public int GetChannelId()
        {
            return _androidJavaObject.Call<int>("requestUltraSdkGetChannelId");
        }

        /// <summary>
        /// 渠道名称
        /// </summary>
        /// <returns></returns>
        public string GetChannelName()
        {
            return _androidJavaObject.Call<string>("requestUltraSdkGetChannelName");
        }

        /// <summary>
        /// 获取渠道SDK的版本名
        /// </summary>
        /// <returns></returns>
        public string GetChannelSdkVersionName()
        {
            return _androidJavaObject.Call<string>("requestUltraSdkGetChannelSdkVersionName");
        }

        /// <summary>
        /// 判断当前渠道在调用退出接口时是否会弹出退出框
        /// </summary>
        /// <returns></returns>
        public bool IsChannelHasExitDialog()
        {
            return _androidJavaObject.Call<bool>("requestUltraSdkisChannelHasExitDialog");
        }

        /// <summary>
        /// 获取英雄官网渠道的ProjectId，常用于游戏需要CPS分包时
        /// </summary>
        /// <returns></returns>
        public string GetProjectId()
        {
            return _androidJavaObject.Call<string>("requestUltraSdkGetProjectId");
        }

        /// <summary>
        /// 获取用户在UltraSDK后台配置的自定义参数值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCustomParams(string key)
        {
            return _androidJavaObject.Call<string>("requestUltraSdkGetCustomParams", key);
        }

        /// <summary>
        /// 调用渠道的扩展方法(如显示/隐藏悬浮框，防沉迷查询，进入用户中心等)。
        /// </summary>
        /// <param name="extendType"></param>
        /// <returns></returns>
        public bool CallExtendApi(int extendType)
        {
            return _androidJavaObject.Call<bool>("requestUltraSdkcallExtendApi", extendType);
        }

        /// <summary>
        /// 获取当前设备的OAID值
        /// /// <param name="onGetOaidDelegateProxy"><see cref="UltraGetOAIDDelegateProxy.GetOAIDDelegateProxy"/>结果回调</param>
        /// </summary>
        public void GetOAID(UltraGetOAIDDelegateProxy.GetOAIDDelegateProxy onGetOaidDelegateProxy)
        {
            UltraGetOAIDDelegateProxy.ONGetOAIDDelegateProxy = onGetOaidDelegateProxy;
            _androidJavaObject.Call("requestUltraSdkGetOAID");
        }

        /// <summary>
        /// Android端物理返回键【退出游戏】
        /// </summary>
        /// <param name="onExitDelegateProxy"><see cref="UltraExitDelegateProxy.ExitDelegateProxy"/>退出回调</param>
        public void Exit(UltraExitDelegateProxy.ExitDelegateProxy onExitDelegateProxy)
        {
            UltraExitDelegateProxy.ONExitDelegateProxy = onExitDelegateProxy;
            _androidJavaObject.Call("requestUltraSdkExit");
        }

        /// <summary>
        /// banner广告
        /// </summary>
        /// <param name="bannerID">广告ID</param>
        /// <param name="onAdPlayDelegateProxy"><see cref="UltraAdPlayDelegateProxy.AdPlayDelegateProxy"/>广告播放回调</param>
        public void ShowAdBanner(string bannerID, UltraAdPlayDelegateProxy.AdPlayDelegateProxy onAdPlayDelegateProxy)
        {
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy = onAdPlayDelegateProxy;
            _androidJavaObject.Call("requestShowAdBanner", bannerID);
        }

        /// <summary>
        /// 插屏广告
        /// </summary>
        /// <param name="interstialBannerID">广告ID</param>
        /// <param name="onAdPlayDelegateProxy"><see cref="UltraAdPlayDelegateProxy.AdPlayDelegateProxy"/>广告播放回调</param>
        public void ShowAdInterstialBanner(string interstialBannerID,
            UltraAdPlayDelegateProxy.AdPlayDelegateProxy onAdPlayDelegateProxy)
        {
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy = onAdPlayDelegateProxy;
            _androidJavaObject.Call("requestShowAdInterstialBanner", interstialBannerID);
        }

        /// <summary>
        /// 视频广告
        /// </summary>
        /// <param name="videoID">广告ID</param>
        /// <param name="onAdPlayDelegateProxy"><see cref="UltraAdPlayDelegateProxy.AdPlayDelegateProxy"/>广告播放回调</param>
        public void ShowAdVideo(string videoID, UltraAdPlayDelegateProxy.AdPlayDelegateProxy onAdPlayDelegateProxy)
        {
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy = onAdPlayDelegateProxy;
            _androidJavaObject.Call("requestShowAdVideo", videoID);
        }

        /// <summary>
        /// 隐藏横幅广告，很少使用
        /// </summary>
        public void HideAdBanner()
        {
            _androidJavaObject.Call("requestHideAdBanner");
        }

        /// <summary>
        /// 打开内置浏览器
        /// </summary>
        /// <param name="openID"></param>
        public void UltrasdkOpenBrowser(string openID)
        {
            _androidJavaObject.Call("requestUltraSdkOpenBrowser", openID);
        }

        /// <summary>
        /// 打开账号中心
        /// </summary>
        public void UltrasdkShowAccountCenter()
        {
            _androidJavaObject.Call("requestUltraSdkShowAccountCenter");
        }

        /// <summary>
        /// 账号注销
        /// </summary>
        public void UltrasdkAccountCancellation()
        {
            _androidJavaObject.Call("requestUltraSdkAccountCancellation");
        }

        /// <summary>
        /// 撤回账户注销
        /// </summary>
        public void UltrasdkAccountUnCancellation()
        {
            _androidJavaObject.Call("requestUltraSdkAccountUnCancellation");
        }

        /// <summary>
        /// 上报日志
        /// </summary>
        public void UltrasdkUploadIssue()
        {
            _androidJavaObject.Call("requestUltraSdkUploadIssue");
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        public void UltrasdkSwitchAccount()
        {
            _androidJavaObject.Call("requestUltraSdkSwitchAccount");
        }

        #endregion

        #region AndroidBridge

        /*
         * 初始化融合SDK
         * **/
        internal void UltrasdkInit(string productId, string productKey)
        {
            _androidJavaObject.Call("requestUltraSdkInit", productId, productKey);
        }

        /*
         * 有界面登录
         * **/
        internal void UltrasdkEnterLoginView()
        {
            _androidJavaObject.Call("requestUltraSdkEnterLoginView");
        }

        /*
         * 创建角色
         * **/
        internal void UltrasdkRoleRegisterWithGameRoleInfo(UltraGameRoleInfo roleInfo)
        {
            if (isDebug)
            {
                string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
                string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
                string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
                string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;

                string level = string.IsNullOrEmpty(roleInfo.level) ? "" : roleInfo.level;
                string vipLevel = string.IsNullOrEmpty(roleInfo.vipLevel) ? "" : roleInfo.vipLevel;
                string gold1 = string.IsNullOrEmpty(roleInfo.gold1) ? "" : roleInfo.gold1;
                string gold2 = string.IsNullOrEmpty(roleInfo.gold2) ? "" : roleInfo.gold2;
                string sumPay = string.IsNullOrEmpty(roleInfo.sumPay) ? "" : roleInfo.sumPay;

                Debug.Log(" ===========> UltraSDKRoleRegisterWithGameRoleInfo <=========");
                Debug.Log(" ===========> serverId :" + serverId + " <=========");
                Debug.Log(" ===========> serverName :" + serverName + " <=========");
                Debug.Log(" ===========> roleId :" + roleId + " <=========");
                Debug.Log(" ===========> roleName :" + roleName + " <=========");

                Debug.Log(" ===========> level :" + level + " <=========");
                Debug.Log(" ===========> vipLevel :" + vipLevel + " <=========");
                Debug.Log(" ===========> gold1 :" + gold1 + " <=========");
                Debug.Log(" ===========> gold2 :" + gold2 + " <=========");
                Debug.Log(" ===========> sumPay :" + sumPay + " <=========");
            }

            _androidJavaObject.Call("requestUltraSdkCreateRole", roleInfo.serverId, roleInfo.serverName,
                roleInfo.roleId, roleInfo.roleName,
                roleInfo.level, roleInfo.vipLevel, roleInfo.partyName, roleInfo.roleCreateTime,
                roleInfo.balanceLevelOne, roleInfo.balanceLevelTwo, roleInfo.sumPay, roleInfo.partyId,
                roleInfo.roleGender, roleInfo.rolePower, roleInfo.partyRoleId, roleInfo.partyRoleName,
                roleInfo.professionId, roleInfo.profession, roleInfo.friendList);
        }

        /*
         * 进入游戏
         * **/
        internal void UltrasdkRoleLoginWithGameRoleInfo(UltraGameRoleInfo roleInfo)
        {
            if (isDebug)
            {
                string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
                string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
                string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
                string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;

                string level = string.IsNullOrEmpty(roleInfo.level) ? "" : roleInfo.level;
                string vipLevel = string.IsNullOrEmpty(roleInfo.vipLevel) ? "" : roleInfo.vipLevel;
                string gold1 = string.IsNullOrEmpty(roleInfo.gold1) ? "" : roleInfo.gold1;
                string gold2 = string.IsNullOrEmpty(roleInfo.gold2) ? "" : roleInfo.gold2;
                string sumPay = string.IsNullOrEmpty(roleInfo.sumPay) ? "" : roleInfo.sumPay;

                Debug.Log(" ===========> UltraSDKRoleLoginWithGameRoleInfo <=========");
                Debug.Log(" ===========> serverId :" + serverId + " <=========");
                Debug.Log(" ===========> serverName :" + serverName + " <=========");
                Debug.Log(" ===========> roleId :" + roleId + " <=========");
                Debug.Log(" ===========> roleName :" + roleName + " <=========");

                Debug.Log(" ===========> level :" + level + " <=========");
                Debug.Log(" ===========> vipLevel :" + vipLevel + " <=========");
                Debug.Log(" ===========> gold1 :" + gold1 + " <=========");
                Debug.Log(" ===========> gold2 :" + gold2 + " <=========");
                Debug.Log(" ===========> sumPay :" + sumPay + " <=========");
            }

            _androidJavaObject.Call("requestUltraSdkEnterGame", roleInfo.serverId, roleInfo.serverName, roleInfo.roleId,
                roleInfo.roleName,
                roleInfo.level, roleInfo.vipLevel, roleInfo.partyName, roleInfo.roleCreateTime,
                roleInfo.balanceLevelOne, roleInfo.balanceLevelTwo, roleInfo.sumPay, roleInfo.partyId,
                roleInfo.roleGender, roleInfo.rolePower, roleInfo.partyRoleId, roleInfo.partyRoleName,
                roleInfo.professionId, roleInfo.profession, roleInfo.friendList);
        }

        /*
         * 角色升级
         * **/
        internal void UltrasdkRoleLevelUpWithGameRoleInfo(UltraGameRoleInfo roleInfo)
        {
            if (isDebug)
            {
                string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
                string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
                string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
                string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;

                string level = string.IsNullOrEmpty(roleInfo.level) ? "" : roleInfo.level;
                string vipLevel = string.IsNullOrEmpty(roleInfo.vipLevel) ? "" : roleInfo.vipLevel;
                string gold1 = string.IsNullOrEmpty(roleInfo.gold1) ? "" : roleInfo.gold1;
                string gold2 = string.IsNullOrEmpty(roleInfo.gold2) ? "" : roleInfo.gold2;
                string sumPay = string.IsNullOrEmpty(roleInfo.sumPay) ? "" : roleInfo.sumPay;

                Debug.Log(" ===========> UltraSDKRoleLevelUpWithGameRoleInfo <=========");
                Debug.Log(" ===========> serverId :" + serverId + " <=========");
                Debug.Log(" ===========> serverName :" + serverName + " <=========");
                Debug.Log(" ===========> roleId :" + roleId + " <=========");
                Debug.Log(" ===========> roleName :" + roleName + " <=========");

                Debug.Log(" ===========> level :" + level + " <=========");
                Debug.Log(" ===========> vipLevel :" + vipLevel + " <=========");
                Debug.Log(" ===========> gold1 :" + gold1 + " <=========");
                Debug.Log(" ===========> gold2 :" + gold2 + " <=========");
                Debug.Log(" ===========> sumPay :" + sumPay + " <=========");
            }

            _androidJavaObject.Call("requestUltraSdkLevelUp", roleInfo.serverId, roleInfo.serverName, roleInfo.roleId,
                roleInfo.roleName,
                roleInfo.level, roleInfo.vipLevel, roleInfo.partyName, roleInfo.roleCreateTime,
                roleInfo.balanceLevelOne, roleInfo.balanceLevelTwo, roleInfo.sumPay, roleInfo.partyId,
                roleInfo.roleGender, roleInfo.rolePower, roleInfo.partyRoleId, roleInfo.partyRoleName,
                roleInfo.professionId, roleInfo.profession, roleInfo.friendList);
        }

        /*
         * 注销登录
         * **/
        internal void UltrasdkLogout()
        {
            _androidJavaObject.Call("requestUltraSdkLogout");
        }

        /*
         * 退出
         * **/
        internal void requestUltraSdkExit()
        {
            _androidJavaObject.Call("requestUltraSdkExit");
        }

        /*
         * 支付
         * **/
        internal void UltrasdkIapPurchaseWithData(UltraGameRoleInfo roleInfo, UltraPaymentParameters paymentParameters)
        {
            if (isDebug)
            {
                string serverId = string.IsNullOrEmpty(roleInfo.serverId) ? "" : roleInfo.serverId;
                string serverName = string.IsNullOrEmpty(roleInfo.serverName) ? "" : roleInfo.serverName;
                string roleId = string.IsNullOrEmpty(roleInfo.roleId) ? "" : roleInfo.roleId;
                string roleName = string.IsNullOrEmpty(roleInfo.roleName) ? "" : roleInfo.roleName;
                Debug.Log(" ===========> UltraSDKRoleLevelUpWithGameRoleInfo <=========");
                Debug.Log(" ===========> serverId :" + serverId + " <=========");
                Debug.Log(" ===========> serverName :" + serverName + " <=========");
                Debug.Log(" ===========> roleId :" + roleId + " <=========");
                Debug.Log(" ===========> roleName :" + roleName + " <=========");

                string goodsId = string.IsNullOrEmpty(paymentParameters.goodsId) ? "" : paymentParameters.goodsId;
                string extraParams = string.IsNullOrEmpty(paymentParameters.extraParams)
                    ? ""
                    : paymentParameters.extraParams;
                string cpOrder = string.IsNullOrEmpty(paymentParameters.cpOrder) ? "" : paymentParameters.cpOrder;
                string callbackUrl = string.IsNullOrEmpty(paymentParameters.callbackUrl)
                    ? ""
                    : paymentParameters.callbackUrl;
                Debug.Log(" ===========> goodsId :" + goodsId + " <=========");
                Debug.Log(" ===========> extraParams :" + extraParams + " <=========");
                Debug.Log(" ===========> cpOrder :" + cpOrder + " <=========");
                Debug.Log(" ===========> callbackUrl :" + callbackUrl + " <=========");
            }

            _androidJavaObject.Call("requestUltraSdkPay", paymentParameters.goodsId, paymentParameters.extraParams,
                paymentParameters.cpOrder, paymentParameters.callbackUrl,
                roleInfo.serverId, roleInfo.serverName, roleInfo.roleId, roleInfo.roleName,
                roleInfo.level, roleInfo.vipLevel, roleInfo.partyName, roleInfo.roleCreateTime,
                roleInfo.balanceLevelOne, roleInfo.balanceLevelTwo, roleInfo.sumPay, roleInfo.partyId,
                roleInfo.roleGender, roleInfo.rolePower, roleInfo.partyRoleId, roleInfo.partyRoleName,
                roleInfo.professionId, roleInfo.profession, roleInfo.friendList);
        }

        /*
         * 上报踢玩家下线结果
         * **/
        internal void UltrasdkCpKickOffCallBackWithResult(string result)
        {
            if (isDebug)
            {
                Debug.Log(" ===========> UltraSDKCpKickOffCallBackWithResult <=========");
                Debug.Log(" ===========> exit :" + result + " <=========");
            }

            _androidJavaObject.Call("requestUltraSdkNotifyKickResult", result);
        }

        /*
         * 分享
         * **/
        internal void UltrasdkShare(UltraShareInfo shareInfo)
        {
            if (isDebug)
            {
                Debug.Log(" ===========> UltraSDKShare <=========");
                Debug.Log(" ===========> title :" + shareInfo.title + " <=========");
                Debug.Log(" ===========> content :" + shareInfo.content + " <=========");
                Debug.Log(" ===========> imgPath :" + shareInfo.imgPath + " <=========");
                Debug.Log(" ===========> url :" + shareInfo.url + " <=========");
                Debug.Log(" ===========> shareTo :" + shareInfo.shareTo + " <=========");
            }

            _androidJavaObject.Call("requestShare", shareInfo.hasUi, shareInfo.title, shareInfo.content,
                shareInfo.imgPath,
                shareInfo.imgUrl, shareInfo.url, shareInfo.shareTo, shareInfo.extenal);
        }

        /*
		 * 获取协议内容
		 * **/
        internal string UltrasdkGetProtocolResult()
        {
            string result = _androidJavaObject.Call<string>("requestUltraSdkGetProtocolResult");
            if (isDebug)
            {
                Debug.Log(" ===========> UltraSDKGetProtocolResult <=========");
                Debug.Log(" ===========> result :" + result + " <=========");
            }

            return result;
        }

        /*
		 * 点击同意通知
		 * **/
        internal void UltrasdkSetAgreeProtocol()
        {
            _androidJavaObject.Call("requestUltraSdkSetAgreeProtocol");
        }

        #endregion

        #region CallBack

        /*
         * 融合初始化成功
         * **/
        public void onInitSuccess()
        {
            //回调
            UltraInitDelegateProxy.ONInitDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, "初始化成功");
        }

        /*
         * 融合初始化失败
         * **/
        public void onInitFaliure(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraInitDelegateProxy.ONInitDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, errorMsg);
        }

        /*
         * 登录成功
         * **/
        public void onLoginSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string plat = data["plat"].Value;
            UltraUserInfo userInfo = new UltraUserInfo();
            userInfo.plat = plat;
            userInfo.channelToken = data["channelToken"].Value;
            userInfo.extendParams = data["extendParams"].Value;
            userInfo.isFristLogin = data["isFristLogin"].Value;
            userInfo.serverMessage = data["serverMessage"].Value;
            userInfo.accessToken = data["accessToken"].Value;
            userInfo.sdkUserId = data["sdkUserId"].Value;
            userInfo.userName = data["userName"].Value;
            //回调
            UltraLoginDelegateProxy.ONLoginDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, userInfo,
                "登陆成功");
        }

        /*
         * 取消登录
         * **/
        public void onLoginCancel()
        {
            //回调
            UltraLoginDelegateProxy.ONLoginDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultCancel, null, "取消登陆");
        }

        /*
         * 登录失败
         * **/
        public void onLoginFailed(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraLoginDelegateProxy.ONLoginDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, null, errorMsg);
        }

        /*
         * 登录失效
         * **/
        public void onLogonInvalid(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errMsg = data["msg"].Value;
            //回调
            UltraLogoutDelegateProxy.ONLoginInvalidDelegateProxy?.Invoke(
                UltraSDKLoginInvalidResult.UltraSDKLoginInvalidKick, errMsg);
        }

        /*
         * 切换账号成功
         * **/
        public void onSwitchAccountSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string plat = data["plat"].Value;
            UltraUserInfo userInfo = new UltraUserInfo();
            userInfo.plat = plat;
            userInfo.channelToken = data["channelToken"].Value;
            userInfo.extendParams = data["extendParams"].Value;
            userInfo.isFristLogin = data["isFristLogin"].Value;
            userInfo.serverMessage = data["serverMessage"].Value;
            userInfo.accessToken = data["accessToken"].Value;
            userInfo.sdkUserId = data["sdkUserId"].Value;
            userInfo.userName = data["userName"].Value;
            //回调
            UltraSwitchAccDelegateProxy.ONSwitchAccDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, userInfo,
                null);
        }

        /*
         * 切换账号后失败
         * **/
        public void onSwtichAccountFaliure(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraSwitchAccDelegateProxy.ONSwitchAccDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, null,
                errorMsg);
        }

        /*
         * 切换账号取消
         * **/
        public void onSwitchAccountCancel()
        {
            //回调
            UltraSwitchAccDelegateProxy.ONSwitchAccDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultCancel, null,
                "取消切换账号");
        }

        /*
         * 注销登录成功
         * **/
        public void onLogoutSuccess()
        {
            //回调
            UltraLogoutDelegateProxy.ONLogutDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, null);
        }

        /*
         * 注销登录失败
         * **/
        public void onLogoutFailure(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraLogoutDelegateProxy.ONLogutDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, errorMsg);
        }

        /*
         * 退出成功
         * **/
        public void onExitSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraExitDelegateProxy.ONExitDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, errorMsg);
        }

        /*
         * 退出失败
         * **/
        public void onExitFailure(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraExitDelegateProxy.ONExitDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, errorMsg);
        }

        /*
         * 支付成功
         * **/
        public void onPaySuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            UltraPaymentOrder result = new UltraPaymentOrder();
            result.plat = "android";
            result.sdkOrderId = data["sdkOrderId"].Value;
            result.cpOrderId = data["cpOrderId"].Value;
            result.extraParams = data["extraParams"].Value;
            //回调
            UltraPayDelegateProxy.ONPayDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, result, null);
        }

        /*
         * 支付失败
         * **/
        public void onPayFailed(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraPayDelegateProxy.ONPayDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, null, errorMsg);
        }

        /*
         * 支付取消，Android
         * **/
        public void onPayCancel(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var errorMsg = data["msg"].Value;
            //回调
            UltraPayDelegateProxy.ONPayDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultCancel, null, errorMsg);
        }

        /*
         * 分享取消
         * **/
        public void onShareCancelAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string shareType = data["shareType"].Value;
            //回调
            UltraShareDelegateProxy.ONShareDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultCancel, shareType,
                "分享取消");
        }

        /*
         * 分享失败
         * **/
        public void onShareFailedAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string tmsg = data["msg"].Value;
            string shareType = data["shareType"].Value;
            //回调
            UltraShareDelegateProxy.ONShareDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, shareType, tmsg);
        }

        /*
         * 分享成功
         * **/
        public void onShareSuccessdAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string shareType = data["shareType"].Value;
            //回调
            UltraShareDelegateProxy.ONShareDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, shareType,
                "分享成功");
        }

        /*
         * 同意协议回调
         * **/
        public void onClickProtocol(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            //回调
            UltraInitDelegateProxy.ONProtocolAgree?.Invoke();
        }

        /*
         * 广告视频点击回调
         * **/
        public void onAdClickedAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            //回调
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy?.Invoke(UltraSDKAdResult.UltraSDKAdResultClicked, "视频点击回调");
        }

        /*
         * 广告视频关闭回调
         * **/
        public void onAdClosedAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            //回调
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy?.Invoke(UltraSDKAdResult.UltraSDKAdResultClosed, "视频关闭回调");
        }

        /*
         * 广告视频播放完成回调
         * **/
        public void onAdPlayCompleteAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            //回调
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy?.Invoke(UltraSDKAdResult.UltraSDKAdResultPlayComplete,
                "视频播放完成回调");
        }

        /*
         * 广告视频播放失败回调
         * **/
        public void onAdPlayFailedAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string tmsg = data["msg"].Value;
            //回调
            UltraAdPlayDelegateProxy.ONAdPlayDelegateProxy?.Invoke(UltraSDKAdResult.UltraSDKAdResultPlayFailed, tmsg);
        }

        /*
         * 获取oaid回调
         * **/
        public void onGetOAIDResultAction(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string oaid = data["oaid"].Value;
            //回调
            UltraGetOAIDDelegateProxy.ONGetOAIDDelegateProxy?.Invoke(oaid);
        }

        /// <summary>
        /// 打开内置浏览器成功回调	
        /// </summary>
        /// <param name="msg"></param>
        public void onOpenBrowserSucccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string tmsg = data["msg"].Value;
            UltraOpenBrowerDelegateProxy.ONOpenBrowerDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultSuccess, tmsg);
        }

        /// <summary>
        /// 打开内置浏览器失败
        /// </summary>
        /// <param name="msg"></param>
        public void onOpenBrowserFailed(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string tmsg = data["msg"].Value;
            UltraOpenBrowerDelegateProxy.ONOpenBrowerDelegateProxy?.Invoke(UltraSDKResult.UltraSDKResultFailed, tmsg);
        }

        /// <summary>
        /// 撤回注销成功回调
        /// </summary>
        /// <param name="msg"></param>
        public void onAccountUnCancellationSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string tmsg = data["msg"].Value;
            UltraAccountUnCancellationDelegateProxy.ONAccountUnCancellationDelegateProxy?.Invoke(
                UltraSDKResult.UltraSDKResultSuccess, tmsg);
        }

        /// <summary>
        /// 撤回账号注销失败回调
        /// </summary>
        /// <param name="msg"></param>
        public void onAccountUnCancellationFail(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            string tmsg = data["msg"].Value;
            UltraAccountUnCancellationDelegateProxy.ONAccountUnCancellationDelegateProxy?.Invoke(
                UltraSDKResult.UltraSDKResultFailed, tmsg);
        }

        #endregion

        public void UltrasdkAgreeProtocol(string s)
        {
            _androidJavaObject.Call("requestUltraSdkSetListener", s);
        }
    }
}