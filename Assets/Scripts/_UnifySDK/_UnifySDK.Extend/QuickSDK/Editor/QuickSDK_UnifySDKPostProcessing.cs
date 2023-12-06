using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UnifySDK
{
    class QuickSDK_UnifySDKPostProcessing : IPostprocessBuildWithReport
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
            // EditorCode(path);
        }

        private static void EditorCode(string filePath)
        {
            // //读取UnityAppController.mm文件
            // var UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");
            // UnityAppController.WriteBelow("#include <sys/sysctl.h>", "#import \"QuickSDK_ios.h\"");
            // UnityAppController.WriteBelow("#include <sys/sysctl.h>", "#import <SMPCQuickSDK/SMPCQuickSDK.h> ");
            //
            // UnityAppController.WriteBelow("[KeyboardDelegate Initialize];",
            //     "[[QuickSDK_ios shareInstance] addNotifications];SMPCQuickSDKInitConfigure *cfg =[[SMPCQuickSDKInitConfigure alloc] init];cfg.productKey = @\"37359213\";cfg.productCode = @\"61942186438665891722635121162189\";int error = [[SMPCQuickSDK defaultInstance] initWithConfig:cfg application:application didFinishLaunchingWithOptions:launchOptions];if (error != 0){NSLog(@\"初始化失败：%d\",error);}");
        }
#endif
    }
}