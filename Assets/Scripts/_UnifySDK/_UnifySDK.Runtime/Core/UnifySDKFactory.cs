using System;
using System.Collections.Generic;

namespace UnifySDK
{
    public static class Factory
    {
        public static List<IUnifySDK> Create()
        {
            var unifySDKs = new List<IUnifySDK>();
            var list = Tools.GetTypesByBaseClass<BaseUnifySDKFactory>();
            foreach (var factory in list)
            {
                try
                {
                    var unfiySdk =  factory.Create();
#if UNITY_EDITOR
                    if (!EnvironmentVariableSettings.Instance.Keys.Contains(unfiySdk.SDKType.ToString()))
                        continue;
#endif
                    unifySDKs.Add(unfiySdk);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            unifySDKs.Sort((x, y) =>  y.Priority - x.Priority);
            return unifySDKs;
        }
    }
}

