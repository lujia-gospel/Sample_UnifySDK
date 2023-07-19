using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnifySDK;
using UnifySDK.Editor;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class SDKEditor 
{
    [MenuItem("Tool/Unify/TestBuild")]
    static void TestBuild()
    {
        UnifySDKAssetsEditor.BuildBeforeClearSDKAssets();
        //string path = "Assets/Scripts/_UnifySDK.Editor/SDKAssetsSetting/Crasheye.asset";

        for (int i = 0; i < Math.Min(BuildArgumentsSettings.Instance.Keys.Count, BuildArgumentsSettings.Instance.Values.Count); i++)
        {
            var assetsSetting =   UnifySDKAssetsSetting.GetUnifySDKAssetsSetting(BuildArgumentsSettings.Instance.Keys[i]);
            UnifySDKAssetsEditor.MoveToProject(assetsSetting);
        }
        return;
        //var assetsSetting = AssetDatabase.LoadAssetAtPath<UnifySDKAssetsSetting>(path);
        

        var apkPath = "./APK/Test123.apk";
        BuildReport buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, apkPath,
            BuildTarget.Android,
            BuildOptions.CompressWithLz4HC);
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 拷贝SDK内容
    /// </summary>
    /// <param name="sdkName"></param>
    /// <returns></returns>
    private static bool CopySDK(string sdkdirpath, string sdkName, bool copyDef = true)
    {
        try
        {
            string sdkPath = sdkdirpath + "/" + sdkName;
            string defPath = sdkdirpath + "/" + "DefSDK/";
            string androidSDKPath = Application.dataPath + "/plugins/Android";

            if (!Directory.Exists(defPath))
            {
                Debug.LogError(defPath + " 不存在");
                return false;
            }

            if (!Directory.Exists(sdkPath))
            {
                Debug.LogError(sdkPath + " 不存在");
                return false;
            }

            DeleteDirectory(androidSDKPath);
            if (copyDef)
            {
                CopyDirectory(defPath, androidSDKPath, true);
            }

             CopyDirectory(sdkPath, androidSDKPath, true);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.StackTrace);
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// 删除指定目录中所有内容
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Debug.LogError("The dir is not exist.");
            return;
        }

        Directory.Delete(path, true);
    }

    /// <summary>
    /// 拷贝指定文件夹中所有内容到指定目录
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    public static void CopyDirectory(string sourceDirName, string destDirName, bool overwrite)
    {

        if (!Directory.Exists(sourceDirName))
        {
            Debug.LogError("The src dir is not exist.");
            return;
        }

        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
            File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
        }

        if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
            destDirName = destDirName + Path.DirectorySeparatorChar;

        string[] files = Directory.GetFiles(sourceDirName);
        foreach (string file in files)
        {
            File.Copy(file, destDirName + Path.GetFileName(file), true);
            File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
        }

        string[] dirs = Directory.GetDirectories(sourceDirName);
        foreach (string dir in dirs)
        {
            CopyDirectory(dir, destDirName + Path.GetFileName(dir), overwrite);
        }
    }
}
