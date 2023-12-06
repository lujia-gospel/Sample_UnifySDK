using UnityEngine;

using System.Runtime.InteropServices;
using AOT;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

public class Tracking: MonoBehaviour
{
	private static Tracking _instance = null;

    private static Regex regex = new Regex("^([a-zA-Z])([a-zA-Z0-9_]){0,63}$");

    public static Tracking Instance {
		get {
			if (!_instance) {
				_instance = FindObjectOfType (typeof(Tracking)) as Tracking;
				if (!_instance) {
					GameObject am = new GameObject ("Tracking");
					_instance = am.AddComponent (typeof(Tracking)) as Tracking;
				}
			}
			return _instance;           
		}
	}

    private static List<Action> waitingTaskList = new List<Action>();

    
    private static List<Action> executingTaskList = new List<Action>();

    public static void PostTask(Action task)
    {
        lock (waitingTaskList)
        {
            waitingTaskList.Add(task);
        }
    }

    private void Update()
    {
        lock (waitingTaskList)
        {
            if (waitingTaskList.Count > 0)
            {
                executingTaskList.AddRange(waitingTaskList);

                waitingTaskList.Clear();
            }
        }

        for (int i = 0; i < executingTaskList.Count; ++i)
        {
            Action task = executingTaskList[i];
            try
            {
                task();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, this);
            }
        }

        executingTaskList.Clear();
    }



    void Awake ()
	{
		DontDestroyOnLoad (this);
	}

    public delegate void DeferredDeeplinkCallBack(string result);

    public delegate void AttributionCalllback(string result, int status);


    private DeferredDeeplinkCallBack deferredDeeplinCallback_private = null;

    private AttributionCalllback attributionCalllback_private = null;

#if UNITY_IOS && !UNITY_EDITOR

    internal delegate void _internalDeferredDeeplinkCallBack(string result);

    internal delegate void _internalAttributionCalllback(string result, int status);

    [DllImport ("__Internal")]
	private static extern void _internalInitWithAppKeyAndChannel_Tracking (string appKey, string channelId);

    [DllImport ("__Internal")]
    private static extern void _internalInitWithAppKeyAndChannel_Tracking_newer (string appKey, string channelId,string caid1,string caid2,string oid,string install_params,string startup_params);

	[DllImport ("__Internal")]
	private static extern void _internalSetRegisterWithAccountID_Tracking_newer (string account,string params_string);

    [DllImport ("__Internal")]
	private static extern void _internalSetLoginWithAccountIDAndServerId_Tracking_newer (string account,string serverId,string params_string);

	[DllImport ("__Internal")]
	private static extern void _internalSetRyzf_Tracking_newer (string ryTID, string ryzfType, string hbType, float hbAmount,string params_string);

	[DllImport ("__Internal")]
	private static extern void _internalSetDD_Tracking_newer (string ryTID, string hbType, float hbAmount,string params_string);

	[DllImport ("__Internal")]
	private static extern void _internalSetEvent_Tracking_newer (string EventName,string params_string);

    [DllImport ("__Internal")]
    private static extern void _internalSetTrackViewDuration_Tracking_newer (string viewID,long duration,string params_string);

    [DllImport ("__Internal")]
    private static extern void _internalSetAdShow_Tracking_newer(string adPlatform,string adid,int success,string params_string);

    [DllImport ("__Internal")]
    private static extern void _internalSetAdClick_Tracking_newer(string adPlatform,string adid,string params_string);

    [DllImport ("__Internal")]
    private static extern void _internalSetTrackAppDuration_Tracking_newer(long duration,string params_string);

	[DllImport ("__Internal")]
	private static extern string _internalGetDeviceId_Tracking ();

	[DllImport ("__Internal")]
	private static extern void _internalSetPrintLog_Tracking (bool printLog);

    [DllImport ("__Internal")]
	private static extern void _internalSetASAEnable_Tracking (bool enable);

    [DllImport ("__Internal")]
	private static extern void _internalSetMobDNAEnable_Tracking (bool enable);

    [DllImport("__Internal")]
    private static extern void _internal_SetAttributionCalllbackDelegate_Tracing(_internalAttributionCalllback callback);

    [DllImport("__Internal")]
    private static extern void _internal_SetDeferredDeeplinkCallBack_Tracking(_internalDeferredDeeplinkCallBack callback);


    [DllImport ("__Internal")]
	private static extern void _internalSetIPAdds6Enable_Tracking (bool enable);
#endif


#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaObject getApplicationContext ()
	{
		
		using (AndroidJavaClass jc = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject> ("currentActivity")) {
				return jo.Call<AndroidJavaObject> ("getApplicationContext");
			}
		}
		
		return null;
	}
#endif

    /// <summary>
    /// 初始化方法   
    /// </summary>
    /// <param name="appKey">appKey</param>
    /// <param name="channelId">标识推广渠道的字符</param>
    /// <param name="caid1">caid1 ios 专用字段,广告协会caid字段，默认为空</param>
    /// <param name="caid2">caid2 ios 专用字段,广告协会caid字段，默认为空</param>
    /// <param name="oaid">msa oaid ,android 专用字段</param>
    /// <param name="assetFileName">使用oaid版本1.0.26+以上时，需要此参数，如果传入了oaid参数则不需要此参数（该参数需向msa申请证书进行获取）android 专用字段</param>
    /// <param name="oaidLibraryString">使用oaid版本1.0.26+以上时，需要此参数，如果传入了oaid参数则不需要此参数，根据msa文档1.0.26对应值为 nllvm1623827671，1.0.27对应值为 nllvm1630571663641560568，1.0.30及以上对应值为msaoaidsec android 专用字段</param>
    /// <param name="startupParams">自定义startup参数</param>
    /// <param name="installParams">自定义install参数</param>
    public void init (string appKey, string channelId,string caid1 = null,string caid2 = null,
        string oaid = null,string assetFileName = null, string oaidLibraryString = null,Dictionary<string,object> startupParams = null, Dictionary<string, object> installParams = null)
	{
#if UNITY_IOS && !UNITY_EDITOR
        string  paramstring_install = DictionaryToJsonString(installParams);
        if (paramstring_install == null) {
            paramstring_install = "{}";
        }
        string  paramstring_startup = DictionaryToJsonString(startupParams);
        if (paramstring_startup == null) {
            paramstring_startup = "{}";
        }
		_internalInitWithAppKeyAndChannel_Tracking_newer (appKey, channelId,caid1,caid2,null, paramstring_install,paramstring_startup);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring_install = DictionaryToJsonString(installParams);
        if (paramstring_install == null) {
            paramstring_install = "{}";
        }
        string  paramstring_startup = DictionaryToJsonString(startupParams);
        if (paramstring_startup == null) {
            paramstring_startup = "{}";
        }
	    using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
		    TrackingIO.CallStatic ("initWithKeyAndChannelId", getApplicationContext (), appKey, channelId, oaid,paramstring_startup,paramstring_install,null,assetFileName,oaidLibraryString,false);
	    }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// 玩家服务器注册
    /// </summary>
    /// <param name="account">账号ID</param>
    /// 
    public void register (string account, Dictionary<string, object> customParams = null)
	{
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
		_internalSetRegisterWithAccountID_Tracking_newer(account,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
		using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
			TrackingIO.CallStatic ("setRegisterWithAccountID", account,paramstring);
		}
#else
        Debug.LogError("Current platform not implemented！");
#endif

    }

    /// <summary>
    /// 玩家的账号登陆服务器
    /// </summary>
    /// <param name="account">账号</param>
    public void login (string account,string serverId = null, Dictionary<string, object> customParams = null)
	{
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
		_internalSetLoginWithAccountIDAndServerId_Tracking_newer (account,serverId,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
		using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
			TrackingIO.CallStatic ("setLoginSuccessBusiness", account,serverId,paramstring);
		}
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }


    /// <summary>
    /// 玩家的充值数据
    /// </summary>
    /// <param name="ryTID">交易的流水号</param>
    /// <param name="ryzfType">支付类型</param>
    /// <param name="hbType">货币类型</param>
    /// <param name="hbAmount">支付的真实货币的金额</param>
    public void setryzf (string ryTID, string ryzfType, string hbType, float hbAmount, Dictionary<string, object> customParams = null)
	{
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
		_internalSetRyzf_Tracking_newer (ryTID, ryzfType, hbType, hbAmount,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
		using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
		    TrackingIO.CallStatic ("setPayment", ryTID, ryzfType, hbType, hbAmount,paramstring);
		}
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// 玩家的订单数据
    /// </summary>
    /// <param name="ryTID">交易的流水号</param>
    /// <param name="hbType">货币类型</param>
    /// <param name="hbAmount">支付的真实货币的金额</param>
	public void setDD (string ryTID, string hbType, float hbAmount, Dictionary<string, object> customParams = null)
	{
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
		_internalSetDD_Tracking_newer (ryTID,hbType,hbAmount,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
		using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
		TrackingIO.CallStatic ("setOrder", ryTID, hbType, hbAmount,paramstring);
		}
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// 统计玩家的自定义事件
    /// </summary>
    /// <param name="eventName">事件名 必须为event_1到event_30</param>
    public void setEvent (string eventName, Dictionary<string, object> customParams = null)
	{
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
		_internalSetEvent_Tracking_newer (eventName,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
		using (AndroidJavaClass reyun = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
			reyun.CallStatic ("setEvent", eventName, paramstring);
		}
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// 监测页面展示时长
    /// </summary>
    /// <param name="pageID">页面唯一标识</param>
    /// <param name="duration">页面展示时长</param>
    public void setTrackViewDuration(string pageID,long duration, Dictionary<string, object> customParams = null)
    {
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
        _internalSetTrackViewDuration_Tracking_newer(pageID,duration,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
        using (AndroidJavaClass reyun = new AndroidJavaClass("com.reyun.tracking.sdk.Tracking"))
        {
            reyun.CallStatic("setPageDuration", pageID, duration*1000,paramstring);
        }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// //广告展示时调用
    /// </summary>
    /// <param name="adPlatform">广告平台缩写，如穿山甲广告平台传入 "csj"</param>
    /// <param name="adId">广告位ID</param>
    /// /// <param name="playSuccess">是否展示成功</param>
    public void setTrackAdShow(string adPlatform,string adId,bool playSuccess, Dictionary<string, object> customParams = null)
    {
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
        int successInt = playSuccess ? 1 : 2;
        _internalSetAdShow_Tracking_newer(adPlatform,adId,successInt,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string successString = playSuccess ? "1" : "2";
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
        using (AndroidJavaClass reyun = new AndroidJavaClass("com.reyun.tracking.sdk.Tracking"))
        {
            reyun.CallStatic("setAdShow", adPlatform, adId,successString,paramstring);
        }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// //广告点击时调用
    /// </summary>
    /// <param name="adPlatform">广告平台缩写，如穿山甲广告平台传入 "csj"</param>
    /// <param name="adId">广告位ID</param>
    public void setTrackAdClick(string adPlatform, string adId, Dictionary<string, object> customParams = null)
    {
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
        _internalSetAdClick_Tracking_newer(adPlatform,adId,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
        using (AndroidJavaClass reyun = new AndroidJavaClass("com.reyun.tracking.sdk.Tracking"))
        {
            reyun.CallStatic("setAdClick", adPlatform, adId,paramstring);
        }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    /// <summary>
    /// 统计APP运行时长
    /// </summary>
    /// <param name="duration">时间长度</param>
    public void setTrackAppDuration(long duration, Dictionary<string, object> customParams = null)
    {
#if UNITY_IOS && !UNITY_EDITOR
        string paramstring = DictionaryToJsonString(customParams);
        _internalSetTrackAppDuration_Tracking_newer(duration,paramstring);
#elif UNITY_ANDROID && !UNITY_EDITOR
        string  paramstring = DictionaryToJsonString(customParams);
        if (paramstring == null) {
            paramstring = "{}";
        }
        using (AndroidJavaClass reyun = new AndroidJavaClass("com.reyun.tracking.sdk.Tracking"))
        {
            reyun.CallStatic("setAppDuration", duration*1000,paramstring);
        }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }



    /// <summary>
    /// 获取用户的设备ID信息
    /// </summary>
    public string getDeviceId ()
	{
#if UNITY_IOS && !UNITY_EDITOR
		return _internalGetDeviceId_Tracking ();
#elif UNITY_ANDROID && !UNITY_EDITOR
		string str = "unknown";

		using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
			str = TrackingIO.CallStatic<string> ("getDeviceId");
		}
		return str;
#else
        Debug.LogError("Current platform not implemented！");
        return "unknown";
#endif
    }

	/// 开启日志打印
	public void setPrintLog (bool print)
	{
#if UNITY_IOS && !UNITY_EDITOR
		_internalSetPrintLog_Tracking (print);
#elif UNITY_ANDROID && !UNITY_EDITOR
	  using (AndroidJavaClass TrackingIO = new AndroidJavaClass ("com.reyun.tracking.sdk.Tracking")) {
	        TrackingIO.CallStatic("setDebugMode", print);
	  }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    public void setASAEnable(bool enable)
    {
#if UNITY_IOS && !UNITY_EDITOR
		_internalSetASAEnable_Tracking (enable);
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

    public void setMobDNAEnable(bool enable)
    {
#if UNITY_IOS && !UNITY_EDITOR
		_internalSetMobDNAEnable_Tracking (enable);
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }

#if UNITY_IOS && !UNITY_EDITOR
    [MonoPInvokeCallback(typeof(_internalDeferredDeeplinkCallBack))]
    private static void OnDeeplinkCallback(string msg)
    {
        PostTask(() =>
        {
            if (Tracking.Instance.deferredDeeplinCallback_private != null && msg != null)
            {
                Tracking.Instance.deferredDeeplinCallback_private.Invoke(msg);
            }
        });
    }

    [MonoPInvokeCallback(typeof(_internalAttributionCalllback))]
    private static void OnAttibutionCallback(string result, int status)
    {
        PostTask(() =>
        {
            if (Tracking.Instance.attributionCalllback_private != null)
            {
                Tracking.Instance.attributionCalllback_private.Invoke(result, status);
            }
        });
    }
#endif

    #region iOS install Attribution 
    public void setAttributionCalllbackDelegate(AttributionCalllback callback)
    {
#if UNITY_IOS && !UNITY_EDITOR
        Tracking.Instance.attributionCalllback_private = callback;
        _internal_SetAttributionCalllbackDelegate_Tracing(OnAttibutionCallback);
#elif UNITY_ANDROID && !UNITY_EDITOR
        Tracking.Instance.attributionCalllback_private = callback;
        using (AndroidJavaClass TrackingIO = new AndroidJavaClass("com.reyun.tracking.sdk.Tracking"))
        {
            TrackingIO.CallStatic("setAttributionQueryListener", new AndroidAttributionQuery());
        }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }
    #endregion

    #region iOS deferred deeplink 
    public void setDeferredDeeplinkCalllbackDelegate(DeferredDeeplinkCallBack callback)
    {
#if UNITY_IOS && !UNITY_EDITOR
        Tracking.Instance.deferredDeeplinCallback_private = callback;
        _internal_SetDeferredDeeplinkCallBack_Tracking(OnDeeplinkCallback);
#elif UNITY_ANDROID && !UNITY_EDITOR
        Tracking.Instance.deferredDeeplinCallback_private = callback;
        using (AndroidJavaClass TrackingIO = new AndroidJavaClass("com.reyun.tracking.sdk.Tracking"))
        {
            TrackingIO.CallStatic("setDeepLinkListener", new AndroidDeferredDeeplink());
        }
#else
        Debug.LogError("Current platform not implemented！");
#endif
    }
    #endregion


#if UNITY_ANDROID && !UNITY_EDITOR
    private sealed class AndroidAttributionQuery : AndroidJavaProxy {

        public AndroidAttributionQuery() : base("com.reyun.tracking.utils.IAttributionQueryListener")
        {
        }
        public void onComplete(int paramInt, String param_string) {
            Tracking.PostTask(() =>
            {
                if (Tracking.Instance.attributionCalllback_private != null)
                {
                    Tracking.Instance.attributionCalllback_private.Invoke(param_string, paramInt);
                }
            });
        }
    
    }

    private sealed class AndroidDeferredDeeplink : AndroidJavaProxy
    {

        public AndroidDeferredDeeplink() : base("com.reyun.tracking.utils.IDeepLinkListener")
        {
        }
        public void onComplete(bool isSuccess, String dpUrl, String dpPath)
        {
            Tracking.PostTask(() =>
            {
                if (Tracking.Instance.deferredDeeplinCallback_private != null)
                {
                    string jsonString = "";
                    if (isSuccess)
                    {
                        jsonString += "{\"dp_url\":\"" + (dpUrl != null? dpUrl : "") + "\",";
                        jsonString += "\"dp_path\":\"" + (dpPath != null ? dpPath : "") + "\"}";
                    }
                    else
                    {
                        jsonString = "{}";
                    }
                    Tracking.Instance.deferredDeeplinCallback_private.Invoke(jsonString);
                }
            });

        }

    }
#endif

    private static String DictionaryToJsonString (Dictionary<string, object> dictionary)
    {
        string result = null;
        try
        {
            if (!CheckDictionary(dictionary)) {
                return result;
            }
            result = MiniJSON.Serialize(dictionary);
        }
        catch (Exception e)
        {
            Debug.LogError("ToJsonString error:" + e.Message);
        }
        return result;
    }

    private static bool CheckDictionary(Dictionary<string, object> dictionary)
    {
        if (dictionary == null || dictionary.Count == 0)
        {
            return false;
        }
        foreach (var entry in dictionary)
        {
            if (entry.Key == null || !(entry.Key is string) || !regex.IsMatch(entry.Key))
            {
                return false;
            }
            if (entry.Value == null || !(entry.Value is string || isNumber(entry.Value)))
            {
                return false;
            }
            if (entry.Value is string stringvalue)
            {
                if (stringvalue == null || stringvalue.Length == 0 || !regex.IsMatch(stringvalue))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static bool isNumber(object value)
    {
        if (value == null)
        {
            return false;
        }
        if (value is float || value is int
                    || value is uint
                    || value is long
                    || value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is ulong
                    || value is double
                    || value is decimal)
        {
            return true;
        }
        return false;
    }
}

