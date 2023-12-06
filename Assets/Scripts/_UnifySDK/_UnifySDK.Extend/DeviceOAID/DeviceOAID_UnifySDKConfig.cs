using System;

namespace UnifySDK
{
    [Serializable]
    public class  DeviceOAID_UnifySDKConfig : BaseUnifySDKConfig
    {
        public bool istru;
        public override string[] GetTargetPlatforms()
        {
            return new [] { "Android_Default","IOS_Default"};
        }
    }
}
