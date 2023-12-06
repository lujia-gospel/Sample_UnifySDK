using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraShareDelegateProxy
    {
        public delegate void ShareDelegateProxy(UltraSDKResult result,string shareType, string msg) ;

        public static ShareDelegateProxy ONShareDelegateProxy ;
    }
}