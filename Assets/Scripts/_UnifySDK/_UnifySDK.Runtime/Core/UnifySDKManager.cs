using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnifySDK
{
    public partial class UnifySDKManager 
    {
        
        private static UnifySDKManager instance;

        public static UnifySDKManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnifySDKManager();
                }
                return instance;
            }
        }
        
        private  List<IUnifySDK> AllUnifySDK;

        private  bool _inited = false;
        
        public  readonly Dictionary<Type,List<object>> AllInterfaceUnifySDKs=new Dictionary<Type, List<object>>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod ()
        {
            Debug.Log("   Start initializing all UnifySDKS ");
            Instance.OnCreate();
            Debug.Log("   All UnifySDKS were successfully initialized  ");
        }
        void OnCreate()
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

            for (int i = 0; i < AllUnifySDK.Count; i++)
            {
                var initfaces = AllUnifySDK[i].GetType().GetInterfaces()
                    .Where(interfaceType => interfaceType.IsDefined(typeof(UnifySDKInterfaceAttribute), false));
                foreach (var initfaceType in initfaces)
                {
                    if (AllInterfaceUnifySDKs.ContainsKey(initfaceType))
                        AllInterfaceUnifySDKs[initfaceType].Add(AllUnifySDK[i]);
                    else
                        AllInterfaceUnifySDKs.Add(initfaceType,new List<object>(){AllUnifySDK[i]});
                }
            }
        }

        /// <summary>
        /// 获取含有 该[UnifySDKInterface]特性的接口的所有实例类 
        /// </summary>
        /// <param name="interFaceType"></param>
        public List<object> GetUnifySDKList(Type interFaceType)
        {
            if (interFaceType == null)
                Debug.LogError("GetUnifySDKList 参数 type==null ");
            else if (!interFaceType.IsDefined(typeof(UnifySDKInterfaceAttribute), false))
                Debug.LogError("该类型没有包含该特性");
            else if (!AllInterfaceUnifySDKs.ContainsKey(interFaceType))
                Debug.LogError($"没有该{interFaceType.Name}接口的SDK");
            else
                return AllInterfaceUnifySDKs[interFaceType];
            return null;
        }
        
        /// <summary>
        /// 获取含有 该[UnifySDKInterface]特性的接口且继承Base 的所有实例类 
        /// </summary>
        /// <param name="interFaceType">UnifySDKInterfaceAttribute</param>
        /// <param name="unifySDKType">UnifySDKInterfaceAttribute</param>
        public object GetUnifySDK(Type interFaceType,Type unifySDKType)
        {
            var list= GetUnifySDKList(interFaceType);
            foreach (var sdk in list)
            {
                if (sdk.GetType()==unifySDKType)
                {
                    return sdk;
                }
            }
            Debug.LogError($"该{unifySDKType.Name}类型的sdk 是空的");
            return null;
        }
    }
}

