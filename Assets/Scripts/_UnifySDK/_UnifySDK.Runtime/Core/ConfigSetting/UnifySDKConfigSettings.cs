using System;
using System.Collections.Generic;

using UnityEngine;

namespace UnifySDK
{
    public class UnifySDKConfigSettings : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<string> Keys = new List<string>();

        [SerializeField]
        List<BaseUnifySDKConfig> Values = new List<BaseUnifySDKConfig>();
        Dictionary<string, BaseUnifySDKConfig> Settings = new Dictionary<string, BaseUnifySDKConfig>();
        public Dictionary<string, BaseUnifySDKConfig> DicSettings
        {
            get => Settings;
        }

        public void SetSettingsForTargetPlatform(string targetPlatform, BaseUnifySDKConfig settings)
        {
            Settings[targetPlatform] = settings;
        }
        
        public BaseUnifySDKConfig GetSettingsForTargetPlatform(string targetPlatform)
        {
            BaseUnifySDKConfig ret = null;
            Settings.TryGetValue(targetPlatform, out ret);
            return ret;
        }
        
        /// <summary>Serialization override.</summary>
        public void OnBeforeSerialize()
        {
            Keys.Clear();
            Values.Clear();

            foreach (var kv in Settings)
            {
                Keys.Add(kv.Key);
                Values.Add(kv.Value);
            }
        }

        /// <summary>Serialization override.</summary>
        public void OnAfterDeserialize()
        {
            Settings = new Dictionary<string, BaseUnifySDKConfig>();
            for (int i = 0; i < Math.Min(Keys.Count, Values.Count); i++)
            {
                Settings.Add(Keys[i], Values[i]);
            }
        }

        public void Destroy(string configName)
        {
            Keys.Remove(configName);
            Values.Remove(Settings[configName]);
            DestroyImmediate(Settings[configName],true);
            Settings.Remove(configName);
        }

        public void Clear()
        {
            Keys.Clear();
            Values.Clear();
            Settings.Clear();
        }
    }
}