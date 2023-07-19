using System;
using System.Collections.Generic;
using System.Reflection;

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
                    unifySDKs.Add(factory.Create()); 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
            unifySDKs.Sort((x, y) => x.Priority - y.Priority );
            return unifySDKs;
        }
    }
}

