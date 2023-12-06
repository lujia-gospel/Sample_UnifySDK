namespace UnifySDK
{
    public partial class EnvironmentVariableSettings
    {
        [CustomHeader("编辑模式走UnifySDK","编辑模式不走UnifySDK")]
        public bool EditorRunUnifySDK = false;
        [CustomHeader("资源模式:Bundle","资源模式:Local本地")]
        public bool AssetBundleMode = false;
        [CustomHeader("游戏模式","UI模板模式")]
        public bool UITemplateMode = false;
    }
}


