using System;

namespace UnifySDK
{
    [Serializable]
    public class MaxSDK_UnifySDKConfig : BaseUnifySDKConfig
    {
        public override string[] GetTargetPlatforms()
        {
            return new string[] { "Android_Default","IOS_Default","Ultra_1","Ultra_2","Ultra_3","Ultra_4","Ultra_5"}; 
        }
    }
}
