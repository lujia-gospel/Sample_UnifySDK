using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public static class UltraInitDelegateProxy
    {
        public delegate void InitDelegateProxy(UltraSDKResult result, string msg) ;

        public static InitDelegateProxy ONInitDelegateProxy ;

        public delegate void ProtocolAgree();

        public static ProtocolAgree ONProtocolAgree;
    }
}