using com.ultrasdk.unity.Entry;

namespace com.ultrasdk.unity.Handle
{
    public class UltraSwitchAccDelegateProxy
    {
        public delegate void SwitchAccDelegateProxy(UltraSDKResult result,UltraUserInfo userInfo, string msg) ;

        public static SwitchAccDelegateProxy ONSwitchAccDelegateProxy ;
    }
}