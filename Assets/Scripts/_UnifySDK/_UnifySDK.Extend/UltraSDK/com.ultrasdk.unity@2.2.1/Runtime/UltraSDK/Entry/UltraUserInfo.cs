namespace com.ultrasdk.unity.Entry
{
    public class UltraUserInfo
    {
        public string plat;
        public string accessToken;
        public string sdkUserId;
        public string userName ;
        
        /*
         * iOS独有
         * **/
        public string accessCode;
        
        /*
         * android独有
         * **/
        public string channelToken;
        public string extendParams;
        public string isFristLogin;
        public string serverMessage;
    }
}