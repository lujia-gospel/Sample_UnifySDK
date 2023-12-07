using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq; 
using UnifySDK;
using UnityEditor.SceneManagement; 

public static partial class Build
{
    // string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
    public static string userName = System.Environment.UserName;
    public static string tmpPath = "/Users/" + userName + "/tmp/";
    public static string targetPath = "/Users/" + userName + "/Downloads/tomcat938/webapps/4wd";

    public static void GenerateLuaWrap()
    {
        if (IsGenerateLuaWrap)
        {
            CSObjectWrapEditor.Generator.ClearAll();
            CSObjectWrapEditor.Generator.GenAll();
        }
    }

    public static string XCodeProjectPath
    {
        get
        {
            string xcodeProjectPath = tmpPath + "ios/project/";
            return xcodeProjectPath;
        }
    }

    public static void UpdateDevelopBuild()
    {
        if (IsDevelopBuild)
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.connectProfiler = true;
            EditorUserBuildSettings.buildWithDeepProfilingSupport = true;
            PlayerSettings.enableInternalProfiler = true;
        }
        else
        {
            EditorUserBuildSettings.development = false;
            EditorUserBuildSettings.connectProfiler = false;
            EditorUserBuildSettings.buildWithDeepProfilingSupport = false;
            PlayerSettings.enableInternalProfiler = false;
        }
    }

    public static void BuildBundle()
    {
    }

    public static bool IsPatch
    {
        get
        {
            string buildBundleType = EnvironmentUtils.Get("IsPatch", string.Empty);
            if (buildBundleType.ToLower() == "true")
            {
                return true;
            }

            return false;
        }
    }

    public static bool IsBuilderServer
    {
        get
        {
            string buildBundleType = EnvironmentUtils.Get("IsBuilderServer", string.Empty);
            if (buildBundleType.ToLower() == "true")
            {
                return true;
            }

            return false;
        }
    }

    public static bool IsAndroidCreateSymbolsZip
    {
        get
        {
            var buildBundleType = EnvironmentUtils.Get("IsAndroidCreateSymbolsZip", string.Empty);
            return buildBundleType.ToLower() == "true";
        }
    }

    public static string TargetVersionNum
    {
        get
        {
            string targetVersionNum = EnvironmentUtils.Get("TargetVersionNum", "");

            return targetVersionNum;
        }
    }

    public static string BundleBackupRootPath
    {
        get
        {
            string bundleBackupRootPath = EnvironmentUtils.Get("BundleBackupRootPath", "null");

            return bundleBackupRootPath;
        }
    }

    public static string PatchRootPath
    {
        get
        {
            string patchRootPath = EnvironmentUtils.Get("PatchRootPath", "null");

            return patchRootPath;
        }
    }

    public static bool IsGenerateLuaWrap
    {
        get
        {
            string buildBundleType = EnvironmentUtils.Get("GenLuaWrap", string.Empty);
            if (buildBundleType == "Yes")
            {
                return true;
            }

            return false;
        }
    }


    public static string GetBuildNumber
    {
        get
        {
            string buildVersion = EnvironmentUtils.Get("BuildNumber", "test");

            return buildVersion;
        }
    }

    public static string JobName
    {
        get
        {
            string buildVersion = EnvironmentUtils.Get("JobName", "testJobName");

            return buildVersion;
        }
    }

    public static bool IsIPA
    {
        get
        {
            string isIpa = EnvironmentUtils.Get("IsIPA", "null");
            if (isIpa == "Yes")
                return true;

            return false;
        }
    }

    public static bool OnlyBundleModify
    {
        get
        {
            string onlyBundleModify = EnvironmentUtils.Get("OnlyBundleModify", "null");
            var strs = onlyBundleModify.Split(',');

            if (strs.Length == 1)
            {
                if (strs[0] == "Yes")
                    return true;
            }
            else if (strs.Length == 2)
            {
                if (strs[1] == "Yes")
                    return true;
            }


            return false;
        }
    }

    public static bool IsUseNetEaseSDK
    {
        get
        {
            string isUseNetEaseSDK = EnvironmentUtils.Get("IsUseNetEaseSDK", "null");
            if (isUseNetEaseSDK == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsUseNetEaseSDK_TW
    {
        get
        {
            string isUseNetEaseSDK_TW = EnvironmentUtils.Get("IsUseNetEaseSDK_TW", "null");
            if (isUseNetEaseSDK_TW == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsFE
    {
        get
        {
            string isFE = EnvironmentUtils.Get("IsFE", "null");
            if (isFE == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsUseGATSdk
    {
        get
        {
            string isUseGATSdk = EnvironmentUtils.Get("IsUseGATSDK", "null");
            if (isUseGATSdk == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsUseQuickSdk
    {
        get
        {
            string isUseQuickSdk = EnvironmentUtils.Get("IsUseQuickSdk", "null");
            if (isUseQuickSdk == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsUseHuaWeiSdk
    {
        get
        {
            string IsUseHuaWeiSdk = EnvironmentUtils.Get("IsUseHuaWeiSdk", "null");
            if (IsUseHuaWeiSdk == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsUseHuaWeiSdkSelf
    {
        get
        {
            string IsUseHuaWeiSdkSelf = EnvironmentUtils.Get("IsUseHuaWeiSdkSelf", "null");
            if (IsUseHuaWeiSdkSelf == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsAAB
    {
        get
        {
            string isAAB = EnvironmentUtils.Get("IsAAB", "null");
            if (isAAB == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsConvertApk
    {
        get
        {
            string isConvertApk = EnvironmentUtils.Get("IsConvertApk", "Yes");
            if ((isConvertApk == "Yes") && IsAAB)
                return true;
            return false;
        }
    }


    public static bool IsUseHotFix
    {
        get
        {
            string IsUseHotfix = EnvironmentUtils.Get("IsUseHotfix", "null");
            if (IsUseHotfix == "Yes")
                return true;

            return false;
        }
    }

    public static bool IsGM
    {
        get
        {
            string GM = EnvironmentUtils.Get("GM", "null");
            if (GM == "Yes")
                return true;

            return false;
        }
    }

    public static int QuickServerType
    {
        get
        {
            string type = EnvironmentUtils.Get("QuickServerType", "1");
            return int.Parse(type);
        }
    }

    public static bool IsTakePicture
    {
        get
        {
            string buildBundleType = EnvironmentUtils.Get("IsTrackPicture", string.Empty);
            if (buildBundleType.ToLower() == "true")
            {
                return true;
            }

            return false;
        }
    }

    public static bool IsDevelopBuild
    {
        get
        {
            string DevelopBuild = EnvironmentUtils.Get("DevelopBuild", "null");
            if (DevelopBuild == "Yes")
                return true;

            return false;
        }
    }

    public static string GetIP
    {
        get
        {
            string ip = EnvironmentUtils.Get("IP", "");

            var strs = ip.Split(',');
            if (strs.Length == 1)
            {
                return strs[0];
            }
            else
            {
                return strs[1];
            }
        }
    }

    public static int GetUseSDKType
    {
        get
        {
            var sdkType = EnvironmentUtils.Get("UseSDKType", "0");
            var strs = sdkType.Split(',');
            return int.Parse(strs.Length == 1 ? strs[0] : strs[1]);
        }
    }

    public static int GetIsTs
    {
        get
        {
            string isTs = EnvironmentUtils.Get("isTs", "0");
            return int.Parse(isTs);
        }
    }

    public static string GetHotFixdIP
    {
        get
        {
            string ip = EnvironmentUtils.Get("HotFixdIP", "");

            var strs = ip.Split(',');
            if (strs.Length == 1)
            {
                return strs[0];
            }
            else
            {
                return strs[1];
            }
        }
    }

    public static string GetAnnouncementUrl
    {
        get
        {
            string ip = EnvironmentUtils.Get("AnnouncementUrl", "");

            var strs = ip.Split(',');
            if (strs.Length == 1)
            {
                return strs[0];
            }
            else
            {
                return strs[1];
            }
        }
    }


    public static string ClientVersion
    {
        get
        {
            string version = EnvironmentUtils.Get("ClientVersion", "");
            return version;
        }
    }

    public static bool NewBieGuideEnable
    {
        get
        {
            string newGuide = EnvironmentUtils.Get("NewBieGuide", "");
            if (newGuide == "Yes")
                return true;
            return false;
        }
    }

    public static bool PlayerIdEntityEnable
    {
        get
        {
            string playerIdEntity = EnvironmentUtils.Get("PlayerIdEntity", "");
            if (playerIdEntity == "Yes")
                return true;
            return false;
        }
    }


    public static bool PlayVideoEnable
    {
        get
        {
            string playVideo = EnvironmentUtils.Get("PlayVideo", "");
            if (playVideo == "Yes")
                return true;
            return false;
        }
    }

    public static int BundleVersionCode
    {
        get
        {
            string _bundleVersionCode = EnvironmentUtils.Get("BundleVersionCode", "-1");
            int bundleVersionCode = int.TryParse(_bundleVersionCode, out int result) ? result : -1;
            if (bundleVersionCode >= 0)
                return bundleVersionCode;
            else
            {
#if UNITY_ANDROID
                return EditorPrefs.GetInt("Editor_VersionCode_Android", 6) + 1;
#elif UNITY_IPHONE
                return EditorPrefs.GetInt("Editor_VersionCode_IOS", 1) + 1;
#endif
                return 0;
            }
        }
        set
        {
#if UNITY_ANDROID
            EditorPrefs.SetInt("Editor_VersionCode_Android", value);
#elif UNITY_IPHONE
            EditorPrefs.SetInt("Editor_VersionCode_IOS", value);
#endif
        }
    }


    public static void IsForceQuitEditor()
    {
        bool isJenkinsBuild = !string.IsNullOrEmpty(Build.ClientVersion);
        if (isJenkinsBuild)
        {
            EditorUtility.DisplayProgressBar("打Bundle出错", "停止打包", 0);
            UnityEngine.Debug.LogError("error : build bundle failed");
            EditorUtility.ClearProgressBar();
            EditorApplication.Exit(1);
        }
    }


    /// <summary>
    /// 设置平台宏定义
    /// </summary>
    // private static string GetSymbols(string symbols)
    // {
    //     //return DEFINESYMBOLS + symbols;
    //     return PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android) + symbols;
    // }


    // public static string defineSymbols
    // {
    //     get
    //     {
    //         if (GameProperties.Get().IsUseQuickSDK)
    //             return "USE_QUICK_SDK";
    //         else if (GameProperties.Get().IsUseUltraSdk)
    //             return "USE_ULTRA_SDK";
    //         else if (GameProperties.Get().IsUseUltraSdk2)
    //             return "USE_ULTRA_SDK2";
    //         return "USE_ULTRA_SDK8888";
    //     }
    // }

    public static string SDKListModel
    {
        get
        {
            string sdkConfig = EnvironmentUtils.Get("SDKListModel", "");
            return sdkConfig;
        }
    }

    public static string CustomSDKList
    {
        get
        {
            string sdkConfig = EnvironmentUtils.Get("CustomSDKList", "");
            return sdkConfig;
        }
    }


    public static void SetGameProperties()
    {
        string startScenePath = Path.GetFullPath("./Assets/Scenes/StartScene.unity");
        var scene = EditorSceneManager.OpenScene(startScenePath);


        EditorSceneManager.SaveScene(scene);

        AssetDatabase.SaveAssets();
    }

    public static void SetEnvironmentVariable()
    {
        UDebug.Sys.Log($"      SetEnvironmentVariable  SDKListModel: {SDKListModel}          ");
        UDebug.Sys.Log($"      SetEnvironmentVariable  CustomSDKList: {CustomSDKList}          ");
        string sdkModelName = "None";
        var path = UnifySDK.Tools.ReadFileString($"{EnvironmentVariableSettings.SavePath}/SDKListTemplate.txt");
        var sdkModelDic = LitJson.JsonMapper.ToObject<Dictionary<string, Dictionary<string, string>>>(path);
        if (!string.IsNullOrEmpty(SDKListModel) && !sdkModelDic.ContainsKey(SDKListModel))
        {
            UDebug.Sys.LogError($"SDKListTemplate 不包含该值{SDKListModel} （Jenkins SDKListModel） ");
        }
        else if (!string.IsNullOrEmpty(SDKListModel))
        {
            sdkModelName = SDKListModel;
        }

        if (sdkModelName == "CustomSDKList")
        {
            EnvironmentVariableSettings.Instance.UpdateDic(new Dictionary<string, string>());
            var list = CustomSDKList.Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                var kv = list[i].Split(':');
                EnvironmentVariableSettings.Instance.SetSDKValue(kv[0], kv[1]);
            }
        }
        else
            EnvironmentVariableSettings.Instance.UpdateDic(sdkModelDic[sdkModelName]);

        AssetDatabase.SaveAssetIfDirty(EnvironmentVariableSettings.Instance);
        AssetDatabase.SaveAssets();
    }


    [MenuItem("Tools/设置闪屏图片")]
    public static void SetSplahsScreen()
    {
        return;
    }

    [MenuItem("Tools/设置Icon图片")]
    public static void SetDefaultIcon()
    {
    }

    [MenuItem("Tools/清空数据")]
    public static void ClearPersit()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Tools/添加所有本地场景")]
    public static void RebackScenes()
    {
        string scenePath = "/ArtRes/Bundles/scene";

        string path = Application.dataPath + scenePath;


        UnityEngine.Debug.LogError(path);

        string[] files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories).Select(e => e).ToArray();

        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length + 1];


        scenes[0] = new EditorBuildSettingsScene("Assets/Scenes/StartScene.unity", true);


        EditorBuildSettings.scenes = scenes;

        AssetDatabase.SaveAssets();
    }
}


public static class EnvironmentUtils
{
    public static string Get(string name, string defaultValue)
    {
        try
        {
            string[] args = System.Environment.GetCommandLineArgs();
            foreach (var arg in args)
            {
                if (arg.Contains("="))
                {
                    string[] keypair = arg.Split('=');
                    if (keypair.Length > 1)
                    {
                        if (keypair[0].EndsWith(name, System.StringComparison.Ordinal) &&
                            !string.IsNullOrEmpty(keypair[1]))
                        {
                            return keypair[1];
                        }
                    }
                }
            }
        }
        catch
        {
        }

        return defaultValue;
    }
}