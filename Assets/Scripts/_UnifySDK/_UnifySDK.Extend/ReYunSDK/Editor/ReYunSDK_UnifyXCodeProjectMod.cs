using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace UnifySDK
{
    public class ReYunSDK_UnifyXCodeProjectMod
    {
        [UnityEditor.Callbacks.PostProcessBuild(999)]
        public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
        {
            if (BuildTarget != BuildTarget.iOS) return;
            Debug.Log(path);
            var projPath = PBXProject.GetPBXProjectPath(path);
            var pbxProject = new PBXProject();
            pbxProject.ReadFromString(File.ReadAllText(projPath));
            var targetFrameGuid = pbxProject.GetUnityFrameworkTargetGuid();

            //添加lib 这些一般 sdk 都有了 这些不重复添加了
            // pbxProject.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
            // pbxProject.AddBuildProperty(targetFrameGuid, "OTHER_LDFLAGS", "-ObjC");
            // AddLibToProject(pbxProject, targetFrameGuid, "libsqlite3.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libsqlite3.0.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libxml2.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libz.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libresolv.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libc++.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libc.tbd");
            // AddLibToProject(pbxProject, targetFrameGuid, "libresolv.9.tbd");

            AddSystemFrameToProject(pbxProject, targetFrameGuid, "Security.framework"); //ReYun 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "CoreTelephony.framework"); //ReYun 需要 
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "SystemConfiguration.framework"); //ReYun 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "CoreMotion.framework"); //ReYun 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "AdServices.framework", true); //ReYun 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "AVFoundation.framework"); //ReYun 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "CFNetwork.framework"); //ReYun 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "AdSupport.framework"); //获取 UDID 需要
            AddSystemFrameToProject(pbxProject, targetFrameGuid, "WebKit.framework"); //ReYun 需要
            // 应用修改
            File.WriteAllText(projPath, pbxProject.WriteToString());
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
    }
}