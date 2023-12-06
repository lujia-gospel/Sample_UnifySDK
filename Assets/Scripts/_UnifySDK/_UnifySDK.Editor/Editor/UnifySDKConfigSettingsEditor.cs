using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace UnifySDK.Editor
{
    public class UnifySDKConfigSettingsEditor
    {
         // 将类实例化为配置文件
         public static void CreateAsset<T>() where T: BaseUnifySDKConfig
         {
             var type= typeof(T);
       
             BaseUnifySDKConfig unifySDKConfig = Activator.CreateInstance(type) as BaseUnifySDKConfig;
             var targetPlatforms = unifySDKConfig.GetTargetPlatforms();
             var scriptObj = MonoScript.FromScriptableObject(unifySDKConfig);
                  
             var path = AssetDatabase.GetAssetPath(scriptObj).Replace($"{type.Name}.cs","Resources");
             if (Directory.Exists(path)==false)  
             {  
                 Directory.CreateDirectory(path);  
             }
             var assetPath= $"{path}/{type.Name} Settings.asset";
             UnifySDKConfigSettings unifySDKConfigSettings = AssetDatabase.LoadAssetAtPath<UnifySDKConfigSettings>(assetPath);
         
             if (unifySDKConfigSettings == null)
             {
                 unifySDKConfigSettings = ScriptableObject.CreateInstance<UnifySDKConfigSettings>();
                 AssetDatabase.CreateAsset(unifySDKConfigSettings, assetPath);
             }

             BaseUnifySDKConfig[] unifySDKConfigs = new BaseUnifySDKConfig[targetPlatforms.Length];
             for (int i = 0; i < targetPlatforms.Length; i++)
             {
                 unifySDKConfigs[i] = unifySDKConfigSettings.GetSettingsForTargetPlatform(targetPlatforms[i]);
                 
                 if ( unifySDKConfigs[i] == null)  
                 {  
                     unifySDKConfigs[i] = ScriptableObject.CreateInstance(type) as BaseUnifySDKConfig;
                     unifySDKConfigs[i].name = $"{targetPlatforms[i]} Setting";
                     unifySDKConfigSettings.SetSettingsForTargetPlatform(targetPlatforms[i],unifySDKConfigs[i]);
                     AssetDatabase.AddObjectToAsset(unifySDKConfigs[i], assetPath);
                 }
             }

             List<string> needDeletes = new List<string>();
             foreach (var key in unifySDKConfigSettings.DicSettings.Keys)
             {
                 var name = key.Replace(" Setting", string.Empty);
                 if (!targetPlatforms.Contains(name))
                 {
                     needDeletes.Add(key);
                 }
             }

             foreach (var name in needDeletes)
             {
                 unifySDKConfigSettings.Destroy(name);
             }

             JenkinsProperties(unifySDKConfigSettings);
             AssetDatabase.SaveAssets();
         }
         public static T GetUnifySDKConfig<T>(string targetPlatform) where T: BaseUnifySDKConfig
         {
             var type= typeof(T);
             BaseUnifySDKConfig unifySDKConfig = Activator.CreateInstance(type) as BaseUnifySDKConfig;
             var targetPlatforms = unifySDKConfig.GetTargetPlatforms();
             var scriptObj = MonoScript.FromScriptableObject(unifySDKConfig);
             var path = AssetDatabase.GetAssetPath(scriptObj).Replace($"{type.Name}.cs","Resources");
             var assetPath= $"{path}/{type.Name} Settings.asset";
             UnifySDKConfigSettings unifySDKConfigSettings = AssetDatabase.LoadAssetAtPath<UnifySDKConfigSettings>(assetPath);
             if (unifySDKConfigSettings==null)
                 return null;
             return unifySDKConfigSettings.GetSettingsForTargetPlatform(targetPlatform) as T;
         }
         public static void JenkinsPropertiesDeleteSDK(string sdkName)
         {
             var jenkinsProperties= Tools.ReadFileString($"{EnvironmentVariableSettings.SavePath}/jenkins.properties");
             UDebug.Sys.Log(jenkinsProperties);
             var headerFile = "SDK_Name\tSDK_Platform_Config\r\n";
             var content= jenkinsProperties.Replace(headerFile, string.Empty);
             Dictionary<string, List<string>> dic = new Dictionary<string,  List<string>>();
             var kvContent = content.Split("\r\n");
             for (int i = 0; i < kvContent.Length; i++)
             {
                 if (!string.IsNullOrEmpty(kvContent[i]))
                 {
                     var k_v=  kvContent[i].Split("\t");
                     if (dic.ContainsKey(k_v[0]))
                     {
                         dic[k_v[0]].Add( k_v[1].Split(":")[1]);
                     }
                     else
                         dic[k_v[0]]=new List<string>(){ k_v[1].Split(":")[1]};  
                 }
             }
             dic.Remove(sdkName);
         
             StringBuilder sb = new();
             sb.Append(headerFile);
             foreach (var kv in dic)
             {
                 foreach (var p in kv.Value)
                 {
                     sb.Append($"{kv.Key}\t{kv.Key}:{p}\r\n");
                 }
             }
             UDebug.Sys.Log(sb.ToString());
             Tools.WriteFileString($"{EnvironmentVariableSettings.SavePath}/jenkins.properties",sb.ToString());
             sb.Clear();
         }

         public static void JenkinsProperties(UnifySDKConfigSettings unifySDKConfigSettings)
         {
             var sdkName = unifySDKConfigSettings.name.Replace("_UnifySDKConfig Settings", string.Empty);
             var jenkinsProperties= Tools.ReadFileString($"{EnvironmentVariableSettings.SavePath}/jenkins.properties");
        
             var headerFile = "SDK_Name\tSDK_Platform_Config\r\n";
             var content= jenkinsProperties.Replace(headerFile, string.Empty);
             Dictionary<string, List<string>> dic = new Dictionary<string,  List<string>>();
             var kvContent = content.Split("\r\n");
             for (int i = 0; i < kvContent.Length; i++)
             {
                 if (!string.IsNullOrEmpty(kvContent[i]))
                 {
                     var k_v=  kvContent[i].Split("\t");
                     if (dic.ContainsKey(k_v[0]))
                     {
                         dic[k_v[0]].Add( k_v[1].Split(":")[1]);
                     }
                     else
                         dic[k_v[0]]=new List<string>(){ k_v[1].Split(":")[1]};  
                 }
             }

             bool isChange = false;
             
             if (dic.ContainsKey(sdkName))
             {
                 var keys = unifySDKConfigSettings.DicSettings.Keys;
                 foreach (var key in keys)
                 {
                     if (!dic[sdkName].Contains(key))
                     {
                         isChange=true;
                         break;
                     }
                 }
                 foreach (var key in dic[sdkName])
                 {
                     if (!keys.Contains(key))
                     {
                         isChange=true;
                         break;
                     }
                 }
             }
             else
                 isChange=true;
             dic[sdkName] = unifySDKConfigSettings.DicSettings.Keys.ToList();
            

             if (isChange)
             {
                 StringBuilder sb = new();
                 sb.Append(headerFile);
                 foreach (var kv in dic)
                 {
                     foreach (var p in kv.Value)
                     {
                         sb.Append($"{kv.Key}\t{kv.Key}:{p}\r\n");
                     }
                 }
                 Tools.WriteFileString($"{EnvironmentVariableSettings.SavePath}/jenkins.properties",sb.ToString());
                 sb.Clear();
             }
         }

         [MenuItem("Tools/UnifySDK/扩展SDK生成配置")]
        public static void UnifySDKConfigAutoGen()
        {
            var list= Tools.GetTypesByBaseClass<BaseUnifySDKConfig>();

            foreach (var config in list)
            {
                MethodInfo createAssetMethod = typeof(UnifySDKConfigSettingsEditor).GetMethod("CreateAsset").MakeGenericMethod(config.GetType());
                createAssetMethod.Invoke(null, null);
            }
        }
    }
}