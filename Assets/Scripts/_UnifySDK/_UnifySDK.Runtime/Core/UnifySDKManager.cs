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

        public IUnifySDK IUnifySDK;
        
        public IUnifySDK_Account IUnifySDK_Account;
        
        public IUnifySDK_Purchase IUnifySDK_Purchase;
        
        public IUnifySDK_Crash IUnifySDK_Crash;
        
        public IUnifySDK_Device IUnifySDK_Device;
        
        private List<IUnifySDK> AllUnifySDK;

        private bool _inited = false;
        
        public readonly Dictionary<Type,List<object>> AllInterfaceUnifySDKs=new Dictionary<Type, List<object>>();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod ()
        {
            UDebug.Sys.Log("   Start initializing all UnifySDKS ");
            Instance.OnCreate();
            UDebug.Sys.Log("   All UnifySDKS were successfully initialized  ");
        }
        void OnCreate()
        {
            if (_inited)
            {
                UDebug.Sys.Log("   UnifySDKManager  has been initialized ");
                return;
            }
            _inited = true;
            AllUnifySDK = Factory.Create();
            foreach (var sdk in AllUnifySDK)
            { 
                UDebug.Sys.Log($"{sdk.GetType().Name  }  starts to initialize ");
                sdk.OnInit();
                UDebug.Sys.Log($"{sdk.GetType().Name  }  is successfully initialized   ");
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
                    InitInterFace(AllUnifySDK[i]);
                }
            }
        }

        private void InitInterFace(IUnifySDK unifySDK)
        {
            IUnifySDK = IUnifySDK ?? unifySDK;
            IUnifySDK_Account = IUnifySDK_Account ?? unifySDK as IUnifySDK_Account ;
            IUnifySDK_Purchase = IUnifySDK_Purchase ?? unifySDK as IUnifySDK_Purchase;
            IUnifySDK_Crash = IUnifySDK_Crash ?? unifySDK as IUnifySDK_Crash;
            IUnifySDK_Device = IUnifySDK_Device ?? unifySDK as IUnifySDK_Device;
        }

        /// <summary>
        /// 获取含有 该[UnifySDKInterface]特性的接口的所有实例类 
        /// </summary>
        /// <param name="interFaceType"></param>
        public List<object> GetUnifySDKList(Type interFaceType)
        {
            if (interFaceType == null)
                UDebug.Sys.LogError("GetUnifySDKList 参数 type==null ");
            else if (!interFaceType.IsDefined(typeof(UnifySDKInterfaceAttribute), false))
                UDebug.Sys.LogError("interFaceType 不包含 UnifySDKInterface 特性");
            else if (!AllInterfaceUnifySDKs.ContainsKey(interFaceType))
                UDebug.Sys.LogError($"没有该{interFaceType.Name}接口的SDK");
            else
                return AllInterfaceUnifySDKs[interFaceType];
            return null;
        }
        
        /// <summary>
        /// 获取含有 该[UnifySDKInterface]特性的接口且继承Base 的所有实例类 
        /// </summary>
        /// <param name="interFaceType">UnifySDKInterfaceAttribute</param>
        /// <param name="unifySDKName">SDKName</param>
        public object GetUnifySDK(Type interFaceType,string unifySDKName)
        {
            var list= GetUnifySDKList(interFaceType);
            foreach (var sdk in list)
            {
                if ( (sdk as IUnifySDK).SDKName == unifySDKName)
                {
                    return sdk;
                }
            }
            UDebug.Sys.LogError($"该{unifySDKName}类型的sdk 是空的");
            return null;
        }
    }
}

