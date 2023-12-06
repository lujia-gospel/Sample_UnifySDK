using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraExitDelegateProxy
    {
        public delegate void ExitDelegateProxy(UltraSDKResult result, string msg) ;

        public static ExitDelegateProxy ONExitDelegateProxy ;
    }
}