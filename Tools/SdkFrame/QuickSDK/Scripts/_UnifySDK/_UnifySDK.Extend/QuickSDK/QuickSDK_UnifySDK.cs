using quicksdk;
using UnifySDK.Event;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnifySDK
{
    #region BaseUnifySDKFactory
    public class QuickSDK_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override string SDKName
        {
            get => "QuickSDK";
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<QuickSDK_UnifySDKConfig>();;
            return new QuickSDK_UnifySDK(sdkConfig,SDKName);
        }
    }
    #endregion
    
    #region BaseUnifySDK
    public partial class QuickSDK_UnifySDK : BaseUnifySDK<QuickSDK_UnifySDKConfig>
    {
        public QuickSDK_UnifySDK(QuickSDK_UnifySDKConfig config,string sdkName) : base(config,sdkName) {}
        
        public override void OnInit()
        {
            var go = new GameObject("QuickSDKListener_UnifySDK",typeof(QuickSDKListener_UnifySDK));
            var lister = go.GetComponent<QuickSDKListener_UnifySDK>();
            if(Application.isPlaying)
                Object.DontDestroyOnLoad(go);
            QuickSDK.getInstance ().setListener (lister);
            QuickSDK.getInstance().init();
        }

        public override string GetDeviceId()
        {
            return QuickSDK.getInstance().getDeviceId();
        }
        
        public override string GetChannelID()
        {
            return  QuickSDK.getInstance().channelType().ToString();
        }

        public override string GetChannelName()
        {
            return  QuickSDK.getInstance().channelName();
        }
    }

    #endregion
    
    #region IUnifySDK_Account
    public partial class QuickSDK_UnifySDK : IUnifySDK_Account
    {
        public void Login()
        {
            QuickSDKImp.getInstance ().login();
        }

        public void Logout()
        {
            QuickSDKImp.getInstance ().logout();
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
            QuickSDK.getInstance().showPrivace();
        }
        
        [UnifySDKEvent(typeof(AEvent<InitSuccessData>))]
        public AEvent<InitSuccessData> OnInitSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<InitFailedData>))]
        public AEvent<InitFailedData> OnInitFailed { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<LoginSuccessData>))]
        public AEvent<LoginSuccessData> OnLoginSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<SwitchAccountSuccessData>))]
        public AEvent<SwitchAccountSuccessData> OnSwitchAccountSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<LoginFailedData>))]
        public AEvent<LoginFailedData> OnLoginFailed { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<LogoutSuccessData>))]
        public AEvent<LogoutSuccessData> OnLogoutSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<ExitSuccessData>))]
        public AEvent<ExitSuccessData> OnExitSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<PrivacyAgreeData>))]
        public AEvent<PrivacyAgreeData> OnPrivacyAgree { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<PrivacyRefuseData>))]
        public AEvent<PrivacyRefuseData> OnPrivacyRefuse { get; set; }
      
    }
    #endregion
    
    #region IUnifySDK_Purchase
    public partial class QuickSDK_UnifySDK :IUnifySDK_Purchase
    {
        public void Purchase(PurchaseOrderInfo data)
        {
            OrderInfo orderInfo = new OrderInfo();
            orderInfo.goodsID = data.goodsID;
            orderInfo.goodsName = data.goodsName;
            orderInfo.goodsDesc = data.goodsDesc;
            orderInfo.quantifier = data.quantifier;
            orderInfo.extrasParams = data.extrasParams ;
            orderInfo.count = data.count;
            orderInfo.amount = data.amount;
            orderInfo.price = data.price ;
            orderInfo.callbackUrl =  data.callbackUrl;
            orderInfo.cpOrderID =  data.cpOrderID;
            QuickSDK.getInstance().pay(orderInfo, CurrGameRoleInfo);
        }

        private GameRoleInfo CurrGameRoleInfo;
        
        private enum SubmitType
        {
            StartGame = 1,
            CreateRole = 2,
            UpdateRole = 3,
        }
        
        public void SubmitPurchaseRoleInfo(PurchaseRoleInfo data)
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
                gameRoleBalance =data.gameRoleBalance,
                gameRoleID =data. gameRoleID,
                gameRoleLevel =data. gameRoleLevel,
                gameRoleName =data. gameRoleName,
                partyName =data. partyName,
                serverID =data. serverID,
                serverName =data. serverName,
                vipLevel =data. vipLevel,
                roleCreateTime =data.roleCreateTime,
                gameRoleGender =data.gameRoleGender,
                gameRolePower =data.gameRolePower,
                partyId =data.partyId,
                professionId =data.professionId,
                profession =data.profession,
                partyRoleId =data.partyRoleId,
                partyRoleName =data.partyRoleName,
                friendlist =data.friendlist
            };
            switch ((SubmitType)data.SubmitType)
            {
                case SubmitType.StartGame:
                    QuickSDK.getInstance().callFunction(FuncType.QUICK_SDK_FUNC_TYPE_REAL_NAME_REGISTER);

                    QuickSDK.getInstance().enterGame(gameRoleInfo); //开始游戏
                    break;
                case SubmitType.CreateRole:
                    QuickSDK.getInstance().createRole(gameRoleInfo); //创建角色
                    break;
                case SubmitType.UpdateRole:
                    QuickSDK.getInstance().updateRole(gameRoleInfo);
                    break;
            }
            
            CurrGameRoleInfo = gameRoleInfo;
            
        }


        public void QueryGoods() 
        {
            QuickSDK.getInstance().callFunction(FuncType.QUICK_SDK_FUNC_TYPE_QUERY_GOODS_INFO);
        }

        [UnifySDKEvent(typeof(AEvent<PurchaseSuccessData>))]
        public AEvent<PurchaseSuccessData> OnPurchaseSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<PurchaseFailedData>))]
        public AEvent<PurchaseFailedData> OnPurchaseFailed { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<PurchaseCancelData>))]
        public AEvent<PurchaseCancelData> OnPurchaseCancel { get; set; }
     
    }
    #endregion
}
