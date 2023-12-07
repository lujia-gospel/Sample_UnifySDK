using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.XCodeEditor;
using UnityEngine;

class XCodePostProcess : IPostprocessBuildWithReport
{
#if UNITY_EDITOR
    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        Debug.Log("MyCustomBuildProcessor.OnPostprocessBuild for target " + report.summary.platform + " at path " +
                  report.summary.outputPath);
        if (report.summary.platform != BuildTarget.iOS)
        {
            Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        //得到xcode工程的路径
        var path = Path.GetFullPath(report.summary.outputPath);

        // //编辑代码文件
        // var propertiesObj = GameObject.Find("GameProperties");
        // var properties = propertiesObj.GetComponent<GameProperties>();
        // if (properties.IsUseQuickSDK)
        //     EditorCode(path);
        // //编辑代码文件
        // if (properties.IsUseUltraSdk)
            // EditorCodeUltra(path);
    }

    private static void EditorCode(string filePath)
    {
        //  =======================推送相关代码begin=============================
        //读取UnityAppController.mm文件
        var UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");
        //在指定代码中替换一行示例
        //UnityAppController.Replace("return YES;","return NO");
        //在指定代码后面增加一行
        UnityAppController.WriteBelow("#include <sys/sysctl.h>",
            "#import <JYouLoginKit/REDeLoginKit.h>");
        UnityAppController.WriteBelow("UnitySendDeviceToken(deviceToken);",
            "   [REDeLoginKit applicationRegisterForRemoteNotificationsWithDeviceToken:deviceToken];");
        //在指定代码后面增加一行
        UnityAppController.WriteBelow("UnitySendRemoteNotificationError(error);",
            "   [REDeLoginKit applicationdidFailToRegisterForRemoteNotifications];");
        //读取Preprocessorh.h文件
        var Preprocessorh = new XClass(filePath + "/Classes/Preprocessor.h");
        //在指定代码中替换一行
        Preprocessorh.Replace("#define UNITY_USES_REMOTE_NOTIFICATIONS 0", "#define UNITY_USES_REMOTE_NOTIFICATIONS 1");
        //  =======================推送相关代码end=============================
    }

    private static void EditorCodeUltra(string filePath)
    {
        //读取UnityAppController.mm文件
        var UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");
        //在指定代码中替换一行示例
        //UnityAppController.Replace("return YES;","return NO");
        //在指定代码后面增加一行
        UnityAppController.WriteBelow("#include <sys/sysctl.h>",
            "#import <AppTrackingTransparency/AppTrackingTransparency.h>");
        //在指定代码后面增加一行
        UnityAppController.WriteBelow("_didResignActive = false;",
            "    if (@available(iOS 14, *)) {[ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {}];}");
    }
#endif
}