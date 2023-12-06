using System;

namespace UnifySDK
{
    [Serializable]
    public class ReYunSDK_UnifySDKConfig : BaseUnifySDKConfig
    {
        public string assetFileName;
        public string oaidLibraryString;
        public override string[] GetTargetPlatforms()
        {
            return new string[] { "Android_B","Android_C","IOS_Default"};
        }
    }
}