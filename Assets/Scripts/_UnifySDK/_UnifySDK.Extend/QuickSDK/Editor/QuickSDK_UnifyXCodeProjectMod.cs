using System.IO;
using PlasticGui;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEngine;


namespace UnifySDK
{
    public class QuickSDK_UnifyXCodeProjectMod
    {
//#if UNITY_IPHONE
        [UnityEditor.Callbacks.PostProcessBuild(999)]
        public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
        {
            if (BuildTarget != BuildTarget.iOS) return;
            Debug.Log(path);
            var projPath = PBXProject.GetPBXProjectPath(path);
            var pbxProject = new PBXProject();
            pbxProject.ReadFromString(File.ReadAllText(projPath));
            var targetGuid = pbxProject.GetUnityMainTargetGuid();
            AddCapbility(pbxProject, projPath, targetGuid);

            pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-lz");
            pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
            pbxProject.AddBuildProperty(targetGuid, "GCC_PREPROCESSOR_DEFINITIONS", "ENABLE_DOWNLOADER=1");
            pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
            pbxProject.AddBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS",
                "$(PLATFORM_DIR)/Developer/Library/Frameworks");

            // 证书
            pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY", "iPhone Developer: Nan Xu (8MM8764MCJ)");
            pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY[sdk=iphoneos*]",
                 "iPhone Developer: Nan Xu (8MM8764MCJ)");
            pbxProject.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE", "a10c98ae-d204-465e-86ed-3b50b56706d4");
            pbxProject.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE_SPECIFIER", "qmgame_developer");
            pbxProject.SetTeamId(targetGuid, "9QG859R5P2");


            var targetFrameGuid = pbxProject.GetUnityFrameworkTargetGuid();


            //AddFile("Assets/Plugins/iOS/QuickSDK/libSMPCQuickChannel.a",
            //  "Libraries/libSMPCQuickChannel.a", path, pbxProject, targetGuid);
            //AddFile("Assets/Plugins/iOS/QuickSDK/SMPCQuickSDK.framework",
            // "Frameworks/SMPCQuickSDK.framework", path, pbxProject, targetFrameGuid);

            // var libSMPCQuickChannel =
            //     pbxProject.AddFile(
            //         "Libraries/Plugins/iOS/QuickSDK/libSMPCQuickChannel.a",
            //         "Libraries/libSMPCQuickChannel.a", PBXSourceTree.Source);
            // pbxProject.AddFileToBuild(targetFrameGuid, libSMPCQuickChannel);
            // var SMPCQuickSDK =
            //     pbxProject.AddFile("Frameworks/Plugins/iOS/QuickSDK/JySDK.framework",
            //         "Frameworks/JySDK.framework", PBXSourceTree.Source);
            // pbxProject.AddFileToEmbedFrameworks(targetFrameGuid, SMPCQuickSDK);
            
            AddBundleFile("Frameworks/Plugins/iOS/jywlRes.bundle", path,
                pbxProject, targetGuid);

            pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
            pbxProject.AddBuildProperty(targetFrameGuid, "OTHER_LDFLAGS", "-ObjC");
            //添加lib
            AddLibToProject(pbxProject, targetFrameGuid, "libsqlite3.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libsqlite3.0.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libxml2.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libz.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libresolv.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libc++.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libc.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libresolv.9.tbd");

            AddSystemFrameToProject(pbxProject, targetFrameGuid, "AdSupport.framework"); //获取 UDID 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "StoreKit.framework"); //IAP 框架，支付需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "SystemConfiguration.framework");
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "AppTrackingTransparency.framework", true); //IDFA获取权限
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "UIKit.framework",
                true); //需要设置成 optional，否则在 iOS 7 以下的版本会出现 App 崩溃
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "Foundation.framework",
                true); //需要设置成 optional，否则在 iOS 7 以下的版本会出现 App 崩溃
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "WebKit.framework",
                true); //需要设置成 optional，否则在 iOS 7 以下的版本会出现 App 崩溃
            // AddSystemFrameToProject(pbxProject, targetFrameGuid, "Accelerate.framework"); //2.15.0版本后Facebook需要
            // AddSystemFrameToProject(pbxProject, targetFrameGuid, "MobileCoreServices.framework"); //AF用
            // AddSystemFrameToProject(pbxProject, targetFrameGuid, "UserNotifications.framework"); //推送
            // AddSystemFrameToProject(pbxProject, targetFrameGuid, "iAd.framework"); //广告  
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "MessageUI.framework"); //获取 UDID 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "SystemConfiguration.framework"); //获取 UDID 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "CoreTelephony.framework"); //获取 UDID 需要
            // 应用修改
            File.WriteAllText(projPath, pbxProject.WriteToString());
            var plistPath = path + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            var rootDict = plist.root;
            var urlInnerArray = rootDict.CreateArray("CFBundleURLTypes");
            var item0 = urlInnerArray.AddDict();
            rootDict.SetString("Privacy - Photo Library Additions Usage Description", "允许此权限才能访问：相册");
            rootDict.SetString("Privacy - Camera Usage Description", "允许此权限才能访问：相机");
            rootDict.SetString("Privacy - Calendars Usage Description", "允许此权限才能访问：日历");
            rootDict.SetString("ITSAppUsesNonExemptEncryption", "false");
            rootDict.SetBoolean("UIRequiresFullScreen", true);
            rootDict.SetBoolean("UIFileSharingEnabled", true);
            rootDict.SetBoolean("RegisterRemoteNotification", true);
            rootDict.SetString("UILaunchStoryboardName", "iOS 14 + 放大模式的适配"); //必须
            rootDict.SetString("NSPhotoLibraryUsageDescription", "App需要您的同意,才能访问相册"); //必须
            rootDict.SetString("NSPhotoLibraryAddUsageDescription", "App需要您的同意,才能访问相册"); //必须
            rootDict.SetString("NSCameraUsageDescription", "允许此权限才能访问：相机"); //扫码功能使用
            rootDict.SetString("NSMicrophoneUsageDescription", "允许此权限才能访问：麦克风"); //聊天功能使用
            rootDict.SetString("NSSpeechRecognitionUsageDescription", "允许此权限才能访问：语音识别"); //语音识别功能使用
            rootDict.SetString("NSLocationWhenInUseUsageDescription", "允许此权限才能访问：定位"); //LBS相关功能使用
            rootDict.SetString("NSFaceIDUsageDescription", "允许此权限才能访问：Face ID"); //可选
            rootDict.SetString("NSContactsUsageDescription", "允许此权限才能访问：通讯录"); //可选
            rootDict.SetString("NSBluetoothAlwaysUsageDescription", "允许此权限才能访问：蓝牙"); //可选
            rootDict.SetString("NSUserTrackingUsageDescription",
                "获取IDFA标记权限向您提供更优质、安全的个性化服务及内容；开启后，您也可以前往系统“设置-隐私 ”中随时关闭"); //可选  
            var pedic = rootDict.CreateDict("App Transport Security Settings");
            pedic.SetString("Allow Arbitrary Loads", "YES");
            plist.WriteToFile(plistPath);
        }

        /// <summary>
        /// 给xcode工程添加文件
        /// </summary>
        /// <param name="fileInUnityPaths">文件的路径，可以放在unity下，也可以放在其他路径</param>
        /// <param name="fileInXcodePath">需要拷贝到xcode中的那个路径，是相对于xcode工程来说的相对路径</param>
        /// <param name="xcodeProPath">xcode工程路径</param>
        /// <param name="pbxProject"></param>
        /// <param name="targetMainGuid">Unity-iPhone 的GUID</param>
        static void AddFile(string fileInUnityPaths, string fileInXcodePath, string xcodeProPath, PBXProject pbxProject,
            string targetMainGuid)
        {
            Debug.Log("filePath-----" + fileInUnityPaths);
            var copyToPath = Path.Combine(xcodeProPath, fileInXcodePath);
            Debug.Log("拷贝到xcode工程中的路径-----" + copyToPath);
            File.Copy(fileInUnityPaths, copyToPath, true);

            var targetFileGuid = pbxProject.AddFile(fileInXcodePath, fileInXcodePath, PBXSourceTree.Source);
            pbxProject.AddFileToBuild(targetMainGuid, targetFileGuid);
        }

        static void AddBundleFile(string fileInXcodePath, string xcodeProPath, PBXProject pbxProject,
            string targetMainGuid)
        {
            var copyToPath = Path.Combine(xcodeProPath, fileInXcodePath);
            Debug.Log("拷贝到xcode工程中Bundle的路径-----" + copyToPath);

            var targetFileGuid = pbxProject.AddFile(fileInXcodePath, fileInXcodePath, PBXSourceTree.Source);
            pbxProject.AddFileToBuild(targetMainGuid,
                pbxProject.AddFile(fileInXcodePath, fileInXcodePath, PBXSourceTree.Source));
        }

        //添加lib方法
        static void AddLibToProject(PBXProject inst, string targetGuid, string lib)
        {
            var fileGuid = inst.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
            inst.AddFileToBuild(targetGuid, fileGuid);
        }

        //添加framework方法
        static void AddSystemFrameToProject(PBXProject inst, string targetGuid, string framework,
            bool isOptional = false)
        {
            inst.AddFrameworkToProject(targetGuid, framework, isOptional);
        }

        internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
        {
            //路径下该文件夹若存在，则删除
            if (Directory.Exists(dstPath))
            {
                Directory.Delete(dstPath);
            }

            //路径下的文件若存在，则删除
            if (File.Exists(dstPath))
            {
                File.Delete(dstPath);
            }

            //创建该路径下文件夹
            Directory.CreateDirectory(dstPath);

            foreach (var file in Directory.GetFiles(srcPath))
                File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

            foreach (var dir in Directory.GetDirectories(srcPath))
                CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
        }

        private static void AddCapbility(PBXProject pbxProject, string projPath, string targetGuid)
        {
            var projectCapabilityManager =
                new ProjectCapabilityManager(projPath, "Unity-iPhone/NetEaseFuncReleaseForRunning.entitlements",
                    "Unity-iPhone", targetGuid);

            projectCapabilityManager.AddSignInWithApple();
            projectCapabilityManager.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);

            // projectCapabilityManager.AddPushNotifications(true);
            string[] domains = {"applinks:fourwdtw.onelink.me", "applinks:www.fourwdtw.onelink.me"};
            projectCapabilityManager.AddAssociatedDomains(domains);


            projectCapabilityManager.WriteToFile();

            pbxProject.AddFileToBuild(targetGuid,
                pbxProject.AddFile("Unity-iPhone/NetEaseFuncReleaseForRunning.entitlements",
                    "NetEaseFuncReleaseForRunning.entitlements", PBXSourceTree.Source));

            pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_ENTITLEMENTS",
                "Unity-iPhone/NetEaseFuncReleaseForRunning.entitlements");
        }

// #endif
    }
}