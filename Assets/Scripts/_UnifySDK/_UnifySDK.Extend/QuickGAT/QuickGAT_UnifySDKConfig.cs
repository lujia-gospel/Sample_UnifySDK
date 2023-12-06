using System;

namespace UnifySDK
{
    [Serializable]
    public class QuickGAT_UnifySDKConfig : BaseUnifySDKConfig
    {
        public bool EnableCSharpStackTrace;
        public override string[] GetTargetPlatforms()
        {
            return new string[] { "Android_Default","IOS_Default"};
        }
    }
}
