using UnityEngine;

namespace com.ultrasdk.unity
{
    public class UltraSDKLoader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void UnitySuportListenerLoader()
        {
            UltraSDKImp.Instance.OnInitListener();
        }
    }
}