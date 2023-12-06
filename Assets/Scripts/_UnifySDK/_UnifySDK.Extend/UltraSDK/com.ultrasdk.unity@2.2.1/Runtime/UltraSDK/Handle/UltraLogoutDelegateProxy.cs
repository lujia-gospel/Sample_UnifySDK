using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraLogoutDelegateProxy
    {
        public delegate void LogutDelegateProxy(UltraSDKResult result, string msg) ;

        public static LogutDelegateProxy ONLogutDelegateProxy ;
        
        public delegate void LoginInvalidDelegateProxy(UltraSDKLoginInvalidResult result, string msg) ;

        public static LoginInvalidDelegateProxy ONLoginInvalidDelegateProxy ;
    }
}