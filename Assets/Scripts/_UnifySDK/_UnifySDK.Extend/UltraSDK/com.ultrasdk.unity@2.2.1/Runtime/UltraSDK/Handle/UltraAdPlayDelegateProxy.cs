using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraAdPlayDelegateProxy
    {
        public delegate void AdPlayDelegateProxy(UltraSDKAdResult result, string msg) ;

        public static AdPlayDelegateProxy ONAdPlayDelegateProxy ;
    }
}