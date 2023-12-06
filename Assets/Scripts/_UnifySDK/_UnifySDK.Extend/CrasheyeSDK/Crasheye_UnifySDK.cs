using System;
using com.xsj.Crasheye.U3D;
using UnifySDK.Event;
using UnifySDK.Event.Crash;
using UnityEngine;
#if !UNITY_STANDALONE_WIN
using com.xsj.Crasheye.U3D;
#endif
#if UNITY_ANDROID
using com.xsj.Crasheye.U3D.Android;
#endif
#if UNITY_IPHONE || UNITY_IOS
using com.xsj.Crasheye.U3D.IOS;

#endif

namespace UnifySDK
{
    #region BaseUnifySDKFactory

    public class Crasheye_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override UnifySDKType SDKType
        {
            get => UnifySDKType.CrasheyeSDK;
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<Crasheye_UnifySDKConfig>();
            return new Crasheye_UnifySDK(sdkConfig, SDKType);
        }
    }

    #endregion

    #region BaseUnifySDK

    public partial class Crasheye_UnifySDK : BaseUnifySDK<Crasheye_UnifySDKConfig>
    {
        public string YourAppKeyForAndroid = "YourAppKeyForAndroid";
        public string YourChannelIdForAndroid = "YourChannelIdForAndroid";

        public string YourAppKeyForIOS = "YourAppKeyForIOS";
        public string YourChannelIdForIOS = "YourChannelIdForIOS";

        public string YourAppKeyForPC = "YourAppKeyForPC";
        public string YourChannelIdForPC = "YourChannelIdForPC";

#if !UNITY_STANDALONE_WIN
        public static CrasheyeLib crasheyeLib = null;
#endif

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS || UNITY_STANDALONE_WIN
        private static string YourChannelId = "NA";
#endif

        public Crasheye_UnifySDK(Crasheye_UnifySDKConfig config, UnifySDKType type) : base(config, type)
        {
#if UNITY_ANDROID
            YourAppKeyForAndroid = Config.Appkey;
            YourChannelIdForAndroid = Config.ChannelId;
#elif UNITY_IPHONE || UNITY_IOS
            YourAppKeyForIOS = Config.Appkey;
            YourAppKeyForPC = Config.ChannelId;
#elif UNITY_STANDALONE_WIN
            YourAppKeyForPC = Config.Appkey;
            YourChannelIdForPC = Config.ChannelId;
#endif
            SetChannelID(Config.ChannelId);
        }


        public override void OnInit()
        {
            base.OnInit();

            if (UnifySDKManager.Instance.CheckContainSDK(UnifySDKType.QuickSDK))
            {
                UnifySDKManager.Instance.UnifySDKDic[UnifySDKType.QuickSDK].OnInitSuccess.Handler +=
                    (_data, eventArgs) =>
                    {
                        //GameObject.DontDestroyOnLoad(new GameObject("CrasheyeSample",new []{typeof(CrasheyeSample) }));
                        if (Application.platform == RuntimePlatform.Android)
                        {
#if UNITY_ANDROID
                SetChannelID(YourChannelIdForAndroid);
                StartInitCrasheye(YourAppKeyForAndroid);
#endif
                        }
                        else if (Application.platform == RuntimePlatform.IPhonePlayer)
                        {
#if UNITY_IPHONE || UNITY_IOS
                            SetChannelID(YourChannelIdForIOS);
                            StartInitCrasheye(YourAppKeyForIOS);
#endif
                        }
                        else if (Application.platform == RuntimePlatform.WindowsPlayer)
                        {
#if UNITY_STANDALONE_WIN
                SetChannelID(YourAppKeyForPC);
                StartInitCrasheye(YourAppKeyForPC);
#endif
                        }
                    };
            }
        }

        /// <summary>
        /// 启动Crasheye
        /// </summary>
        /// <param name="Your_AppKey"></param>
        public void StartInitCrasheye(string Your_AppKey)
        {
            RegisterLogCallback();
            if (Config.EnableCSharpStackTrace)
                EnableCSharpStackTrace();
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.Init(Your_AppKey, YourChannelId);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.Init(Your_AppKey, YourChannelId);
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN
                DumpForPC.Init(Your_AppKey, YourChannelId);
#endif
            }
        }

        /// <summary>
        /// 设置渠道号
        /// </summary>
        /// <param name="yourChannelID"></param>
        public override void SetChannelID(string yourChannelID)
        {
            if (String.IsNullOrEmpty(yourChannelID))
            {
                Debug.LogError("set channel id value is null or empty!");
                return;
            }

            if (yourChannelID.Equals("YourChannelIdForAndroid") || yourChannelID.Equals("YourChannelIdForIOS"))
            {
                return;
            }

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS || UNITY_STANDALONE_WIN
            YourChannelId = yourChannelID;
#endif
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="setUserIdentifier">用户标识</param>
        public override void SetUserIdentifier(string userIdentifier)
        {
            if (string.IsNullOrEmpty(userIdentifier))
            {
                return;
            }

            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetUserIdentifier(userIdentifier);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetUserIdentifier(userIdentifier);
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN
                DumpForPC.SetUserIdentifier_UTF8(userIdentifier);
#endif
            }
        }


        /// <summary>
        /// 注册脚本捕获
        /// </summary>
        public void RegisterLogCallback()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                crasheyeLib = new LibForAndroid();
                CrasheyeForAndroid.InitCrasheyeLib = crasheyeLib.InitCrasheyeLib;
                CrasheyeForAndroid.SetExceptionCallback = crasheyeLib.SetExceptionCallback;
                CrasheyeForAndroid.setInternalExtraData();
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                crasheyeLib = new LibForiOS();
                CrasheyeForIOS.InitCrasheyeLib = crasheyeLib.InitCrasheyeLib;
                CrasheyeForIOS.SetExceptionCallback = crasheyeLib.SetExceptionCallback;
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN
                Debug.Log("RegisterLogCallback");
                AppDomain.CurrentDomain.UnhandledException += DumpForPC.OnHandleUnresolvedException;
                SetRegisterLogFunction(DumpForPC.OnHandleLogCallback);
                Application.logMessageReceived += RegisterLogFunction;
#endif
            }
#if !UNITY_STANDALONE_WIN
            if (crasheyeLib == null)
            {
                return;
            }

            System.AppDomain.CurrentDomain.UnhandledException +=
                new System.UnhandledExceptionEventHandler(crasheyeLib.OnHandleUnresolvedException);

            SetRegisterLogFunction(crasheyeLib.OnHandleLogCallback);

#if UNITY_5
             Application.logMessageReceived += RegisterLogFunction;
#else
            Application.RegisterLogCallback(RegisterLogFunction);
#endif
#endif
        }

        /// <summary>
        /// 设置版本号信息
        /// </summary>
        /// <param name="yourAppVersion">App版本号</param>
        public override void SetAppVersion(string yourAppVersion)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetAppVersion(yourAppVersion);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetAppVersion(yourAppVersion);
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN
                DumpForPC.SetAppVersion(yourAppVersion);
#endif
            }
        }

        private void RegisterLogFunction(string logString, string stackTrace, LogType type)
        {
#if (UNITY_IPHONE || UNITY_IOS) && !UNITY_EDITOR
    		if (Application.platform == RuntimePlatform.IPhonePlayer)
    		{
    			CrasheyeForIOS.SaveLogger(logString);
    		}
    
    		if (CrasheyeForIOS.GetLoggingLines() > 0 && 
                Application.platform == RuntimePlatform.IPhonePlayer && 
                (type == LogType.Assert || type == LogType.Exception)
               )
    		{
    			CrasheyeForIOS.addLog(CrasheyeForIOS.GetLogger());
    		}
#endif
            if (m_RegisterLog != null)
            {
                m_RegisterLog(logString, stackTrace, type);
            }

#if (UNITY_IPHONE || UNITY_IOS) && !UNITY_EDITOR
    		if (CrasheyeForIOS.GetLoggingLines() > 0 && 
                Application.platform == RuntimePlatform.IPhonePlayer && 
                (type == LogType.Assert || type == LogType.Exception)
                )
    		{
    
    			CrasheyeForIOS.removeLog();
    		}
#endif
        }

        public delegate void RegisterLog(string logString, string stackTrace, LogType type);

        private RegisterLog m_RegisterLog = null;

        /// <summary>
        /// 设置用户的脚本回调的函数
        /// </summary>
        /// <param name="registerLogCBFun"></param>
        public void SetRegisterLogFunction(RegisterLog registerLogCBFun)
        {
            Debug.Log("SetRegisterLogFunction");
            m_RegisterLog += registerLogCBFun;
        }
    }

    #endregion

    public partial class Crasheye_UnifySDK : IUnifySDK_Crash
    {
        private IUnifySDK_Crash _unifySDKImplementation;

        /// <summary>
        /// 发送脚本异常
        /// </summary>
        /// <param name="ex">Excepiton Info</param>
        public void SendScriptException(Exception ex)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                crasheyeLib.LibSendScriptException(ex.Message, ex.StackTrace);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                crasheyeLib.LibSendScriptException(ex.Message, ex.StackTrace);
#endif
            }
        }

        /// <summary>
        /// 上报脚本异常
        /// </summary>
        /// <param name="errorTitle">错误的标题</param>
        /// <param name="stacktrace">错误堆栈信息</param>
        /// <param name="language">语言</param>
        public void SendScriptException(ScriptExceptionConfigData data)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                crasheyeLib.LibSendScriptException(data.errorTitle, data.stacktrace, data.language);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                crasheyeLib.LibSendScriptException(data.errorTitle, data.stacktrace, data.language);
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN
                DumpForPC.SendScriptException(errorTitle, stacktrace, language);
#endif
            }
        }

        /// <summary>
        /// 设置是否只在wifi下上报报告文件
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFlushOnlyOverWiFi(bool enabled)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetFlushOnlyOverWiFi(enabled);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetFlushOnlyOverWiFi(enabled);
#endif
            }
        }

        /// <summary>
        /// 设置该版本是否为测试版本
        /// </summary>
        /// <param name="isBeta">是否为测试版本</param>
        public void SetIsBetaVersion(bool isBeta)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetIsBetaVersion(isBeta);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetBeta(isBeta);
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
                DumpForPC.SetBeta();
#endif
            }
        }

        /// <summary>
        /// 如果启动了C#堆栈回溯可能会导致某些机型出现宕机
        /// </summary>
        public void EnableCSharpStackTrace()
        {
#if UNITY_ANDROID
            CrasheyeForAndroid.EnableCSharpStackTrace();
#endif
        }


        /// <summary>
        /// 指定获取应用程序log日志的行数
        /// </summary>
        /// <param name="lines">需要获取log行数</param>
        public void SetLogging(int lines)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetLogging(lines);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetLogging(lines);
#endif
            }
        }

        /// <summary>
        /// 获取应用程序log日志关键字过滤
        /// </summary>
        /// <param name="filter">需要过滤关键字</param>
        public void SetLogging(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return;
            }

            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetLogging(filter);
#endif
            }
        }

        /// <summary>
        /// 获取应用程序log日志（过滤条件：关键字过滤+行数）
        /// </summary>
        /// <param name="lines">需要获取的行数</param>
        /// <param name="filter">需要过滤的关键字</param>
        public void SetLogging(LoggingData data)
        {
            if (string.IsNullOrEmpty(data.filter))
            {
                return;
            }

            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetLogging(data.lines, data.filter);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetLogging(data.lines, data.filter);
#endif
            }
        }

        /// <summary>
        /// SetURL
        /// </summary>
        /// <param name="SetUrl"></param>
        public void SetURL(string url)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
#if UNITY_ANDROID
                CrasheyeForAndroid.SetURL(url);
#endif
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                CrasheyeForIOS.SetURL(url);
#endif
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
#if UNITY_STANDALONE_WIN
               DumpForPC.SetURL(url);
#endif
            }
        }
    }
}