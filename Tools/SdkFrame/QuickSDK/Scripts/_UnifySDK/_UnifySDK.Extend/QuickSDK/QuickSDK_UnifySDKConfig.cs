using System;

namespace UnifySDK
{
    [Serializable]
    public class QuickSDK_UnifySDKConfig : BaseUnifySDKConfig
    {
        public override string[] GetTargetPlatforms()
        {
            return new string[] { "Android_Default"};
        }
    }
}
