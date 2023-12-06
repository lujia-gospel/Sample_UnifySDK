using System;

namespace UnifySDK
{
    [Serializable]
    public class   Crasheye_UnifySDKConfig : BaseUnifySDKConfig
    {
        [CustomHeader("启动C#堆栈回溯","关闭C#堆栈回溯")]
        public bool EnableCSharpStackTrace;
        public override string[] GetTargetPlatforms()
        {
            return new [] { "Android_Default","IOS_Default"};
        }
    }
}