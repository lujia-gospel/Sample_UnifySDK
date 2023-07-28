using System;

namespace UnifySDK
{
    [Serializable]
    public class  DeviceOAID_UnifySDKConfig : BaseUnifySDKConfig
    {
        public override string[] GetTargetPlatforms()
        {
            return new [] { "Android_Default"};
        }
    }
}
