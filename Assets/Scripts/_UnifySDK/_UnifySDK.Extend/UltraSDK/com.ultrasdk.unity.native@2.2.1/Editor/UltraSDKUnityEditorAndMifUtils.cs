using System;
using System.IO;
using UnityEngine;

namespace com.ultrasdk.unity.native.Editor
{
    public class UltraSDKUnityEditorAndMifUtils
    {
        static string GetCurSourceFileAbsDir
        {
            get
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                return Path.GetDirectoryName(st.GetFrame(0).GetFileName());
            }
        }

        static string UnityEditorDataPath
        {
            get
            {
                var appPath = System.Environment.GetCommandLineArgs()[0];
                var unityAppDir = Path.GetDirectoryName(appPath);

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    return Path.Combine(unityAppDir, "Data");
                }

                // macOS path will be here
                return Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(unityAppDir)));
            }
        }

        static string UnityManifest
        {
            get
            {
                return Path.Combine(UnityEditorDataPath, "PlaybackEngines", "AndroidPlayer", "Apk",
                    "UnityManifest.xml");
            }
        }

        static string LauncherManifest
        {
            get
            {
                return Path.Combine(UnityEditorDataPath, "PlaybackEngines", "AndroidPlayer", "Apk",
                    "LauncherManifest.xml");
            }
        }

        public static void InitUnityManifest(string destPath, bool overwrite = false)
        {
            if (File.Exists(destPath)) { return ; }
            File.Copy(UnityManifestPath(), destPath, overwrite);
        }

        public static string UnityManifestPath()
        {
            string defaultAndroidManifest = Path.Combine(GetCurSourceFileAbsDir, "DefaultAndroidManifest.xml");
            if (File.Exists(defaultAndroidManifest))
            {
                return defaultAndroidManifest;
            }
            
            if (File.Exists(UnityManifest))
            {
                return UnityManifest;
            }
            
            throw new Exception($"can not find default AndroidManifest at: {defaultAndroidManifest}");
        }

        public static string UltraSdkCfgXmlPath()
        {
            string defaultAndroidManifest = Path.Combine(GetCurSourceFileAbsDir, "DefaultUltraSdkCfg.xml");
            if (File.Exists(defaultAndroidManifest))
            {
                return defaultAndroidManifest;
            }
            throw new Exception($"can not find default DefaultUltraSdkCfg at: {defaultAndroidManifest}");
        }

        public static void InitUnityUltraSdkCfgXml(string ultrasdkcfgXml,bool overwrite = false)
        {
            File.Copy(UltraSdkCfgXmlPath(), ultrasdkcfgXml, overwrite);
        }
    }
}