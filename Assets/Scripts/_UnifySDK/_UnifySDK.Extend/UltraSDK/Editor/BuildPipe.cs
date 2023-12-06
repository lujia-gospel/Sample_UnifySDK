using com.ultrasdk.unity.native.Editor; 
using UnifySDK;
using UnifySDK.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEngine;

public class BuildPipe : UnityEditor.Editor, IPreprocessBuild
{
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        // 检查文件和配置
        if (target == BuildTarget.Android)
        {
            //开启调试
            UltraSDKAndroidEditor.SetVerbose(true);
            //编译Android清单等文件
            UltraSDKAndroidEditor.AndroidBuild();

            UltraSDK_UnifySDKConfig cfg =
                UnifySDKConfigSettingsEditor.GetUnifySDKConfig<UltraSDK_UnifySDKConfig>("Android_Default");
            if (cfg != null)
            { 
                UltraSDKAndroidEditor.UltraSdkConfigXml(cfg.myUltraProductId, cfg.myUltraProductKey);
                UDebug.Logic.Log(
                    "OnPreprocessBuild1:myUltraProductId" + cfg.myUltraProductId + "myUltraProductKey" + cfg.myUltraProductKey);
            } 
        }
    }

    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string path)
    {
        if (target == BuildTarget.iOS)
        {
            //开启调试
            UltraSDKiOSEditor.SetVerbose(true);
            //融合SDK
            UltraSDKiOSEditor.iOSBuild(path);
        }
    }

    public int callbackOrder { get; }
}