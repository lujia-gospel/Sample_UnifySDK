using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraLoginDelegateProxy
    {
        public delegate void LoginDelegateProxy(UltraSDKResult result,UltraUserInfo userInfo, string msg) ;

        public static LoginDelegateProxy ONLoginDelegateProxy ;
        
    }
}