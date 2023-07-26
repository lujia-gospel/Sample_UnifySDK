namespace UnifySDK
{
    public abstract class BaseUnifySDKFactory : IUnifySDKFactory
    {
        protected abstract string SDKName { get; }
        public virtual IUnifySDK Create()
        {
            return null;
        }
        
        protected T GetSDKConfig<T>() where T:BaseUnifySDKConfig
        {
            var configName = EnvironmentVariableSettings.Instance.GetSDKValue(SDKName);
            

            var configSettings  = UnifySDKConfig<T>.GetConfig();
            
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(configName))
            {
                foreach (var targetPlatformConfig in configSettings.DicSettings.Values)
                {
                    return targetPlatformConfig as T;
                }
            }
#endif
            return configSettings.GetSettingsForTargetPlatform(configName) as T;
        }
    }
}