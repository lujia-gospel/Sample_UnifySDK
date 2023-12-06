using System;
using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;

namespace com.ultrasdk.unity.native.Editor
{
    public class UltraSDKiOSEditor : UnityEditor.Editor
    {
        private static bool _verbose;
        private const string SDKName = "UltraSDK";
        private readonly string m_BuildPath;
        private readonly string m_TargetGuid;
        private readonly string m_PBXProjectPath;
        private readonly string m_EntitlementFilePath;
        private PlistDocument m_Entitlements;
        private PlistDocument m_InfoPlist;
        protected internal PBXProject project;

        public static void SetVerbose(bool verbose)
        {
            _verbose = verbose;
        }

        private static void LOGDebug(string message)
        {
            if (_verbose)
            {
                UnityEngine.Debug.Log($"sdk build DEBUG {SDKName} : {message}");
            }
        }

        private static void LOGInfo(string message)
        {
            UnityEngine.Debug.Log($"sdk build INFO {SDKName} : {message}");
        }

        private static void LOGError(string message)
        {
            UnityEngine.Debug.LogError($"sdk build ERROR {SDKName} : {message}");
        }

        private static void LOGException(string message)
        {
            Exception exception = new Exception($"sdk build Exception {SDKName} : {message}");
            UnityEngine.Debug.LogException(exception);
            EditorApplication.Exit(128);
        }
        public static void iOSBuild(string pathToBuiltProject)
        {
            string nativeVersion = UltraSDKEditorSetting.GetUltraSDKUnityNativeVersion();
            LOGInfo($"UltraSDK Native Version: { nativeVersion } ");
            LOGInfo($"iOS build start");

            // 创建工程设置对象
            string projectPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
            if (!File.Exists(projectPath))
            {
                LOGException($"----------> project.pbxproj exists <------------");
            }
            bool exists = File.Exists(projectPath);
            LOGInfo($" File Exists : { exists }");
            UltraSDkiOSPCManager capabilityManager = new UltraSDkiOSPCManager(projectPath,
                "ultra.entitlements", pathToBuiltProject);
            capabilityManager.WriteToFile();
            
            LOGInfo($"iOS build end");
        }
    }
}