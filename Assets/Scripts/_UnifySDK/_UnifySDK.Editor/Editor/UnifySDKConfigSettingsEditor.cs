using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
             AssetDatabase.SaveAssets();
         }
        
        [MenuItem("Tools/UnifySDK/UnifySDKConfigAutoGens")]
        public static void UnifySDKConfigAutoGen()
        {
            var list= Tools.GetTypesByBaseClass<BaseUnifySDKConfig>();

            foreach (var config in list)
            {
                MethodInfo createAssetMethod = typeof(UnifySDKConfigSettingsEditor).GetMethod("CreateAsset").MakeGenericMethod(config.GetType());
                createAssetMethod.Invoke(null, null);
            }
        }
        
        public static void CreateConfigSetting()
        {
            
        }
    }
}