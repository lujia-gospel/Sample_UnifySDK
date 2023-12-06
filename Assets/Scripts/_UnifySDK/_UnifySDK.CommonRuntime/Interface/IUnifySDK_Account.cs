using UnifySDK.Event;
using UnifySDK.Event.Account;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Account
    {
        void Login(bool isShowMenu);
        void Logout();
        void AccountDeletion();
        void UnAccountDeletion();
        bool IsUserGuest();
        void QuitGame();
        bool CanQuitGame();
        void ShowMenu();
        void DismissMenu();
        void ShowUserCenter();
        void ShowCustomerCenter();
        void ShowPrivacy();
        string GetCPSChannelID();
        
        #region Listener
        
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
        #endregion
    }
}