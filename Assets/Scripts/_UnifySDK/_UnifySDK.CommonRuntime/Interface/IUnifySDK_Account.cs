using UnifySDK.Event;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Account
    {
        void Login();
        void Logout();
        void AccountDeletion();
        void QuitGame();
        bool CanQuitGame();
        void ShowMenu();
        void DismissMenu();
        void ShowUserCenter();
        void ShowCustomerCenter();
        void ShowPrivacy();
        
        #region Listener
        public AEvent<InitSuccessData> OnInitSuccess { get; set; }
        public AEvent<InitFailedData> OnInitFailed { get; set; }
        public AEvent<LoginSuccessData> OnLoginSuccess { get; set; }
        public AEvent<SwitchAccountSuccessData> OnSwitchAccountSuccess { get; set; }
        public AEvent<LoginFailedData> OnLoginFailed { get; set; }
        public AEvent<LogoutSuccessData> OnLogoutSuccess { get; set; }
        public AEvent<ExitSuccessData> OnExitSuccess { get; set; }
        public AEvent<PrivacyAgreeData> OnPrivacyAgree { get; set; }
        public AEvent<PrivacyRefuseData> OnPrivacyRefuse { get; set; }
        #endregion
    }
}