using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using com.ultrasdk.unity.Entry;
using com.ultrasdk.unity.Handle;
using com.ultrasdk.unity.Platform;

namespace com.ultrasdk.unity
{
    public class UltraSDK : UltraMonoSingleton<UltraSDK>
    {
        private UltraSDK()
        {
        }

        public void SetDebug(bool isDebug)
        {
            UltraSDKImp.Instance.SetDebug(isDebug);
        }

        /*
        * =================== 登录支付相关API ===================
        */
        /// <summary>
        /// UltraSDK初始化
        /// </summary>
        /// <param name="project"><see cref="UltraProject"/>初始化参数对象</param>
        /// <param name="onInitDelegateProxy"><see cref="UltraInitDelegateProxy.InitDelegateProxy"/>初始化回调</param>
        /// <param name="onProtocolAgree"><see cref="UltraInitDelegateProxy.ProtocolAgree"/>同意协议回调</param>
        public void InitWithUltraSDK(UltraProject project, UltraInitDelegateProxy.InitDelegateProxy onInitDelegateProxy)
        {
            UltraSDKImp.Instance.InitWithUltraSDK(project, onInitDelegateProxy);
        }

        /// <summary>
        /// 同意协议回调
        /// </summary>
        /// <param name="onProtocolAgree"><see cref="UltraInitDelegateProxy.ProtocolAgree"/>同意协议后回调，CP在回调中处理其他SDK初始化</param>
        public void SdkProtocolAgree(UltraInitDelegateProxy.ProtocolAgree onProtocolAgree)
        {
            UltraSDKImp.Instance.SdkProtocolAgree(onProtocolAgree);
        }

        /// <summary>
        /// UltraSDK有界面登录
        /// </summary>
        /// <param name="onLoginDelegateProxy"><see cref="UltraLoginDelegateProxy.LoginDelegateProxy"/>登录结果回调</param>
        /// <param name="onSwitchAccDelegateProxy"><see cref="UltraSwitchAccDelegateProxy.SwitchAccDelegateProxy"/>切换账号结果回调</param>
        public void Login(UltraLoginDelegateProxy.LoginDelegateProxy onLoginDelegateProxy,
            UltraSwitchAccDelegateProxy.SwitchAccDelegateProxy onSwitchAccDelegateProxy)
        {
            UltraSDKImp.Instance.Login(onLoginDelegateProxy, onSwitchAccDelegateProxy);
        }

        /// <summary>
        /// 进入游戏
        /// </summary>
        /// <param name="roleInfo"><see cref="UltraGameRoleInfo"/>角色信息对象</param>
        public void EnterGame(UltraGameRoleInfo roleInfo)
        {
            UltraSDKImp.Instance.EnterGame(roleInfo);
        }

        /// <summary>
        /// 角色注册
        /// </summary>
        /// <param name="roleInfo"><see cref="UltraGameRoleInfo"/>角色信息对象</param>
        public void CreateNewRole(UltraGameRoleInfo roleInfo)
        {
            UltraSDKImp.Instance.CreateNewRole(roleInfo);
        }

        /// <summary>
        /// 角色升级
        /// </summary>
        /// <param name="roleInfo"><see cref="UltraGameRoleInfo"/角色信息对象></param>
        public void RoleLevelUp(UltraGameRoleInfo roleInfo)
        {
            UltraSDKImp.Instance.RoleLevelUp(roleInfo);
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        /// <param name="onLogoutDelegateProxy"><see cref="UltraLogoutDelegateProxy.LogoutDelegateProxy"/>注销登录结果回调</param>
        public void Logout(UltraLogoutDelegateProxy.LogutDelegateProxy onLogoutDelegateProxy)
        {
            UltraSDKImp.Instance.Logout(onLogoutDelegateProxy);
        }

        /// <summary>
        /// 登录失效
        /// </summary>
        /// <param name="onLoginInvalidDelegateProxy"><see cref="UltraLogoutDelegateProxy.LoginInvalidDelegateProxy"/>登录失效或者未成年人被踢下线回调</param>
        public void SDKLoginInvalid(UltraLogoutDelegateProxy.LoginInvalidDelegateProxy onLoginInvalidDelegateProxy)
        {
            UltraSDKImp.Instance.SDKLoginInvalid(onLoginInvalidDelegateProxy);
        }

        /// <summary>
        /// UltraSDK内购
        /// </summary>
        /// <param name="paymentParametersData"><see cref="UltraPaymentParameters"/>支付参数对象</param>
        /// <param name="roleInfo"><see cref="UltraGameRoleInfo"/>角色信息对象</param>
        /// <param name="onPayDelegateProxy"><see cref="UltraPayDelegateProxy.PayDelegateProxy"/>支付结果回调,最终以服务器结果为准</param>
        public void Pay(UltraPaymentParameters paymentParametersData, UltraGameRoleInfo roleInfo,
            UltraPayDelegateProxy.PayDelegateProxy onPayDelegateProxy)
        {
            UltraSDKImp.Instance.Pay(paymentParametersData, roleInfo, onPayDelegateProxy);
        }

        /// <summary>
        /// 未成年人收到SDK踢下线通知后，游戏需要将角色踢下线后，回调SDK结果
        /// </summary>
        /// <param name="result"><see cref="result"/>结果</param>
        public void CpKickOffCallBackWithResult(string result)
        {
            UltraSDKImp.Instance.CpKickOffCallBackWithResult(result);
        }

        /// <summary>
        /// 获取UltraSDK协议内容[JSON格式]
        /// </summary>
        public string GetProtocolResult()
        {
            return UltraSDKImp.Instance.GetProtocolResult();
        }

        /// <summary>
        /// 游戏自己实现协议同意界面弹框时，点击同意协议时，需要通知SDK
        /// </summary>
        public void SetAgreeProtocol()
        {
            UltraSDKImp.Instance.SetAgreeProtocol();
        }

        /// <summary>
        /// 打开内置浏览器显示内容
        /// </summary>
        /// <param name="openID">中台配置URL地址的ID</param>
        public void OpenBrowser(string openID,
            UltraOpenBrowerDelegateProxy.OpenBrowerDelegateProxy openBrowerDelegateProxy)
        {
            UltraSDKImp.Instance.OpenBrowser(openID, openBrowerDelegateProxy);
        }

        /// <summary>
        /// 打开用户中心
        /// </summary>
        public void ShowAccountCenter()
        {
            UltraSDKImp.Instance.ShowAccountCenter();
        }

        /// <summary>
        /// 账号删除
        /// 调用此接口会弹出删除账号界面，删除账号后服务器也会删除，请谨慎操作
        /// </summary>
        public void AccountCancellation()
        {
            UltraSDKImp.Instance.AccountCancellation();
        }

        /// <summary>
        /// 解除【账号删除】状态，将账号恢复成正常账号
        /// 这个接口主要用于全球渠道无界面使用
        /// </summary>
        public void AccountUnCancellation(
            UltraAccountUnCancellationDelegateProxy.AccountUnCancellationDelegateProxy
                accountUnCancellationDelegateProxy)
        {
            UltraSDKImp.Instance.AccountUnCancellation(accountUnCancellationDelegateProxy);
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        public void SwitchAccount()
        {
            UltraSDKImp.Instance.SwitchAccount();
        }


        /*
        * =================== 附加功能 ===================
        */
        /// <summary>
        /// 分享
        /// 目前仅支持国内分享渠道
        /// </summary>
        /// <param name="shareInfo"><see cref="UltraShareInfo"/>分享对象</param>
        /// <param name="onShareDelegateProxy"><see cref="UltraShareDelegateProxy.ShareDelegateProxy"/>分享结果回调</param>
        public void Share(UltraShareInfo shareInfo, UltraShareDelegateProxy.ShareDelegateProxy onShareDelegateProxy)
        {
            UltraSDKImp.Instance.Share(shareInfo, onShareDelegateProxy);
        }

        /*
         *  ========================== iOS ==========================
         * **/

        /// <summary>
        /// 获取iOS拓展信息
        /// </summary>
        /// <returns></returns>
        public UltraiOSPlatform iOSExtra()
        {
            return UltraSDKImp.Instance.iOSExtra();
        }

        /*
         *  ========================== android ==========================
         * **/
        /// <summary>
        /// 获取Android拓展信息
        /// </summary>
        /// <returns></returns>
        public UltraAndroidPlatform AndroidExtra()
        {
            return UltraSDKImp.Instance.AndroidExtra();
        }
    }
}