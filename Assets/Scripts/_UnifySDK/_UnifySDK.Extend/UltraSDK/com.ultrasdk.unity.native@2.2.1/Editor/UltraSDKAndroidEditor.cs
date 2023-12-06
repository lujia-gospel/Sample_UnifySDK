using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace com.ultrasdk.unity.native.Editor
{
    public class UltraSDKAndroidEditor : UnityEditor.Editor
    {

        static string GetCurSourceFileAbsDir
        {
            get
            {
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
                return Path.GetDirectoryName(st.GetFrame(0).GetFileName());
            }
        }

        private const string SDKName = "UltraSDK";
        private static bool _verbose;

        /// <summary>
        /// 修改Android清单文件
        /// </summary>
        /// <param name="lanuchMF"></param>
        /// <param name="mainActivityName"></param>
        public static void AndroidBuild (
            string lanuchMF = "Assets/Plugins/Android/AndroidManifest.xml", 
            string mainActivityName = "com.unity.ultra.android.support.UltraSDKUnityPlayerActivity")
        {
            // return;
            Debug.Log("@@@@@@@@@@@@@@@@@@@@@");
            string nativeVersion = UltraSDKEditorSetting.GetUltraSDKUnityNativeVersion();
            LOGInfo($"UltraSDK Native Version: { nativeVersion } ");
 
            LOGInfo($"android build start");
            if (!Directory.Exists("Assets/Plugins/Android/"))
            {
                Directory.CreateDirectory("Assets/Plugins/Android/");
            }
            //编辑器中失效配置文件处理
            string AndroidManifestDisabled = "Assets/Plugins/Android/AndroidManifest.xml.DISABLED";
            //如果存在DISABLED文件，则重命名该文件
            if (File.Exists(AndroidManifestDisabled))
            {
                File.Move(AndroidManifestDisabled,AndroidManifestDisabled.Replace("AndroidManifest.xml.DISABLED","AndroidManifest.xml.Bak"));
            }
            if (!File.Exists(lanuchMF))
            {
                //本地清单文件不存在，则按照模板新建一个只包含application的清单文件
                LOGDebug(
                    $"AndroidManifest not exists: ${lanuchMF} just copy at: {UltraSDKUnityEditorAndMifUtils.UnityManifestPath()}");
                UltraSDKUnityEditorAndMifUtils.InitUnityManifest(lanuchMF);
            }
            
            if (File.Exists(lanuchMF))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(lanuchMF);
                XmlNode manifest = xml.SelectSingleNode("manifest");
                XmlNodeList applications = manifest.SelectNodes("application");

                string androidNSpace = "http://schemas.android.com/apk/res/android";
                string sdkAppName = "com.unity.ultra.android.support.UltraSDKApplication";
                bool HasSdkApplication = false;
                XmlElement appElement = null;
                foreach (XmlElement element in applications)
                {
                    appElement = element ;
                    string appName = element.GetAttribute("name",androidNSpace);
                    if (appName.Equals(sdkAppName))
                    {
                        //已经配好application
                        HasSdkApplication = true;
                        break ;
                    } 
                }
                
                //回写Application
                if (!HasSdkApplication)
                {
                    //修改ApplicationName
                    appElement.SetAttribute("name", androidNSpace, sdkAppName);
                    manifest.AppendChild(appElement);
                    xml.AppendChild(manifest);
                    xml.Save(lanuchMF);
                    LOGDebug($"Save Lanuch MF : {lanuchMF} ");
                }
                
                //判断游戏是否需要使用其他主Activity
                string playerActivityName = "com.unity.ultra.android.support.UltraSDKUnityPlayerActivity";
                if (!playerActivityName.Equals(mainActivityName))
                {
                    XmlNodeList activitys = manifest.SelectNodes("activity");
                    bool HasMainActivity = false;
                    XmlElement tactivityElement = null;
                    foreach (XmlElement activityElement in activitys)
                    {
                        tactivityElement = activityElement;
                        string activityName = activityElement.GetAttribute("name",androidNSpace);
                        if (activityName.Equals(playerActivityName))
                        {
                            HasMainActivity = true; break;
                        }
                    }
                    //回写PlayerActivity
                    if (!HasMainActivity)
                    {
                        tactivityElement.SetAttribute("name", androidNSpace, mainActivityName);
                        appElement.AppendChild(tactivityElement);
                        manifest.AppendChild(appElement);
                        xml.AppendChild(manifest);
                        xml.Save(lanuchMF);
                        LOGDebug($"Save Lanuch MF : {lanuchMF} ");            
                    }
                }
                
                ///修改Assets中数据，都需要刷新
                AssetDatabase.ImportAsset(lanuchMF, ImportAssetOptions.ForceUpdate);
                LOGDebug($"ImportAsset path: ${lanuchMF}");
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                LOGDebug($"Refresh finish");
                LOGInfo($"android build finish");
            }
            else
            {
                LOGException($"AndroidManifest build fail: ${lanuchMF}");
            }
        }
        /// <summary>
        /// 修改Ultrasdkcfg.xml参数
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="productKey"></param>
        /// <param name="mainActivityName"></param>
        public static void UltraSdkConfigXml(string productID, string productKey, string mainActivityName = "com.unity.ultra.android.support.UltraSDKUnityPlayerActivity")
        {
            // return;
            LOGInfo($"android update ultrasdkcfg.xml start");
            if (!Directory.Exists("Assets/Plugins/Android/assets/"))
            {
                Directory.CreateDirectory("Assets/Plugins/Android/assets/");
            }
            //编辑器中失效配置文件处理
            string ultrasdkcfgXml = "Assets/Plugins/Android/assets/ultrasdkcfg.xml";
            //如果不存在，则需要新建一个配置文件
            if (!File.Exists(ultrasdkcfgXml))
            {
                LOGDebug(
                    $"ultrasdkcfg.xml not exists: just copy at: {UltraSDKUnityEditorAndMifUtils.UltraSdkCfgXmlPath()}");
                UltraSDKUnityEditorAndMifUtils.InitUnityUltraSdkCfgXml(ultrasdkcfgXml);
            }
            XmlDocument xml = new XmlDocument();
            xml.Load(ultrasdkcfgXml);
            XmlNode resources = xml.SelectSingleNode("resources");
            XmlNodeList nodeList = resources.SelectNodes("string");
            XmlElement newResources = xml.CreateElement("resources");
            foreach (XmlElement element in nodeList)
            {
                string attribute = element.GetAttribute("name");
                if (attribute.Equals("_hu_pid_"))
                {
                    //修改产品ID
                    element.InnerText = productID;
                }
                if (attribute.Equals("_hu_pk_"))
                {
                    //修改产品Key
                    element.InnerText = productKey;
                }
                if (attribute.Equals("_hu_activity_"))
                {
                    //修改主ActivityName
                    element.InnerText = mainActivityName;
                }
                newResources.AppendChild(element);
            }
            //Todo。声明时间，注释、帮助文档
            xml.RemoveChild(resources);
            xml.AppendChild(newResources);
            SaveXmlWithUTF8NotBOM(ultrasdkcfgXml, xml);
            
            LOGDebug($"Save ultrasdkCfg MF : {ultrasdkcfgXml} ");
            LOGInfo($"android update ultrasdkcfg.xml end");
        }
        public static void SaveXmlWithUTF8NotBOM(string savePath, XmlDocument xml)
        {
            StreamWriter sw = new StreamWriter(savePath, false, new UTF8Encoding(false));
            xml.Save(sw);
            sw.WriteLine();
            sw.Close();
        }
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

        /// <summary>
        /// skip unity meta files
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        private static void NoMetaDirectoryCopy(string sourceDirName, string destDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
                return;

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.FullName.EndsWith(".meta"))
                    continue;
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                NoMetaDirectoryCopy(subdir.FullName, temppath);
            }
        }

        public static void CopyDirectorIgnore(string sourceDirName, string destDirName, string ignore = "",
            bool overwrite = false)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo target = new DirectoryInfo(destDirName);

            if (target.FullName.StartsWith(dir.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("The parent directory cannot be copied to the child directory！");
            }

            if (!dir.Exists)
                return;

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                if (!string.IsNullOrEmpty(ignore) && Regex.IsMatch(file.Name, ignore))
                {
                    continue;
                }

                string tempPath = Path.Combine(destDirName, file.Name);
                if (overwrite && File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }

                file.CopyTo(tempPath, overwrite);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destDirName, subdir.Name);
                CopyDirectorIgnore(subdir.FullName, tempPath, ignore);
            }
        }
        
    }
}