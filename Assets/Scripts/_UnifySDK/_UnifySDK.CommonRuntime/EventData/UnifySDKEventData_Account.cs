namespace UnifySDK.Event
{
    public struct InitSuccessData {}
    public struct InitFailedData
    {
        public string errMsg;
    }
    public struct LoginSuccessData
    {
        public string uid; 
        public string userName;
        public string token;
        public string errMsg;
    }
    public struct SwitchAccountSuccessData
    {
        public string uid; 
        public string userName;
        public string token;
        public string errMsg;
    }
    
    public struct LoginFailedData
    {
        public string errMsg;
    }
    
    public struct LogoutSuccessData {}
    public struct ExitSuccessData {}
    
    public struct PrivacyAgreeData {}

    public struct PrivacyRefuseData {}
}