using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnifySDK.Editor
{
    public class UnifySDKAssetsSetting : ScriptableObject
    {
        public static string m_SavePath = string.Empty;
        public static string SavePath
        {
            get
            {
                if (string.IsNullOrEmpty(m_SavePath))
                    Tools.GetScriptPath(typeof(UnifySDKAssetsSetting).Name,ref m_SavePath);
                return m_SavePath;
            }
        }
        
        public static string RelativePath(ScriptableObject script){
            string path = "";
#if UNITY_EDITOR
            MonoScript monoScript = MonoScript.FromScriptableObject(script);
            path = AssetDatabase.GetAssetPath(monoScript);
#endif
            return path;
        }

    

        [Serializable]
        public class UnifySDKAssetInfo
        {
            public string assetFolder;
            public UnifySDKAssetInfo()
            {
            }

            public UnifySDKAssetInfo(string path)
            {
                assetFolder = path;
            }
        }
    
        [SerializeField]
        private List<UnifySDKAssetInfo> m_UnifySDKAssetInfos=new List<UnifySDKAssetInfo>();
    
        public List<UnifySDKAssetInfo> UnifySDKResPathInfos
        {
            get {
                return m_UnifySDKAssetInfos;
            }
        }
        
        public static UnifySDKAssetsSetting[] GetUnifySDKAssetsSettings()
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(UnifySDKAssetsSetting).Name);
            var _unifySDKAssetsSettings = new UnifySDKAssetsSetting[guids.Length];
            for (int i = 0; i < _unifySDKAssetsSettings.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                _unifySDKAssetsSettings[i] = AssetDatabase.LoadAssetAtPath<UnifySDKAssetsSetting>(path);
            }
            return _unifySDKAssetsSettings;
        }
        
        public static UnifySDKAssetsSetting GetUnifySDKAssetsSetting( string sdk)
        {
            string path = $"{SavePath}/{sdk}.asset";
            return  AssetDatabase.LoadAssetAtPath<UnifySDKAssetsSetting>(path);
        }
    }
}
