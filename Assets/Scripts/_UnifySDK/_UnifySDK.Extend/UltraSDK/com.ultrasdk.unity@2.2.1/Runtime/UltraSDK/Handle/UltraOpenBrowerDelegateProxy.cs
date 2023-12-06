using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraOpenBrowerDelegateProxy
    {
        public delegate void OpenBrowerDelegateProxy(UltraSDKResult result, string msg) ;

        public static OpenBrowerDelegateProxy ONOpenBrowerDelegateProxy ;
    }
}