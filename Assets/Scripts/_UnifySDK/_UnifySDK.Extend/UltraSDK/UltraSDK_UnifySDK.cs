using System;
using UnifySDK.Event;
using UnifySDK.Event.Account;
using UnifySDK.Event.Device;
using UnifySDK.Event.Purchase;
using com.ultrasdk.unity;
using com.ultrasdk.unity.Entry;
using com.ultrasdk.unity.Platform;
using UnifySDK.Event.Share;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnifySDK
{
    #region BaseUnifySDKFactory

    public class UltraSDK_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override UnifySDKType SDKType
        {
            get => UnifySDKType.UltraSDK;
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<UltraSDK_UnifySDKConfig>();
            var go = new GameObject("UltraSDK_Mono", typeof(UltraSDK_Mono));
            if (Application.isPlaying)
                Object.DontDestroyOnLoad(go);
            return new UltraSDK_UnifySDK(sdkConfig, SDKType);
        }
    }

    #endregion

    #region BaseUnifySDK

    public partial class UltraSDK_UnifySDK : BaseUnifySDK<UltraSDK_UnifySDKConfig>
    {
        public override int Priority
        {
            get => 100;
        }

        public override bool AutoInit => false;
 
        public string CurrCpOrderId = "0";
        private bool isInit = false;
        private string MyDeviceNum = "";

        public UltraSDK_UnifySDK(UltraSDK_UnifySDKConfig config, UnifySDKType sdkType) : base(config, sdkType)
        {
            Debug.Log("---------InitCallBackUltra");
            if (isInit != false) return;
            Debug.Log("Start Init");

            UltraSDK.Instance.SetDebug(true);

            Debug.Log(
                "InitCallBackUltra1:config.myUltraProductId" + config.myUltraProductId + "myUltraProductKey" + config.myUltraProductKey);
 
            Debug.Log(
                "InitCallBackUltra2:config.myUltraProductId" + config.myUltraProductId + "myUltraProductKey" + config.myUltraProductKey);
        }


        public override void OnInit()
        {  
            base.OnInit();
            var project = new UltraProject {ultraProductId = Config.myUltraProductId, ultraProductKey = Config.myUltraProductKey};
            Debug.Log(
                "OnInit============" + Config.myUltraProductId + "myUltraProductKey" + Config.myUltraProductKey);
#if UNITY_EDITOR
            UnifySDKEventSystem.Instance.Publish(new InitSuccessData());
#elif UNITY_ANDROID
            UltraSDK.Instance.SdkProtocolAgree(() =>
            {
                Debug.Log("SdkProtocolAgree ");
                UltraSDK.Instance.InitWithUltraSDK(project, (code, msg) =>
                {
                    if (code == UltraSDKResult.UltraSDKResultSuccess)
                    {
                        Debug.Log("InitWithUltraSDK Success");
                         
                        isInit = true;
                        UnifySDKEventSystem.Instance.Publish(new InitSuccessData());
                    }
                    else
                    {
                        UnifySDKEventSystem.Instance.Publish(new InitFailedData());
                        Debug.Log("InitWithUltraSDK Fail");
                    }
                });
            });
            UltraSDK.Instance.AndroidExtra()?.GetOAID((msg) => { MyDeviceNum = msg; });
#elif UNITY_IOS
            UltraSDK.Instance.InitWithUltraSDK(project, (code, msg) =>
            {
                if (code == UltraSDKResult.UltraSDKResultSuccess)
                {
                    Debug.Log("InitWithUltraSDK Success");
                    UnifySDKEventSystem.Instance.Publish(new InitSuccessData());
                    isInit = true;
                }
                else
                {
                    UnifySDKEventSystem.Instance.Publish(new InitFailedData());
                    Debug.Log("InitWithUltraSDK Fail");
                }
            });
            UltraSDK.Instance.SdkProtocolAgree(() => { Debug.Log("SdkProtocolAgree "); });
            MyDeviceNum = UltraSDK.Instance.iOSExtra().GetDeviceNum();
            //游戏播放完过渡视频上报闪屏
            UltraSDK.Instance.iOSExtra().PostSplashScreenEndSuccess();
#endif
            UltraSDK.Instance.SDKLoginInvalid(OnLoginInvalidCallBack);
        }

        public override string GetChannelID()
        {
            Debug.Log("GetChannelID--------> start <-----------");
            if (isInit == false) return "0";
            string ChannelID = "0";
#if UNITY_EDITOR
#elif UNITY_ANDROID
            ChannelID = UltraSDK.Instance.AndroidExtra().GetChannelId().ToString();
#elif UNITY_IOS
            ChannelID = "3";
#endif
            Debug.Log("GetChannelID--------> end <-----------" + ChannelID);
            return ChannelID;
        }

        public override string GetChannelName()
        {
            string ChannelName = "PC";
#if UNITY_EDITOR
#elif UNITY_ANDROID
            ChannelName = UltraSDK.Instance.AndroidExtra().GetChannelName();
#elif UNITY_IOS
            ChannelName = "IOS";
#endif
            return ChannelName;
        }

        private void OnLoginInvalidCallBack(UltraSDKLoginInvalidResult result, string msg)
        {
            if (result == UltraSDKLoginInvalidResult.UltraSDKLoginInvalidKick)
            {
                Debug.Log("onUltrasdkLoginInvalid 未成年人被踢下线msg: " + msg);

//游戏退出登录。并返回到未登录界面
//调用接口反馈已T人成功
                UltraSDK.Instance.CpKickOffCallBackWithResult(msg);
                // UltraSdkManager.Get().OnLoginInvalid_lua.Call(UltraSdkManager.Get().UltraSdkCallBack_lua, 1, msg);
            }
            else
            {
                Debug.Log("onUltrasdkLoginInvalid 登录失效msg: " + msg);
                // UltraSdkManager.Get().OnLoginInvalid_lua.Call(UltraSdkManager.Get().UltraSdkCallBack_lua, 2, msg);
            }
            Debug.Log("onUltrasdkLogout 退出登录成功msg: " + msg); 
            UnifySDKEventSystem.Instance.Publish(new LogoutSuccessData {errMsg = msg});
        }

        private void OnLoginCallBack(UltraSDKResult code, UltraUserInfo userInfo, string msg)
        {
            if (code == UltraSDKResult.UltraSDKResultSuccess)
            {
                var channelName = "";
                var channelId = "";
                if (userInfo.plat == "android")
                {
                    Debug.Log("getChannelId >>>> " + UltraSDK.Instance.AndroidExtra().GetChannelId() + "<<<<<<<<");
                    channelId = UltraSDK.Instance.AndroidExtra().GetChannelId().ToString();
                    Debug.Log("getChannelName >>>> " + UltraSDK.Instance.AndroidExtra().GetChannelName() + "<<<<<<<<");
                    channelName = UltraSDK.Instance.AndroidExtra().GetChannelName();
                    Debug.Log("getChannelSdkVersionName >>>> " +
                              UltraSDK.Instance.AndroidExtra().GetChannelSdkVersionName() + "<<<<<<<<");
                    Debug.Log("isChannelHasExitDialog >>>> " +
                              UltraSDK.Instance.AndroidExtra().IsChannelHasExitDialog() +
                              "<<<<<<<<");
                    Debug.Log("getProjectId >>>> " + UltraSDK.Instance.AndroidExtra().GetProjectId() + "<<<<<<<<");
                    UltraSDK.Instance.AndroidExtra().GetOAID((oaid) => { Debug.Log("oaid >>>> " + msg + "<<<<<<<<"); });
                }

                else
                {
                    channelId = "3";
                    channelName = "IOS";
                    Debug.Log("GetSdkId >>>> " + UltraSDK.Instance.iOSExtra().GetSdkId() + "<<<<<<<<");
                    Debug.Log("GetUserId >>>> " + UltraSDK.Instance.iOSExtra().GetUserId() + "<<<<<<<<");
                    Debug.Log("GetUserName >>>> " + UltraSDK.Instance.iOSExtra().GetUserName() + "<<<<<<<<");
                }

                var data = new LoginSuccessData
                {
                    uid = userInfo.sdkUserId,
                    token = userInfo.accessToken,
                    userName = userInfo.userName,
                    channelId = channelId,
                    channelName = channelName
                };
                //sdk用户唯一标识
                UnifySDKEventSystem.Instance.Publish(data);
            }
            else if (code == UltraSDKResult.UltraSDKResultFailed)
            {
                Debug.Log("OnLoginCallBack Failed <<<<<<<<");
                UnifySDKEventSystem.Instance.Publish(new LoginFailedData {errMsg = msg});
            }
            else if (code == UltraSDKResult.UltraSDKResultCancel)
            {
                Debug.Log("OnLoginCallBack Cancel <<<<<<<<");
                Debug.Log("OnLoginCallBack Failed <<<<<<<<");
                UnifySDKEventSystem.Instance.Publish(new LoginCancelData {errMsg = msg});
            }
        }

        private void OnSwitchAccountCallBack(UltraSDKResult result, UltraUserInfo userInfo, string msg)
        {
            if (result == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("OnSwitchAccountCallBack Success <<<<<<<<");
                var data = new SwitchAccountSuccessData();
                data.uid = userInfo.sdkUserId; //sdk用户唯一标识
                data.token = userInfo.accessToken;
                data.userName = userInfo.userName;
                UnifySDKEventSystem.Instance.Publish(data);
            }
            else if (result == UltraSDKResult.UltraSDKResultFailed)
            {
                Debug.Log("OnSwitchAccountCallBack Failed <<<<<<<<");
                UnifySDKEventSystem.Instance.Publish(new SwitchAccountFaileData {errMsg = msg});
            }
            else if (result == UltraSDKResult.UltraSDKResultCancel)
            {
                Debug.Log("OnSwitchAccountCallBack Cancel <<<<<<<<");
                UnifySDKEventSystem.Instance.Publish(new SwitchAccountCancelData {errMsg = msg});
            }
        }

        private void OnLogOutCallBack(UltraSDKResult result, string msg)
        {
            if (result == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("onUltrasdkLogout 退出登录成功msg: " + msg);
//退出成功返回登录界面 
                UnifySDKEventSystem.Instance.Publish(new LogoutSuccessData {errMsg = msg});
            }
            else
            {
                Debug.Log("onUltrasdkLogout 退出登录失败msg: " + msg);
                UnifySDKEventSystem.Instance.Publish(new LogoutFailedData {errMsg = msg});
            }
        }

        private void OnExitCallBack(UltraSDKResult result, string msg)
        {
            if (result == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("onExit Success");
                UnifySDKEventSystem.Instance.Publish(new ExitSuccessData {errMsg = msg});
            }
            else
            {
                Debug.Log("onExit Faliure");
                UnifySDKEventSystem.Instance.Publish(new ExitFailedData {errMsg = msg});
            }
        }

        private void OnPayCallback(UltraSDKResult code, UltraPaymentOrder payResult, string msg)
        {
            if (code == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("OnPayCallback " + payResult.sdkOrderId + "<<<<<<<<");
                Debug.Log($"OrderID : " + payResult.sdkOrderId);
                if (payResult.plat == "android")
                {
                    Debug.Log("onUltrasdkPaySuccess for android ");
                    Debug.Log("cpOrderId: " + payResult.cpOrderId);
                    Debug.Log($"extraParams: {payResult.extraParams}");
                }
                else
                {
                    Debug.Log("onUltrasdkPaySuccess for iOS ");
                    Debug.Log("orderAmount: " + payResult.orderAmount);
                    Debug.Log("currency: " + payResult.currency);
                }

                var data = new PurchaseSuccessData();
                data.orderId = payResult.sdkOrderId;
                data.cpOrderId = payResult.cpOrderId;
                data.extraParam = payResult.extraParams;
                UnifySDKEventSystem.Instance.Publish(data);
                Debug.Log("sdkOrderId: " + payResult.sdkOrderId);
            }
            else if (code == UltraSDKResult.UltraSDKResultFailed)
            {
                Debug.Log("Pay Failed <<<<<<<<");
                var data = new PurchaseFailedData();
                data.orderId = "";
                data.cpOrderId = "";
                data.extraParam = "";
                UnifySDKEventSystem.Instance.Publish(data);  
            }
            else if (code == UltraSDKResult.UltraSDKResultCancel)
            {
                Debug.Log("Pay Cancel <<<<<<<<");
                var data = new PurchaseCancelData();
                data.orderId = "";
                data.cpOrderId = "";
                data.extraParam = "";
                UnifySDKEventSystem.Instance.Publish(data);
            }
        }

        private void OnUnAccountUnCancellationCallBack(UltraSDKResult result, string msg)
        {
            if (result == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("UnAccountUnCancellation Success <<<<<<<<");
            }
            else if (result == UltraSDKResult.UltraSDKResultFailed)
            {
                Debug.Log("UnAccountUnCancellation Failed <<<<<<<<");
            }
            else if (result == UltraSDKResult.UltraSDKResultCancel)
            {
                Debug.Log("UnAccountUnCancellation Cancel <<<<<<<<");
            }
        } 
    }

    #endregion

    #region IUnifySDK_Account

    public partial class UltraSDK_UnifySDK : IUnifySDK_Account
    {
        public void Login(bool isShowMenu)
        {
            Debug.Log("Login  ");
            if (isInit == false) return;
            UltraSDK.Instance.Login(OnLoginCallBack, OnSwitchAccountCallBack);
        } 

        public void Logout()
        {
            Debug.Log("Logout 0 ");
            if (isInit == false) return;
            Debug.Log("Logout  1");
            UltraSDK.Instance.Logout(OnLogOutCallBack);
            Debug.Log("Logout  2");
        }

        public bool IsUserGuest()
        {
            return false;
        }

        public void AccountDeletion()
        {
            Debug.Log("销毁账号--------> start <-----------");
            if (isInit == false) return;
            UltraSDK.Instance.AccountCancellation();
        }

        public void UnAccountDeletion()
        {
            if (isInit == false) return;
            UltraSDK.Instance.AccountUnCancellation(OnUnAccountUnCancellationCallBack);
        }

        public void QuitGame()
        {
            if (isInit == false) return;
            UltraSDK.Instance.AndroidExtra().Exit(OnExitCallBack);
        }

        public bool CanQuitGame()
        {
            Debug.Log("CanQuitGame--------> start <-----------");
            if (isInit == false) return false;
            bool canBe = false;
#if UNITY_EDITOR
#elif UNITY_ANDROID
            canBe = UltraSDK.Instance.AndroidExtra().IsChannelHasExitDialog();
#endif
            Debug.Log("CanQuitGame--------> end <-----------" + canBe.ToString());
            return canBe;
        }

        public void ShowMenu()
        {
            Debug.Log("Login  ");
            if (isInit == false) return;

            UltraSDK.Instance.Login(OnLoginCallBack, OnSwitchAccountCallBack);
        }

        public void DismissMenu()
        {
            if (isInit == false) return;
        }

        public void ShowUserCenter()
        {
            if (isInit == false) return;
            UltraSDK.Instance.ShowAccountCenter();
        }

        public void ShowCustomerCenter()
        {
            if (isInit == false) return;
            UltraSDK.Instance.ShowAccountCenter();
        }

        public void ShowPrivacy()
        {
            //  海外sdk不需要
#if UNITY_EDITOR
            UnifySDKEventSystem.Instance.Publish(new PrivacyAgreeData());
#else
            // UltraSDK.Instance.SetAgreeProtocol(); 
#endif
        }

        public string GetCPSChannelID()
        {
            //  海外sdk不需要
            return "";
        }

        public AEvent<LoginSuccessData> OnLoginSuccess { get; set; }
        public AEvent<SwitchAccountSuccessData> OnSwitchAccountSuccess { get; set; }
        public AEvent<LoginFailedData> OnLoginFailed { get; set; }
        public AEvent<LogoutSuccessData> OnLogoutSuccess { get; set; }
        public AEvent<ExitSuccessData> OnExitSuccess { get; set; }
        public AEvent<PrivacyAgreeData> OnPrivacyAgree { get; set; }
        public AEvent<PrivacyRefuseData> OnPrivacyRefuse { get; set; }
    }

    #endregion

    #region IUnifySDK_Purchase

    public partial class UltraSDK_UnifySDK : IUnifySDK_Purchase
    {
        private UltraGameRoleInfo CurrGameRoleInfo;

        public void QueryGoods()
        {
        }

        public void Purchase(PurchaseOrderInfoData data)
        {
            UDebug.Sys.LogError($"PurchaseOrderInfoData{data.amount} ,{data.cpOrderID},{data.callbackUrl},{data.amount}  ");
            UDebug.Sys.LogError($"PurchaseOrderInfoData isInit{isInit}   ");
            if (isInit == false) return;
            CurrCpOrderId = data.cpOrderID;

            var orderInfo = new UltraPaymentParameters
            {
                goodsId = data.goodsID,
                extraParams = data.goodsID,
                cpOrder = data.cpOrderID,
                callbackUrl = data.callbackUrl
            };
            UDebug.Sys.LogError($"PurchaseOrderInfoData 222  ");
            UltraSDK.Instance.Pay(orderInfo, CurrGameRoleInfo, OnPayCallback);
            UDebug.Sys.LogError($"PurchaseOrderInfoData 333  ");
        }

        public void SubmitPurchaseRoleInfo(PurchaseRoleInfoData data)
        {
            if (isInit == false) return;

            //注：基础信息必须先设置
            var gameRoleInfo = new UltraGameRoleInfo
            {
                channelUserId = data.channelUserId,
                gameUserId = data.gameRoleID,
                serverId = data.serverID,
                serverName = data.serverName,
                roleId = data.gameRoleID,
                roleName = data.gameRoleName,
                roleAvatar = "roleAvatar",
                level = data.gameRoleLevel,
                vipLevel = "1",
                gold1 = data.gold1,
                gold2 = data.gold2,
                sumPay = "1"
            };
            switch (data.submitType)
            {
                case PurchaseRoleInfoData.SubmitType.StartGame:
                    UltraSDK.Instance.EnterGame(gameRoleInfo);
                    break;
                case PurchaseRoleInfoData.SubmitType.CreateRole:
                    UltraSDK.Instance.CreateNewRole(gameRoleInfo);
                    break;
                case PurchaseRoleInfoData.SubmitType.UpdateRole:
                    UltraSDK.Instance.RoleLevelUp(gameRoleInfo);
                    break;
            }

            CurrGameRoleInfo = gameRoleInfo;
        }

        public void ConsumePurchase(string token)
        {
        }

        public void QueryPurchasesAsync()
        {
        }

        public AEvent<PurchaseSuccessData> OnPurchaseSuccess { get; set; }
        public AEvent<PurchaseFailedData> OnPurchaseFailed { get; set; }
        public AEvent<PurchaseCancelData> OnPurchaseCancel { get; set; }
        public AEvent<ConsumeSucceedData> OnConsumeSuccess { get; set; }
        public AEvent<RePaySuccessData> OnRePaySuccess { get; set; }
    }

    #endregion

    #region IUnifySDK_Device

    public partial class UltraSDK_UnifySDK : IUnifySDK_Device
    {
        public void GetDeviceInfo(Action<DeviceInfoData> callBack)
        {
            DeviceInfoData data = new DeviceInfoData();

            callBack(data);
        }

        public void Translate(TranslateData data)
        {
        }
    }

    #endregion

    #region UltraSDK_Share

    public  partial class UltraSDK_UnifySDK : IUnifySDK_Share
    {
        public void SharePhoto(SharePhotoData data)
        { 
            var shareInfo = new UltraShareInfo
            {
                hasUi = true,
                title = data.imageName,
                content = "",
                imgPath = data.imagePath,
                imgUrl = data.caption,
                url = "",
                shareTo = SharePlatform.ALL,
                extenal = data.tag
            };
            UltraSDK.Instance.Share(shareInfo,OnShareCallback); 
        }

        public void ShareUrl(ShareUrlData data)
        {
            var shareInfo = new UltraShareInfo
            {
                hasUi = true,
                title = "",
                content = "",
                imgPath = "",
                imgUrl = "",
                url = data.url,
                shareTo = SharePlatform.ALL,
                extenal = data.tag
            };
            UltraSDK.Instance.Share(shareInfo,OnShareCallback); 
        }

        public void CaptureScreenshotSharePhoto()
        {
            var shareInfo = new UltraShareInfo
            {
                hasUi = true,
                title = "",
                content = "",
                imgPath = "",
                imgUrl = "",
                url = "",
                shareTo = SharePlatform.ALL,
                extenal = ""
            };
            UltraSDK.Instance.Share(shareInfo,OnShareCallback); 
        }
        private void OnShareCallback(UltraSDKResult result, string sharetype, string msg)
        {
            if (result == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("share Success <<<<<<<<"); 
                UnifySDKEventSystem.Instance.Publish(new SharePhotoResultData() {codeType = SharePhotoResultData.CodeType.Succeed});
                
            }
            else if (result == UltraSDKResult.UltraSDKResultFailed)
            {
                Debug.Log("share Failed <<<<<<<<");
                UnifySDKEventSystem.Instance.Publish(new SharePhotoResultData() {codeType = SharePhotoResultData.CodeType.Failed});
            }
            else if (result == UltraSDKResult.UltraSDKResultCancel)
            {
                Debug.Log("share Cancel <<<<<<<<");
                UnifySDKEventSystem.Instance.Publish(new SharePhotoResultData() {codeType = SharePhotoResultData.CodeType.Cancel});
            }
        }
        public AEvent<SharePhotoResultData> OnSharePhotoComplete { get; set; }
        public AEvent<ShareUlrResultData> OnShareUlrComplete { get; set; }
    }

    #endregion
}