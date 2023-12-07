using System;
using Horizon;
using quicksdk;
using UnifySDK.Event;
using UnifySDK.Event.Account;
using UnifySDK.Event.Device;
using UnifySDK.Event.Purchase;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnifySDK
{
    #region BaseUnifySDKFactory

    public class QuickSDK_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override UnifySDKType SDKType
        {
            get => UnifySDKType.QuickSDK;
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<QuickSDK_UnifySDKConfig>();
            ;
            return new QuickSDK_UnifySDK(sdkConfig, SDKType);
        }
    }

    #endregion

    #region BaseUnifySDK

    public partial class QuickSDK_UnifySDK : BaseUnifySDK<QuickSDK_UnifySDKConfig>
    {
        public override int Priority
        {
            get => 100;
        }

        public override bool AutoInit => true;

        public QuickSDK_UnifySDK(QuickSDK_UnifySDKConfig config, UnifySDKType sdkType) : base(config, sdkType)
        {
#if UNITY_IOS && !UNITY_EDITOR
            var goIos = new GameObject("QuickSDKIOSListener_UnifySDK",typeof(QuickSDKIOSListener_UnifySDK));
            var lister2 = goIos.GetComponent<QuickSDKIOSListener_UnifySDK>();
            if(Application.isPlaying)
                Object.DontDestroyOnLoad(goIos);
            QKGame.setListener(lister2);
#elif UNITY_ANDROID && !UNITY_EDITOR
            var go = new GameObject("QuickSDKListener_UnifySDK",typeof(QuickSDKListener_UnifySDK));
            var lister = go.GetComponent<QuickSDKListener_UnifySDK>();
            if(Application.isPlaying)
                Object.DontDestroyOnLoad(go);
            QuickSDK.getInstance ().setListener (lister);
#endif
        }

        public override void OnInit()
        {
            base.OnInit();
#if UNITY_EDITOR
            UnifySDKEventSystem.Instance.Publish(new InitSuccessData());
#elif UNITY_ANDROID
            OnPrivacyAgree.Handler += (_data, eventArgs) =>
            {
                PlayerPrefs.SetInt("Game_4WD_OnShowPrivace", 1);
                QuickSDK.getInstance().init();
            };
            OnPrivacyRefuse.Handler += (_data, eventArgs) => { Application.Quit(); };
            if (QuickSDK.getInstance().channelType() == 114)
            {
                OnExitSuccess.Handler += (_data, eventArgs) => { Application.Quit(); };
            }

            if (PlayerPrefs.GetInt("Game_4WD_OnShowPrivace", 0) == 1 ||
                QuickSDK.getInstance().channelType() == 114)
            {
                QuickSDK.getInstance().init();
            }
            else
            {
                ShowPrivacy();
            }
#elif UNITY_IOS
           QuickSDK.getInstance().init();
#endif
        }

        public override string GetChannelID()
        {
            return QuickSDK.getInstance().channelType().ToString();
        }

        public override string GetChannelName()
        {
            return QuickSDK.getInstance().channelName();
        }
    }

    #endregion

    #region IUnifySDK_Account

    public partial class QuickSDK_UnifySDK : IUnifySDK_Account
    {
        public void Login(bool isShowMenu)
        {
            QuickSDKImp.getInstance().login();
        }

        public void UnAccountDeletion()
        {
        }

        public void Logout()
        {
            QuickSDKImp.getInstance().logout();
        }

        public bool IsUserGuest()
        {
            return false;
        }

        public void AccountDeletion()
        {
            UDebug.Logic.Log("QuickSDKListener_UnifySDK---------不支持销毁账号功能");
        }

        public void QuitGame()
        {
            QuickSDK.getInstance().exit();
        }

        public bool CanQuitGame()
        {
            return QuickSDK.getInstance().isChannelHasExitDialog();
        }

        public void ShowMenu()
        {
            QuickSDK.getInstance().showToolBar(ToolbarPlace.QUICK_SDK_TOOLBAR_BOT_LEFT);
        }

        public void DismissMenu()
        {
            QuickSDK.getInstance().hideToolBar();
        }

        public void ShowUserCenter()
        {
            QuickSDK.getInstance().enterUserCenter();
        }

        public void ShowCustomerCenter()
        {
            QuickSDK.getInstance().enterUserCenter();
        }

        public void ShowPrivacy()
        {
#if UNITY_EDITOR
            UnifySDKEventSystem.Instance.Publish(new PrivacyAgreeData());
#else
       // B站渠道特殊处理
            if (QuickSDK.getInstance().channelType() == 114)
            {
                UnifySDKEventSystem.Instance.Publish(new PrivacyAgreeData());
            }
            QuickSDK.getInstance().showPrivace();
#endif
        }

        public string GetCPSChannelID()
        {
            return QuickSDK.getInstance().callFunctionWithResult(FuncType.QUICK_SDK_FUNC_TYPE_UNDEFINED);
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

    public partial class QuickSDK_UnifySDK : IUnifySDK_Purchase
    {
        public void Purchase(PurchaseOrderInfoData data)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.goodsID = data.goodsID;
            orderInfo.productId = data.productId;
            orderInfo.goodsName = data.goodsName;
            orderInfo.goodsDesc = data.goodsDesc;
            orderInfo.quantifier = data.quantifier;
            orderInfo.extrasParams = data.extrasParams;
            orderInfo.count = data.count;
            orderInfo.amount = data.amount;
            orderInfo.price = data.price;
            orderInfo.callbackUrl = data.callbackUrl;
            orderInfo.cpOrderID = data.cpOrderID;
            QuickSDK.getInstance().pay(orderInfo, CurrGameRoleInfo);
        }

        private GameRoleInfo CurrGameRoleInfo;


        public void SubmitPurchaseRoleInfo(PurchaseRoleInfoData data)
        {
            // UDebug.Log("China---------> start <-----------" + "->" + SubmitType + "->" + channelUserId + "->" +
            //           gameUserId + "->" + serverId + "->" +
            //           serverName + "->" + roleId + "->" +
            //           roleName + "->" + roleAvatar + "->" + level + "->" + vipLevel + "->" + gold1 + "->" + gold2 +
            //           "->" + sumPay);
            UDebug.Logic.Log("China---------SubmitRoleInfo");

            //注：基础信息必须先设置
            //注：GameRoleInfo的字段，如果游戏有的参数必须传，没有则不用传
            var gameRoleInfo = new GameRoleInfo
            {
                gameRoleBalance = data.gameRoleBalance,
                gameRoleID = data.gameRoleID,
                gameRoleLevel = data.gameRoleLevel,
                gameRoleName = data.gameRoleName,
                partyName = data.gameRoleName,
                serverID = data.serverID,
                serverName = data.serverName,
                vipLevel = "0",
                roleCreateTime = "roleCreateTime",
                // gameRoleGender =data.gameRoleGender,
                // gameRolePower =data.gameRolePower,
                // partyId =data.partyId,
                // professionId =data.professionId,
                // profession =data.profession,
                // partyRoleId =data.partyRoleId,
                // partyRoleName =data.partyRoleName,
                // friendlist =data.friendlist,
                gameRoleGender = "男",
                gameRolePower = "0",
                partyId = "0",
                professionId = "0",
                profession = "无",
                partyRoleId = "1",
                partyRoleName = "无",
                friendlist = "无"
            };
            switch (data.submitType)
            {
                case PurchaseRoleInfoData.SubmitType.StartGame:
                    QuickSDK.getInstance().callFunction(FuncType.QUICK_SDK_FUNC_TYPE_REAL_NAME_REGISTER);

                    QuickSDK.getInstance().enterGame(gameRoleInfo); //开始游戏
                    break;
                case PurchaseRoleInfoData.SubmitType.CreateRole:
                    QuickSDK.getInstance().createRole(gameRoleInfo); //创建角色
                    break;
                case PurchaseRoleInfoData.SubmitType.UpdateRole:
                    QuickSDK.getInstance().updateRole(gameRoleInfo);
                    break;
                case PurchaseRoleInfoData.SubmitType.Purchase:
                    QuickSDK.getInstance().updateRole(gameRoleInfo);
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


        public void QueryGoods()
        {
            QuickSDK.getInstance().callFunction(FuncType.QUICK_SDK_FUNC_TYPE_QUERY_GOODS_INFO);
        }

        public AEvent<PurchaseSuccessData> OnPurchaseSuccess { get; set; }
        public AEvent<PurchaseFailedData> OnPurchaseFailed { get; set; }
        public AEvent<PurchaseCancelData> OnPurchaseCancel { get; set; }

        public AEvent<ConsumeSucceedData> OnConsumeSuccess { get; set; }

        public AEvent<RePaySuccessData> OnRePaySuccess { get; set; }
    }

    #endregion

    public partial class QuickSDK_UnifySDK : IUnifySDK_Device
    {
        public void GetDeviceInfo(Action<DeviceInfoData> callBack)
        {
            DeviceInfoData data = new DeviceInfoData();
            data.OAID = QuickSDK.getInstance().getOaid();
            data.UDID = QuickSDK.getInstance().getDeviceId();
            callBack(data);
        }

        public void Translate(TranslateData data)
        {
        }
    }
}