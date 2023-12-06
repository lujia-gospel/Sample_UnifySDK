using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraAccountUnCancellationDelegateProxy
    {
        public delegate void AccountUnCancellationDelegateProxy(UltraSDKResult result, string msg) ;

        public static AccountUnCancellationDelegateProxy ONAccountUnCancellationDelegateProxy ;
    }
}