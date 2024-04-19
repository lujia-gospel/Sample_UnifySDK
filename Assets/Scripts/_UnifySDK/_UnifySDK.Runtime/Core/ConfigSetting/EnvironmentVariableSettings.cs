using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnifySDK
{
    public partial class EnvironmentVariableSettings : ScriptableObject, ISerializationCallbackReceiver
    {       
        internal static EnvironmentVariableSettings s_RuntimeSettingsInstance = null;

        private static string m_SavePath = string.Empty;
#if UNITY_EDITOR
        [BlackList]
        public static string SavePath {
            get
            {
                if (string.IsNullOrEmpty(m_SavePath))
                    Tools.GetScriptPath(typeof(EnvironmentVariableSettings).Name,ref m_SavePath);
                return m_SavePath;
            }
        } 
#endif

      

        [HideInInspector] [SerializeField]
        public List<string> Keys = new List<string>();

        [HideInInspector] [SerializeField]
        public  List<string> Values = new List<string>();

        Dictionary<string, string> argumentsSDKSettings = new Dictionary<string, string>();

        /// <summary>The current settings instance.</summary>
        public static EnvironmentVariableSettings Instance
        {
            get
            {
                if (s_RuntimeSettingsInstance == null)
                    s_RuntimeSettingsInstance = GetBuildArgumentsSettings();
                return s_RuntimeSettingsInstance;
            }
        }
   
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void AttemptInitializeSDKOnLoad()
        {
            if (s_RuntimeSettingsInstance == null)
                s_RuntimeSettingsInstance = GetBuildArgumentsSettings();
        }
        static EnvironmentVariableSettings GetBuildArgumentsSettings()
        {
            EnvironmentVariableSettings environmentVariableSettings = null;
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(EnvironmentVariableSettings).Name);
            if (guids.Length == 0)
            {
                //将对象实例化  
                EnvironmentVariableSettings so = ScriptableObject.CreateInstance<EnvironmentVariableSettings>();  
        
                if (so == null)  
                {  
                    UDebug.Sys.LogError("该对象无效，无法将对象实例化");  
                    return environmentVariableSettings;  
                }

                environmentVariableSettings = so;
                //按指定路径生成配置文件  
                AssetDatabase.CreateAsset(so,SavePath);
                AssetDatabase.SaveAssets();
            }
            else
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                environmentVariableSettings = AssetDatabase.LoadAssetAtPath<EnvironmentVariableSettings>(path);
            }
#else
            // 将配置文件转化为对象
            environmentVariableSettings = Resources.Load<EnvironmentVariableSettings>("EnvironmentVariableSettings");
            //environmentVariableSettings.OnAfterDeserialize();
#endif
            return environmentVariableSettings;
        }

        public void DeleteSDKValue(int index)
        {
            Keys.RemoveAt(index);
            Values.RemoveAt(index);
            OnAfterDeserialize();
        }

        public void SetSDKValue(string targetPlatform, string settings)
        {
            argumentsSDKSettings[targetPlatform] = settings;
            Keys.Add(targetPlatform);
            Values.Add(settings);
        }
        
        public string GetSDKValue(string targetPlatform)
        {
            string ret = null;
            argumentsSDKSettings.TryGetValue(targetPlatform, out ret);
            #if !UNITY_EDITOR
            if (string.IsNullOrEmpty(ret))
                UDebug.Logic.LogError($"配置表里没有 {targetPlatform}");
            #endif
            return ret;
        }
        
        /// <summary>Serialization override.</summary>
        public void OnBeforeSerialize()
        {
            // Keys.Clear();
            // Values.Clear();
            //
            // foreach (var kv in argumentsSDKSettings)
            // {
            //     UDebug.Logic.LogError($"   OnBeforeSerialize   Keys:  {kv.Key}  ");
            //     UDebug.Logic.LogError($"   OnBeforeSerialize   Values:  {kv.Value}   ");
            //     Keys.Add(kv.Key);
            //     Values.Add(kv.Value);
            // }
        }

        /// <summary>Serialization override.</summary>
        public void OnAfterDeserialize()
        {
            argumentsSDKSettings = new Dictionary<string, string>();
            for (int i = 0; i < Math.Min(Keys.Count, Values.Count); i++)
            {
                argumentsSDKSettings.Add(Keys[i], Values[i]);
            }
        }

        public Dictionary<string, string> GetDic()
        {
            return argumentsSDKSettings;
        }

        public void UpdateDic(Dictionary<string, string> dic)
        {
            argumentsSDKSettings.Clear();
            Keys.Clear();
            Values.Clear();
            foreach (var kv in dic)
            {
                SetSDKValue(kv.Key, kv.Value);
            }
        }
    }
}