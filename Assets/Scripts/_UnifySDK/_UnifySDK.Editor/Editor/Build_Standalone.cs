using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static partial class Build
{
    [MenuItem("Tools/Build/Build Windows")]
    public static void BuildWindows()
    {
        //CommandLineUtils.Run("echo ", EnvironmentUtils.Get("BuildBundleType", string.Empty));
        //CommandLineUtils.Run("echo ", EnvironmentUtils.Get("GenLuaWrap", string.Empty));
        //CommandLineUtils.Run("echo ", EnvironmentUtils.Get("BuildNumber", "null"));
        SetGameProperties();
        BuildBundle();
        GenerateLuaWrap();
        BuildWindowsPlayer();

        RebackScenes();
    }

    [MenuItem("Tools/Build/Build Mac")]
    public static void BuildMac()
    {
        //CommandLineUtils.Run("echo ", EnvironmentUtils.Get("BuildBundleType", string.Empty));
        //CommandLineUtils.Run("echo ", EnvironmentUtils.Get("GenLuaWrap", string.Empty));
        //CommandLineUtils.Run("echo ", EnvironmentUtils.Get("BuildNumber", "null"));
        //BuildBundle();
        //GenerateLuaWrap();
        BuildWindowsPlayer();
    }

    public static void BuildWindowsPlayer()
    {
        string windowsPath = tmpPath + "/windows";
        string windowsExe = windowsPath + "/4wd";
     

        if (Directory.Exists(windowsPath))
            Directory.Delete(windowsPath, true);

        string dir = Path.GetDirectoryName(windowsPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        //PlayerSettings.Android.keyaliasPass = "Admin@123";  
        //PlayerSettings.Android.keystorePass = "Admin@123";
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
        {
            new EditorBuildSettingsScene("Assets/Scenes/StartScene.unity", true),
        };

        //EditorUserBuildSettings.development = false;

        try
        {
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, windowsExe, StandaloneType, BuildOptions.CompressWithLz4HC);

        }
        catch (Exception e)
        {
            throw new Exception(e.ToString());
        }

        if (!Directory.Exists(windowsPath))
        {
            throw new System.Exception("Failed to build windows! windowsPath = " + windowsPath);
        }
        else
        {
            string destPath = Path.Combine(targetPath, "Windows/Windows_" + GetBuildNumber + "_4wd.zip");
            if (StandaloneType == BuildTarget.StandaloneOSX)
                destPath = Path.Combine(targetPath, "Mac/Mac_" + GetBuildNumber + "_4wd.zip");

            string destDir = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            UnityEngine.Debug.LogError(destDir + ", " + targetPath);

            CommandLineUtils.Run("cd " + windowsPath + "\n zip -r ", destPath + " ./*" );

            //if (!Directory.Exists(destPath))
            //    Directory.CreateDirectory(destPath);
            //File.Copy(windowsPath + ".zip", destPath, true);
        }
    }

    public static BuildTarget StandaloneType
    { 
        get
        {
            string buildBundleType = EnvironmentUtils.Get("StandaloneType", string.Empty);
            if (buildBundleType == "Windows")
            {
                return BuildTarget.StandaloneWindows64;

            }
            return BuildTarget.StandaloneOSX;

        }
    }
}
