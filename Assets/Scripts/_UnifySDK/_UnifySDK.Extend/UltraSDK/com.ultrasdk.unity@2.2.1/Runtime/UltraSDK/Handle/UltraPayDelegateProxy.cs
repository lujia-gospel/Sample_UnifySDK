using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraPayDelegateProxy
    {
        public delegate void PayDelegateProxy(UltraSDKResult result,UltraPaymentOrder payResult, string msg) ;

        public static PayDelegateProxy ONPayDelegateProxy ;
    }
}