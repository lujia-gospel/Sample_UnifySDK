using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
#if UNITY_IPHONE
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnifySDK;
using UnityEngine;

#if UNITY_IPHONE
public class XCodeProjectMod
{

    [UnityEditor.Callbacks.PostProcessBuild(999)]
    public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
    {
        if (BuildTarget == BuildTarget.iOS)
        {
            Debug.Log(path);
            string projPath = UnityEditor.iOS.Xcode.Custom.PBXProject.GetPBXProjectPath(path);
            PBXProject pbxProject = new PBXProject();

            pbxProject.ReadFromString(File.ReadAllText(projPath));
            string targetGuid = pbxProject.GetUnityMainTargetGuid();

            AddCapbility(pbxProject, projPath, targetGuid);

            //Handle xcodeproj  
            // File.Copy(Application.dataPath + "/Editor/xcodeapi/Res/KeychainAccessGroups.plist",path+"/KeychainAccessGroups.plist",true);  

            // proj.AddFile ("KeychainAccessGroups.plist", "KeychainAccessGroups.plist");  

            //var codesign = Debug.isDebugBuild ? CODE_SIGN_DEVELOPER : CODE_SIGN_DISTRIBUTION;  
            //var provision = Debug.isDebugBuild ? PROVISIONING_DEVELOPER : PROVISIONING_DISTRIBUTION;  
            // 添加flag
            //pbxProject.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
            //proj.SetBuildProperty (target, "CODE_SIGN_IDENTITY", codesign); 

            pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-lz");
            pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
            pbxProject.AddBuildProperty(targetGuid, "GCC_PREPROCESSOR_DEFINITIONS", "ENABLE_DOWNLOADER=1");

            //proj.SetBuildProperty(target,"CODE_SIGN_ENTITLEMENTS","KeychainAccessGroups.plist");  
            pbxProject.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");


            //string targetName = PBXProject.GetUnityTargetName();
            //string guid = pbxProject.TargetGuidByName(targetName);

            pbxProject.SetBuildProperty(targetGuid, "USYM_UPLOAD_AUTH_TOKEN", "FakeToken");
            pbxProject.SetBuildProperty(targetGuid, "USYM_UPLOAD_URL_SOURCE",
                "https://perf-events.cloud.unity3d.com/url");
            pbxProject.SetBuildProperty(targetGuid, "DEBUG_INFORMATION_FORMAT", "dwarf-with-dsym");


            //pbxProject.AddBuildProperty(target, "OTHER_LINKER_FLAGS", "-ObjC");
            //pbxProject.AddBuildProperty(target, "OTHER_LINKER_Flags", "-ObjC -Wl,-sectcreate,__RESTRICT,__restrict,/dev/null");
            pbxProject.AddBuildProperty(targetGuid, "FRAMEWORK_SEARCH_PATHS",
                "$(PLATFORM_DIR)/Developer/Library/Frameworks");


            // framework
            //pbxProject.AddFrameworkToProject(target, "Security.framework", false);


            /*---------------------NetEaseSdk------------------*/
            pbxProject.SetBuildProperty(targetGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");

            string targetFrameGuid = pbxProject.GetUnityFrameworkTargetGuid();
            pbxProject.SetBuildProperty(targetFrameGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");
            pbxProject.SetBuildProperty(targetFrameGuid, "ENABLE_BITCODE", "NO");
            pbxProject.AddBuildProperty(targetFrameGuid, "GCC_PREPROCESSOR_DEFINITIONS", "ENABLE_DOWNLOADER=1");


            //iPhone Distribution: Beijing Quark Molecular Network Technology Co., LTD (9QG859R5P2)
            //pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY", "iPhone Developer");
            pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY", "iPhone Developer: 楠 徐 (H5B46RP4MY)");
            pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY[sdk=iphoneos*]",
                "iPhone Developer: 楠 徐 (H5B46RP4MY)");
            pbxProject.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE", "aee54b4a-1e30-4a6f-9b18-263c6b8834cb");
            pbxProject.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE_SPECIFIER", "fourwd-dev");

            pbxProject.SetTeamId(targetGuid, "5D5MNTC2S2");


            //添加lib
            AddLibToProject(pbxProject, targetFrameGuid, "libsqlite3.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libsqlite3.0.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libxml2.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libz.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libresolv.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libc++.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libc.tbd");
            AddLibToProject(pbxProject, targetFrameGuid, "libresolv.9.tbd");


            /*---------------------NetEaseSdk------------------*/


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

            AddSystemFrameToProject(pbxProject, targetFrameGuid, "Accelerate.framework"); //2.15.0版本后Facebook需要

            AddSystemFrameToProject(pbxProject, targetFrameGuid, "MobileCoreServices.framework"); //AF用

            AddSystemFrameToProject(pbxProject, targetFrameGuid, "UserNotifications.framework"); //推送

            AddSystemFrameToProject(pbxProject, targetFrameGuid, "iAd.framework"); //广告


            /*---------------------NetEaseAppdump------------------*/
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "MessageUI.framework"); //获取 UDID 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "SystemConfiguration.framework"); //获取 UDID 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "CoreTelephony.framework"); //获取 UDID 需要

            /*---------------------NetEaseSdk------------------*/

            // 应用修改
            File.WriteAllText(projPath, pbxProject.WriteToString());


            ////// ファイルを追加
            //var fileName = "Editor/Xcode/Native/NativePlatform.mm";
            //var filePath = Path.Combine("Assets/", fileName);
            //string copyToPath = Path.Combine(path, fileName);
            //if (!Directory.Exists(Path.GetDirectoryName(copyToPath)))
            //{
            //    Directory.CreateDirectory(Path.GetDirectoryName(copyToPath));
            //}

            //if (!File.Exists(copyToPath))
            //    File.Copy(filePath, copyToPath);
            //pbxProject.AddFileToBuild(target, pbxProject.AddFile(fileName, fileName, PBXSourceTree.Source));


            //Handle plist  
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            PlistElementDict rootDict = plist.root;
            // rootDict.SetString ("CFBundleVersion", GetVer ());//GetVer() 返回自定义自增值  
            // 插入array
            PlistElementArray urlInnerArray = rootDict.CreateArray("CFBundleURLTypes");

            PlistElementDict item0 = urlInnerArray.AddDict();

            PlistElementArray URLSchemes = item0.CreateArray("CFBundleURLSchemes");


            item0.SetString("CFBundleTypeRole", "Editor");
            //item0.SetString("CFBundleURLName", "ma122naxx1hmt://");
            //URLSchemes.AddString("ma122naxx1hmt://");
            URLSchemes.AddString("fb524292212420636");

            rootDict.SetString("FacebookAppID", "524292212420636");
            rootDict.SetBoolean("FacebookAutoLogAppEventsEnabled", true);
            rootDict.SetString("FacebookDisplayName", "爆走兄弟：疾速奔跑");
            rootDict.SetString("FacebookClientToken", "33e3deab2ef43ff41584e58719f7f266");

            PlistElementArray lsApplicationQueriesSchemes = rootDict.CreateArray("LSApplicationQueriesSchemes");
            lsApplicationQueriesSchemes.AddString("fbauth");
            lsApplicationQueriesSchemes.AddString("fbapi");
            lsApplicationQueriesSchemes.AddString("fbshareextension");
            lsApplicationQueriesSchemes.AddString("fbauth2");
            lsApplicationQueriesSchemes.AddString("fb - messenger - share - api");


            // 应用修改
            //rootDict.SetString("Privacy - Media Library Usage Description", "允许此权限才能访问：媒体资料库");
            //rootDict.SetString("Privacy - Health Share Usage Description", "允许此权限才能访问：健康数据分享");
            //rootDict.SetString("Privacy - Health Update Usage Description", "允许此权限才能访问：健康更新");
            //rootDict.SetString("Privacy - Location Always and When In Use Usage Description", "允许此权限才能启用：定位功能");
            //rootDict.SetString("Privacy - Location Usage Description", "允许此权限才能启用：定位功能");
            //rootDict.SetString("Privacy - Motion Usage Description", "允许此权限才能访问：运动与健康");
            rootDict.SetString("Privacy - Photo Library Additions Usage Description", "允许此权限才能访问：相册");
            //rootDict.SetString("Privacy - Reminders Usage Description", "允许此权限才能访问：提醒事项");

            //rootDict.SetString("Privacy - Microphone Usage Description", "允许此权限才能访问：麦克风");

            //rootDict.SetString("NSPhotoLibraryUsageDescription", "App需要您的同意,才能访问相册");
            rootDict.SetString("Privacy - Camera Usage Description", "允许此权限才能访问：相机");
            //rootDict.SetString("Privacy - Contacts Usage Description", "允许此权限才能访问：通讯录");
            //rootDict.SetString("Privacy - Location Always Usage Description", "允许此权限才能启用：定位功能");
            //rootDict.SetString("Privacy - Bluetooth Peripheral Usage Description", "允许此权限才能访问：蓝牙");
            //rootDict.SetString("NSLocationWhenInUseUsageDescription", "使用时获取地理位置");
            rootDict.SetString("Privacy - Calendars Usage Description", "允许此权限才能访问：日历");
            rootDict.SetString("ITSAppUsesNonExemptEncryption", "false");
            rootDict.SetBoolean("UIRequiresFullScreen", true);
            rootDict.SetBoolean("UIFileSharingEnabled", true);
            rootDict.SetBoolean("RegisterRemoteNotification", true);

            /*---------------------NetEaseSdk------------------*/
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

            /*---------------------NetEaseSdk------------------*/


            PlistElementDict pedic = rootDict.CreateDict("App Transport Security Settings");
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
            string copyToPath = Path.Combine(xcodeProPath, fileInXcodePath);
            Debug.Log("拷贝到xcode工程中的路径-----" + copyToPath);
            //if (!Directory.Exists(Path.GetDirectoryName(copyToPath)))
            //{
            //    Directory.CreateDirectory(Path.GetDirectoryName(copyToPath));
            //}

            //if (!File.Exists(copyToPath))
            File.Copy(fileInUnityPaths, copyToPath, true);

            string targetFileGuid = pbxProject.AddFile(fileInXcodePath, fileInXcodePath, PBXSourceTree.Source);
            pbxProject.AddFileToBuild(targetMainGuid, targetFileGuid);
        }

        static void AddBundleFile(string fileInXcodePath, string xcodeProPath, PBXProject pbxProject,
            string targetMainGuid)
        {
            string copyToPath = Path.Combine(xcodeProPath, fileInXcodePath);
            Debug.Log("拷贝到xcode工程中Bundle的路径-----" + copyToPath);

            string targetFileGuid = pbxProject.AddFile(fileInXcodePath, fileInXcodePath, PBXSourceTree.Source);
            pbxProject.AddFileToBuild(targetMainGuid,
                pbxProject.AddFile(fileInXcodePath, fileInXcodePath, PBXSourceTree.Source));
        }

//添加lib方法
        static void AddLibToProject(PBXProject inst, string targetGuid, string lib)
        {
            string fileGuid = inst.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
            inst.AddFileToBuild(targetGuid, fileGuid);
        }

//添加framework方法
        static void AddSystemFrameToProject(PBXProject inst, string targetGuid, string framework,
            bool isOptional = false)
        {
            inst.AddFrameworkToProject(targetGuid, framework, isOptional);
        }

        static void CopyAndReplaceDirectory(string srcPath, string dstPath)
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

        static void AddCapbility(PBXProject pbxProject, string projPath, string targetGuid)
        {
            ProjectCapabilityManager projectCapabilityManager =
                new ProjectCapabilityManager(projPath, "Unity-iPhone/NetEaseFuncReleaseForRunning.entitlements",
                    "Unity-iPhone", targetGuid);

            projectCapabilityManager.AddSignInWithApple();

            // pbxProject.AddCapability(targetGuid, PBXCapabilityType.BackgroundModes);
            // projectCapabilityManager.AddBackgroundModes(BackgroundModesOptions.BackgroundFetch );
            projectCapabilityManager.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);

            //projectCapabilityManager.AddPushNotifications(true);


            if (!string.IsNullOrEmpty(
                EnvironmentVariableSettings.Instance.GetSDKValue(UnifySDKType.QuickSDK.ToString())))
            {
                string[] domains = {"applinks:4wdhd.qmolgame.com"};
                projectCapabilityManager.AddAssociatedDomains(domains);
                projectCapabilityManager.AddAssociatedDomains(domains);
            }

            projectCapabilityManager.WriteToFile();

            pbxProject.AddFileToBuild(targetGuid,
                pbxProject.AddFile("Unity-iPhone/NetEaseFuncReleaseForRunning.entitlements",
                    "NetEaseFuncReleaseForRunning.entitlements", PBXSourceTree.Source));

            pbxProject.SetBuildProperty(targetGuid, "CODE_SIGN_ENTITLEMENTS",
                "Unity-iPhone/NetEaseFuncReleaseForRunning.entitlements");
        }
    }
}
#endif