using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnifySDK
{
    public static class UnifySDKManager 
    {
        private static List<IUnifySDK> AllUnifySDK;

        private static bool _inited = false;

        
        public static void OnCreate()
        {
            if (_inited)
            {
                Debug.Log("   UnifySDKManager  has been initialized ");
                return;   
            }
            _inited = true;
            AllUnifySDK = Factory.Create();
            foreach (var sdk in AllUnifySDK)
            { 
                Debug.Log($"{sdk.GetType().Name  }  starts to initialize ");
                sdk.OnInit();
                Debug.Log($"{sdk.GetType().Name  }  is successfully initialized   ");
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod ()
        {
            Debug.Log("   Start initializing all UnifySDKS ");
            OnCreate();
            Debug.Log("   All UnifySDKS were successfully initialized  ");
        }
    }
}

