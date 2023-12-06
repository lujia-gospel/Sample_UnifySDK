using System.Collections;
using System.IO;
using UnityEngine;

namespace com.ultrasdk.unity.native.Editor
{
    public class UltraSDKEditorSetting
    {
        public static string GetUltraSDKUnityNativeVersion()
        {
            string packjson = Path.Combine(Directory.GetParent(GetCurSourceFileAbsDir)?.FullName ?? string.Empty, "package.json") ;
            if (!File.Exists(packjson))
            {
                Debug.LogWarning($"Get UltraSDK Native package.json Fail");
            }
            string json = File.ReadAllText(packjson);
            Hashtable table = json.hashtableFromJson();
            string version = (string)table["version"];
            if (string.IsNullOrEmpty(version))
            {
                Debug.LogWarning($"Get UltraSDK Native Version Fail");
                return string.Empty;
            }
            return version;
        }

        public static string GetUltraSDKUnityNativePackageName()
        {
            string packjson = Path.Combine(Directory.GetParent(GetCurSourceFileAbsDir)?.FullName ?? string.Empty, "package.json") ;
            if (!File.Exists(packjson))
            {
                Debug.LogWarning($"Get UltraSDK Native package.json Fail");
            }
            string json = File.ReadAllText(packjson);
            Hashtable table = json.hashtableFromJson();
            string name = (string)table["name"];
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogWarning($"Get UltraSDK Native Package Fail");
                return string.Empty;
            }
            return name;
        }
        static string GetCurSourceFileAbsDir
        {
            get
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                return Path.GetDirectoryName(st.GetFrame(0).GetFileName());
            }
        }
    }
}