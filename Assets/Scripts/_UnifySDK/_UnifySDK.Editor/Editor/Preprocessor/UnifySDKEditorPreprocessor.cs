//
// using System.IO;
// using System.Xml;
// using UnityEditor.Build;
// using UnityEditor;
// using UnityEngine;
//
//
// namespace UnifySDK.Editor
// {
//     public class UnifySDKEditorPreprocessor : IPreprocessBuildWithReport
//     {
//         
//         public int callbackOrder
//         {
//             get
//             {
//                 return 0;
//             }
//         }
// #if UNITY_2018_1_OR_NEWER
//         public void OnPreprocessBuild(UnityEditor.Build.Reporting.BuildReport report)
//         {
//             OnPreprocessBuild(report.summary.platform, string.Empty);
//         }
// #endif
//
//         public void OnPreprocessBuild(BuildTarget target, string path)
//         {
//             if (target == BuildTarget.Android)
//             {
// #if UNITY_ANDROID
//                 //RunPostProcessTasksAndroid("Adjust/Android/AdjustAndroidManifest.xml");
// #endif
//             }
//         }
// #if UNITY_ANDROID
//         private static void RunPostProcessTasksAndroid(string targetManifestPath)
//         {
//             
//             var androidPluginsPath = Path.Combine(Application.dataPath, "Plugins/Android");
//             var appManifestPath = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
//             // Let's open the app's AndroidManifest.xml file.
//             var manifestFile = new XmlDocument();
//             manifestFile.Load(appManifestPath);
//             var mergeManifestFile = new XmlDocument();
//             //manifestFile.Load(targetManifestPath);
//             var manifestHasChanged = false;
//             
//             manifestHasChanged |= AddPermissions(manifestFile,mergeManifestFile);
//
//             //manifestHasChanged |= AddBroadcastReceiver(manifestFile);
//             // Add intent filter to main activity if it is missing.
//             //manifestHasChanged |= AddBroadcastReceiver(manifestFile);
//         }
//
//         private static bool AddPermissions(XmlDocument manifest,XmlDocument mergeManifest)
//         {
//             var manifestHasChanged = false;
//
//             var permissions= GetAllPermissions(manifest);//GetAllPermissions(mergeManifest);
//             for (int i = 0; i < permissions.Length; i++)
//             {
//                 manifestHasChanged |= AddPermission(manifest, permissions[i]);
//             }
//             return manifestHasChanged;
//         }
//         
//         private static bool AddPermission(XmlDocument manifest, string permissionValue)
//         {
//             if (DoesPermissionExist(manifest, permissionValue))
//             {
//                 UDebug.Log(string.Format("[Adjust]: Your app's AndroidManifest.xml file already contains {0} permission.", permissionValue));
//                 return false;
//             }
//
//             var element = manifest.CreateElement("uses-permission");
//             AddAndroidNamespaceAttribute(manifest, "name", permissionValue, element);
//             manifest.DocumentElement.AppendChild(element);
//             UDebug.Log(string.Format("[Adjust]: {0} permission successfully added to your app's AndroidManifest.xml file.", permissionValue));
//
//             return true;
//         }
//
//         private static string[] GetAllPermissions(XmlDocument manifest)
//         {
//             var xpath = "/manifest/uses-permission";
//             var XmlNodeList= manifest.SelectNodes(xpath, GetNamespaceManager(manifest));
//             string[] permissions = new string[XmlNodeList.Count];
//             for (int i = 0; i < XmlNodeList.Count; i++)
//             {
//                 permissions[i] = XmlNodeList[i].Attributes[0].Value;
//             }
//             return permissions;
//         }
//         
//         private static bool DoesPermissionExist(XmlDocument manifest, string permissionValue)
//         {
//             var xpath = string.Format("/manifest/uses-permission[@android:name='{0}']", permissionValue);
//             return manifest.DocumentElement.SelectSingleNode(xpath, GetNamespaceManager(manifest)) != null;
//         }
//         
//         private static void AddAndroidNamespaceAttribute(XmlDocument manifest, string key, string value, XmlElement node)
//         {
//             var androidSchemeAttribute = manifest.CreateAttribute("android", key, "http://schemas.android.com/apk/res/android");
//             androidSchemeAttribute.InnerText = value;
//             node.SetAttributeNode(androidSchemeAttribute);
//         }
//         
//         private static XmlNamespaceManager GetNamespaceManager(XmlDocument manifest)
//         {
//             var namespaceManager = new XmlNamespaceManager(manifest.NameTable);
//             namespaceManager.AddNamespace("android", "http://schemas.android.com/apk/res/android");
//             return namespaceManager;
//         }
// #endif
//     }
// }
//
//
