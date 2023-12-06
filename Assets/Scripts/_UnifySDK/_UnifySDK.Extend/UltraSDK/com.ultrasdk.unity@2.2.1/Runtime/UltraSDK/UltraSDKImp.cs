using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using com.ultrasdk.unity.Entry;
using com.ultrasdk.unity.Handle;
using com.ultrasdk.unity.Platform;
using Object = UnityEngine.Object;

namespace com.ultrasdk.unity
{
    public class UltraSDKImp : UltraMonoSingleton<UltraSDKImp>
    {
        private bool isDebug = false;
        public bool IsDebug => isDebug;

        /// <summary>
        /// iOS平台对象
        /// </summary>
        private UltraiOSPlatform _ultraiOSPlatform;

        /// <summary>
        /// Android平台对象
        /// </summary>
        private UltraAndroidPlatform _ultraAndroidPlatform;

        private UltraSDKImp()
        {
        }

        #region Debug

        public void SetDebug(bool debug)
        {
            isDebug = debug;
            if (_ultraiOSPlatform != null)
            {
                _ultraiOSPlatform.SetDebug(debug);
            }

            if (_ultraAndroidPlatform != null)
            {
                _ultraAndroidPlatform.SetDebug(debug);
            }
        }

        #endregion

        #region LifeCircle

        protected override void OnSingletonDestroy()
        {
            base.OnSingletonDestroy();
            if (_ultraiOSPlatform != null)
            {
                Object.Destroy(_ultraiOSPlatform);
                _ultraiOSPlatform = null;
            }

            if (_ultraAndroidPlatform != null)
            {
                Object.Destroy(_ultraAndroidPlatform);
                _ultraAndroidPlatform = null;
            }
        }

        /// <summary>
        /// 设置监听对象
        /// </summary>
        public void OnInitListener()
        {
            UnitySuportListener();
        }


        private void UnitySuportListener()
        {
            Debug.Log($" -------> UnityUltraSuportListener <-----------");
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                GameObject obj = new GameObject(nameof(UltraiOSPlatform));
                Object.DontDestroyOnLoad(obj);
                _ultraiOSPlatform = obj.AddComponent<UltraiOSPlatform>();
                _ultraiOSPlatform.name = "UltraiOSPlatform";
            }

            if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                GameObject obj = new GameObject(nameof(UltraAndroidPlatform));
                Object.DontDestroyOnLoad(obj);
                _ultraAndroidPlatform = obj.AddComponent<UltraAndroidPlatform>();
                _ultraAndroidPlatform.name = "UltraAndroidPlatform";
                _ultraAndroidPlatform.OnInitAndroidClzz();
            }
        }

        #endregion

        /// <summary>
        /// 判断是否是编译器
        /// </summary>
        /// <returns></returns>
        private bool IsRuntimeEditor()
        {
            return Application.platform == RuntimePlatform.OSXEditor ||
                   Application.platform == RuntimePlatform.WindowsEditor ||
                   Application.platform == RuntimePlatform.LinuxEditor;
        }

        /// <summary>
        /// 融合SDK初始化
        /// </summary>
        /// <param name="project"><see cref="UltraProject"/>初始化对象</param>
        /// <param name="onInitDelegateProxy"><see cref="UltraInitDelegateProxy.InitDelegateProxy"/>初始化回调</param>
        /// <param name="onProtocolAgree"><see cref="UltraInitDelegateProxy.ProtocolAgree"/>同意协议回调</param>
        public void InitWithUltraSDK(UltraProject project, UltraInitDelegateProxy.InitDelegateProxy onInitDelegateProxy)
        {
            UltraInitDelegateProxy.ONInitDelegateProxy = onInitDelegateProxy;

            string ultraProductId = String.IsNullOrEmpty(project.ultraProductId) ? "" : project.ultraProductId;
            string ultraProductKey = String.IsNullOrEmpty(project.ultraProductKey) ? "" : project.ultraProductKey;

            if (isDebug)
            {
                Debug.Log(" ===========> InitWithUltraSDK <=========");
                Debug.Log(" ===========> ultraProductId :" + ultraProductId + " <=========");
                Debug.Log(" ===========> ultraProductKey :" + ultraProductKey + " <=========");
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkInit(ultraProductId, ultraProductKey);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkInit(ultraProductId, ultraProductKey);
            }
        }

        /// <summary>
        /// 同意协议回调
        /// </summary>
        /// <param name="onProtocolAgree"><see cref="UltraInitDelegateProxy.ProtocolAgree"/>同意协议</param>
        public void SdkProtocolAgree(UltraInitDelegateProxy.ProtocolAgree onProtocolAgree)
        {
            UltraInitDelegateProxy.ONProtocolAgree = onProtocolAgree;

            if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkAgreeProtocol(_ultraAndroidPlatform.name);
            }
        }

        /// <summary>
        /// 登录失效
        /// </summary>
        /// <param name="onLoginInvalidDelegateProxy"><see cref="UltraLogoutDelegateProxy.LoginInvalidDelegateProxy"/>登录失效或者未成年人被踢下线回调</param>
        public void SDKLoginInvalid(UltraLogoutDelegateProxy.LoginInvalidDelegateProxy onLoginInvalidDelegateProxy)
        {
            UltraLogoutDelegateProxy.ONLoginInvalidDelegateProxy = onLoginInvalidDelegateProxy;
        }

        /*
        * =================== 登录支付相关API ===================
        */
        /*
         * 登录
         * **/
        public void Login(UltraLoginDelegateProxy.LoginDelegateProxy onLoginDelegateProxy,
            UltraSwitchAccDelegateProxy.SwitchAccDelegateProxy onSwitchAccDelegateProxy)
        {
            UltraLoginDelegateProxy.ONLoginDelegateProxy = onLoginDelegateProxy;
            UltraSwitchAccDelegateProxy.ONSwitchAccDelegateProxy = onSwitchAccDelegateProxy;

            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkEnterLoginView();
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkEnterLoginView();
            }
        }

        /*
           * 进入游戏
           */
        public void EnterGame(UltraGameRoleInfo roleInfo)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkRoleLoginWithGameRoleInfo(roleInfo);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkRoleLoginWithGameRoleInfo(roleInfo);
            }
        }

        /*
        * 角色注册
        */
        public void CreateNewRole(UltraGameRoleInfo roleInfo)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkRoleRegisterWithGameRoleInfo(roleInfo);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkRoleRegisterWithGameRoleInfo(roleInfo);
            }
        }

        /*
        * 角色升级
        */
        public void RoleLevelUp(UltraGameRoleInfo roleInfo)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkRoleLevelUpWithGameRoleInfo(roleInfo);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkRoleLevelUpWithGameRoleInfo(roleInfo);
            }
        }

        /*
           * 注销、退出登陆
           */
        public void Logout(UltraLogoutDelegateProxy.LogutDelegateProxy onLogutDelegateProxy)
        {
            UltraLogoutDelegateProxy.ONLogutDelegateProxy = onLogutDelegateProxy;
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkLogout();
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkLogout();
            }
        }

        /*
        * 调用SDKIAP内购
        */
        public void Pay(UltraPaymentParameters paymentParametersData, UltraGameRoleInfo roleInfo,
            UltraPayDelegateProxy.PayDelegateProxy onPayDelegateProxy)
        {
            UltraPayDelegateProxy.ONPayDelegateProxy = onPayDelegateProxy;
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkIapPurchaseWithData(paymentParametersData);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkIapPurchaseWithData(roleInfo, paymentParametersData);
            }
        }

        /*
        * 收到踢下线结果回调SDK
        */
        public void CpKickOffCallBackWithResult(string result)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkCpKickOffCallBackWithResult(result);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkCpKickOffCallBackWithResult(result);
            }
        }

        //获取协议内容
        public string GetProtocolResult()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                return _ultraiOSPlatform.UltrasdkGetProtocolResult();
            }

            if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                return _ultraAndroidPlatform.UltrasdkGetProtocolResult();
            }

            return string.Empty;
        }

        //点击同意通知
        public void SetAgreeProtocol()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkSetAgreeProtocol();
            }

            if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkSetAgreeProtocol();
            }
        }

        /// <summary>
        /// 打开内置浏览器显示内容
        /// </summary>
        /// <param name="openID">中台配置URL地址的ID</param>
        public void OpenBrowser(string openID,
            UltraOpenBrowerDelegateProxy.OpenBrowerDelegateProxy openBrowerDelegateProxy)
        {
            UltraOpenBrowerDelegateProxy.ONOpenBrowerDelegateProxy = openBrowerDelegateProxy;
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkOpenBrowser(openID);
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkOpenBrowser(openID);
            }
        }

        /// <summary>
        /// 打开用户中心
        /// </summary>
        public void ShowAccountCenter()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkShowAccountCenter();
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkShowAccountCenter();
            }
        }

        /// <summary>
        /// 账号删除
        /// 调用此接口会弹出删除账号界面，删除账号后服务器也会删除，请谨慎操作
        /// </summary>
        public void AccountCancellation()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkAccountCancellation();
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkAccountCancellation();
            }
        }

        /// <summary>
        /// 解除【账号删除】状态，将账号恢复成正常账号
        /// </summary>
        public void AccountUnCancellation(
            UltraAccountUnCancellationDelegateProxy.AccountUnCancellationDelegateProxy
                accountUnCancellationDelegateProxy)
        {
            UltraAccountUnCancellationDelegateProxy.ONAccountUnCancellationDelegateProxy =
                accountUnCancellationDelegateProxy;
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkAccountUnCancellation();
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkAccountUnCancellation();
            }
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        public void SwitchAccount()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkSwitchAccount();
            }
            else if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkSwitchAccount();
            }
        }


        /*
        * =================== 附加功能 ===================
        */
        public void Share(UltraShareInfo shareInfo, UltraShareDelegateProxy.ShareDelegateProxy onShareDelegateProxy)
        {
            UltraShareDelegateProxy.ONShareDelegateProxy = onShareDelegateProxy;
            if (Application.platform == RuntimePlatform.IPhonePlayer && !IsRuntimeEditor())
            {
                _ultraiOSPlatform.UltrasdkShare(shareInfo);
            }

            if (Application.platform == RuntimePlatform.Android && !IsRuntimeEditor())
            {
                _ultraAndroidPlatform.UltrasdkShare(shareInfo);
            }
        }

        /*
         *  ========================== iOS ==========================
         * **/
        public UltraiOSPlatform iOSExtra()
        {
            if (_ultraiOSPlatform == null)
            {
                return null;
            }

            return _ultraiOSPlatform;
        }


        /*
         *  ========================== android ==========================
         * **/
        public UltraAndroidPlatform AndroidExtra()
        {
            if (_ultraAndroidPlatform == null)
            {
                return null;
            }

            return _ultraAndroidPlatform;
        }
    }
}