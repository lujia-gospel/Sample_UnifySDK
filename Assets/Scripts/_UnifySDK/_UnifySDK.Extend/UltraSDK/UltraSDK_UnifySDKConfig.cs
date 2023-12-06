using System;

namespace UnifySDK
{
    [Serializable]
    public class UltraSDK_UnifySDKConfig : BaseUnifySDKConfig
    {
        public string myUltraProductId = "10244";
        
        public string myUltraProductKey = "ai2aalgjlcitqbc3ouk6";
 
        
        public override string[] GetTargetPlatforms()
        {
            return new string[] { "Android_Default","IOS_Default","Ultra_1","Ultra_2","Ultra_3","Ultra_4","Ultra_5"}; 
        }
    }
}
