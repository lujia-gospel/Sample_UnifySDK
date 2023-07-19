using UnityEngine;
using System;

namespace UnifySDK
{
    public class UnifySDKConfig<T> where T : ScriptableObject
    {
        public static string GetConfigName()
        {
            return $"{typeof(T).Name} Settings" ;
        }

        public static UnifySDKConfigSettings GetConfig(string configName = "")
        {
            var path = string.IsNullOrEmpty(configName) ? GetConfigName():configName;  
   
            // 将配置文件转化为对象  
            var config = Resources.Load<UnifySDKConfigSettings>(path);
            
            return config;
        }
    }

    [Serializable]
    public class BaseUnifySDKConfig : ScriptableObject
    {
        public string Appkey;
        public string ChannelId;

        public virtual string[] GetTargetPlatforms()
        {
            return new string[] { "Unique"};
        }
    }
}
