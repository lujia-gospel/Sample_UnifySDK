using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnifySDK
{
    public partial class BuildArgumentsSettings : ScriptableObject, ISerializationCallbackReceiver
    {       
        internal static BuildArgumentsSettings s_RuntimeSettingsInstance = null;

        private static string m_SavePath = string.Empty;

        public static string SavePath {
            get
            {
                if (string.IsNullOrEmpty(m_SavePath))
                    Tools.GetScriptPath(typeof(BuildArgumentsSettings).Name,ref m_SavePath);
                return m_SavePath;
            }
        } 

        [CustomHeader("拥有的SDK")]
        [SerializeField]
        public List<string> Keys = new List<string>();

        [CustomHeader("对应的配置表")]
        [SerializeField]
        public  List<string> Values = new List<string>();

        Dictionary<string, string> argumentsSDKSettings = new Dictionary<string, string>();

        /// <summary>The current settings instance.</summary>
        public static BuildArgumentsSettings Instance
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
        static BuildArgumentsSettings GetBuildArgumentsSettings()
        {
            BuildArgumentsSettings buildArgumentsSettings = null;
#if UNITY_EDITOR
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(BuildArgumentsSettings).Name);
            if (guids.Length == 0)
            {
                //将对象实例化  
                BuildArgumentsSettings so = ScriptableObject.CreateInstance<BuildArgumentsSettings>();  
        
                if (so == null)  
                {  
                    Debug.LogError("该对象无效，无法将对象实例化");  
                    return buildArgumentsSettings;  
                }

                buildArgumentsSettings = so;
                //按指定路径生成配置文件  
                AssetDatabase.CreateAsset(so,SavePath);
                AssetDatabase.SaveAssets();
            }
            else
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                buildArgumentsSettings = AssetDatabase.LoadAssetAtPath<BuildArgumentsSettings>(path);
            }
#else
            // 将配置文件转化为对象
            buildArgumentsSettings = Resources.Load<BuildArgumentsSettings>("BuildArgumentsSettings");
#endif
            return buildArgumentsSettings;
        }
        
        public void SetSDKValue(string targetPlatform, string settings)
        {
            argumentsSDKSettings[targetPlatform] = settings;
        }
        
        public string GetSDKValue(string targetPlatform)
        {
            string ret = null;
            argumentsSDKSettings.TryGetValue(targetPlatform, out ret);
            return ret;
        }
        
        /// <summary>Serialization override.</summary>
        public void OnBeforeSerialize()
        {
            Keys.Clear();
            Values.Clear();

            foreach (var kv in argumentsSDKSettings)
            {
                Keys.Add(kv.Key);
                Values.Add(kv.Value);
            }
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
    }
}