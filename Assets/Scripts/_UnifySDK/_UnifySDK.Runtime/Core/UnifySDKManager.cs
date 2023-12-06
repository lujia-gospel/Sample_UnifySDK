using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnifySDK
{
    public enum UnifySDKType
    {
        QuickSDK,
        DeviceOAID,
        ReYunSDK,
        CrasheyeSDK,
        UltraSDK,
        QuickGAT,
        Adjust,
    }

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

        public IUnifySDK_Analysis IUnifySDK_Analysis;
        
        public List<IUnifySDK> AllUnifySDK;

        public Dictionary<UnifySDKType, IUnifySDK> UnifySDKDic;

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
            UnifySDKDic = new Dictionary<UnifySDKType, IUnifySDK>();
            AllUnifySDK = Factory.Create();
            foreach (var sdk in AllUnifySDK)
            {
                if (sdk.AutoInit)
                {
                    UDebug.Sys.Log($"AutoInit {sdk.GetType().Name  }  starts to initialize ");
                    try
                    {
                        sdk.OnInit();
                        UDebug.Sys.Log($"AutoInit {sdk.GetType().Name  }  is successfully initialized   ");
                    }
                    catch (Exception e)
                    {
                        UDebug.Sys.LogError($"AutoInit {sdk.GetType().Name  }  initialization failed   {e}");
                    }
                }
                UnifySDKDic.Add(sdk.SDKType,sdk);
                var initfaces = sdk.GetType().GetInterfaces()
                    .Where(interfaceType => interfaceType.IsDefined(typeof(UnifySDKInterfaceAttribute), false));
                foreach (var initfaceType in initfaces)
                {
                    if (AllInterfaceUnifySDKs.ContainsKey(initfaceType))
                        AllInterfaceUnifySDKs[initfaceType].Add(sdk);
                    else
                        AllInterfaceUnifySDKs.Add(initfaceType,new List<object>(){sdk});
                    InitInterFace(sdk);
                }
            }
        }

        public bool CheckContainSDK(UnifySDKType type)
        {
#if UNITY_EDITOR
            if (EnvironmentVariableSettings.Instance.EditorRunUnifySDK)
#endif
            {
                if (UnifySDKDic.ContainsKey(type))
                    return true;
            }
            return false;
        }

        private void InitInterFace(IUnifySDK unifySDK)
        {
            IUnifySDK = IUnifySDK ?? unifySDK;
            IUnifySDK_Account = IUnifySDK_Account ?? unifySDK as IUnifySDK_Account;
            IUnifySDK_Purchase = IUnifySDK_Purchase ?? unifySDK as IUnifySDK_Purchase;
            IUnifySDK_Crash = IUnifySDK_Crash ?? unifySDK as IUnifySDK_Crash;
            IUnifySDK_Device = IUnifySDK_Device ?? unifySDK as IUnifySDK_Device;
            IUnifySDK_Analysis =IUnifySDK_Analysis ?? unifySDK as IUnifySDK_Analysis;
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
        /// <param name="unifySDKName">SDKType</param>
        public object GetUnifySDK(Type interFaceType,UnifySDKType unifySDKType)
        {
            var list= GetUnifySDKList(interFaceType);
            foreach (var sdk in list)
            {
                if ( (sdk as IUnifySDK).SDKType == unifySDKType)
                {
                    return sdk;
                }
            }
            UDebug.Sys.LogError($"该{unifySDKType.ToString()}类型的sdk 是空的");
            return null;
        }
    }
}

