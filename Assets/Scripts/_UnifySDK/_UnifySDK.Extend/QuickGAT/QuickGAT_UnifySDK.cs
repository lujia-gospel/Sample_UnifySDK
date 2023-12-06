using System; 
using UnifySDK.Event;
using UnifySDK.Event.Account;
using UnifySDK.Event.Device;
using UnifySDK.Event.Purchase;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnifySDK
{
    #region BaseUnifySDKFactory
    public class QuickGAT_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override UnifySDKType SDKType
        {
            get => UnifySDKType.QuickGAT;
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<QuickGAT_UnifySDKConfig>();;
            return new QuickGAT_UnifySDK(sdkConfig,SDKType);
        }
    }
    #endregion
    
    #region BaseUnifySDK
    public partial class QuickGAT_UnifySDK : BaseUnifySDK<QuickGAT_UnifySDKConfig>
    {      
        public override int Priority { get=>100; }
        public override bool AutoInit => false;
        readonly bool isInit = false;
        public QuickGAT_UnifySDK(QuickGAT_UnifySDKConfig config, UnifySDKType sdkType) : base(config, sdkType)
        {
            var go = new GameObject("QuickSdkCallBack",typeof(QuickSdkCallBack));
            //var lister = go.GetComponent<QuickSdkCallBack>();
            if(Application.isPlaying)
                Object.DontDestroyOnLoad(go); 
            
            Debug.Log("---------InitCallBack");


            if ( isInit == false)
            {
#if UNITY_EDITOR
#elif UNITY_ANDROID
                AndroidUtility.CurrentActivity.Call("InitCallBack", new FinishInitCallBack());

#elif UNITY_IOS && USE_QUICK_SDK
                QKGameGAT.txQKInit();
#endif
                isInit = true;
            }
        }
        public override void OnInit()
        { 
            base.OnInit();
        }
        
        public override string GetChannelID()
        {
            return  "9999";
        }

        public override string GetChannelName()
        {
            return  "GATQuickGame";
        } 
    }

    #endregion
    
    #region IUnifySDK_Account
    public partial class QuickGAT_UnifySDK : IUnifySDK_Account
    {
        public void Login(bool isShowMenu)
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            string _isShowMenu = isShowMenu ? "1" : "0";
            AndroidUtility.CurrentActivity.Call("login");
#elif UNITY_IOS 
                QKGameGAT.txQKLogin(isShowMenu);
#endif
        }
        public void UnAccountDeletion()
        {
             
        }
        public void Logout()
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            AndroidUtility.CurrentActivity.Call("logout");
#elif UNITY_IOS 
                QKGameGAT.txQKLogout();
#endif
        }

        public bool IsUserGuest()
        {
            return false;
        }

        public void AccountDeletion()
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            AndroidUtility.CurrentActivity.Call("AccountDeletion");
#elif UNITY_IOS  
                QKGameGAT.txQKAccountDeletion();
#endif
        }
        
        public void QuitGame()
        {
             
        }

        public bool CanQuitGame()
        {
            return false;
        }
        public void ShowMenu()
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            AndroidUtility.CurrentActivity.Call("showFloatView");
#elif UNITY_IOS 
                QKGameGAT.txQKShowMenu();
#endif
        }
        
        public void DismissMenu()
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            AndroidUtility.CurrentActivity.Call("closeFloatView");
#elif UNITY_IOS  
                QKGameGAT.txQKDismissMenu();
#endif
        }
        
        public void ShowUserCenter()
        {
             
        }
        public void ShowCustomerCenter()
        {
             
        }

        public void ShowPrivacy()
        {
#if UNITY_EDITOR
            UnifySDKEventSystem.Instance.Publish(new PrivacyAgreeData());  
#endif
        }

        public string GetCPSChannelID()
        {
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
    public partial class QuickGAT_UnifySDK : IUnifySDK_Purchase
    {
        public void Purchase(PurchaseOrderInfoData data)
        {

#if UNITY_EDITOR
#elif UNITY_ANDROID
  
            string Param = (data.extrasParams == "") ? "android" : data.extrasParams;
            if (GameProperties.Get().IsUseHuaWeiSDK)
            {
                Param = (data.extrasParams == "") ? "huawei" : data.extrasParams;
            }
            AndroidUtility.CurrentActivity.Call("createOrder", data.cpOrderID, data.goodsDesc, data.goodsID, data.amount,
                data.currency, data.roleName, data.roleLv, data.serverName,Param);
#elif UNITY_IOS  
                string Param = (data.extrasParams == "") ? "ios" : data.extrasParams;
                QKGameGAT.txQKCongzi(data.goodsID, data.goodsDesc, data.amount.ToString(), data.cpOrderID, "", Param);
#endif
        }

        //private GameRoleInfo CurrGameRoleInfo;

        
        public void SubmitPurchaseRoleInfo(PurchaseRoleInfoData data)
        {
#if UNITY_EDITOR
#elif UNITY_ANDROID
            AndroidJavaObject roleInfo = new AndroidJavaObject("com.quickgame.android.sdk.bean.QGRoleInfo");

            roleInfo.Call("setRoleName", data.gameRoleName);
            roleInfo.Call("setRoleLevel", data.gameRoleLevel);
            roleInfo.Call("setServerName", data.serverName); 
    

            var ins = AndroidUtility.GetQuickIns();

            ins.Call("submitRoleInfo", roleInfo);
#elif UNITY_IOS 
                QKGameGAT.txQKSetRoleInfo(data.serverName, data.serverName, data.gameRoleID, data.gameRoleName, data.gameRoleLevel, data.vipLevel);
#endif   
 
            
        }

        public void ConsumePurchase(string token) {}

        public void QueryPurchasesAsync() {}


        public void QueryGoods() 
        {
             
        }

        public AEvent<PurchaseSuccessData> OnPurchaseSuccess { get; set; }
        public AEvent<PurchaseFailedData> OnPurchaseFailed { get; set; }
        public AEvent<PurchaseCancelData> OnPurchaseCancel { get; set; }
        
        public AEvent<ConsumeSucceedData> OnConsumeSuccess { get; set; }

        public AEvent<RePaySuccessData> OnRePaySuccess { get; set; } 
    }
    #endregion

    public partial class QuickGAT_UnifySDK : IUnifySDK_Device
    {
        
        public void GetDeviceInfo(Action<DeviceInfoData> callBack)
        {
            DeviceInfoData data = new DeviceInfoData();
            // data.OAID = QuickGAT.getInstance().getOaid();
            // data.UDID = QuickGAT.getInstance().getDeviceId();
            callBack(data);
        }

        public void Translate(TranslateData data)
        {
           
        }
    }
}
