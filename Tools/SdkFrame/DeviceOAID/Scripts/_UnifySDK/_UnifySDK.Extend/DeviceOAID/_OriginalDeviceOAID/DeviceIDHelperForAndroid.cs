using System;
using System.Collections.Generic;
using UnityEngine;
public class DeviceIDHelperForAndroid:MonoBehaviour
{  
    float timer = 0;
    bool waiting = false;
    public void waitResult(float timeout = 5)
    {
        timer = timeout;
        waiting = true;
    }
    
    public Action<string> cbFunc = null;
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                waiting = false;
                timer = 0;
                if (cbFunc != null)
                {
                    cbFunc(null);
                }
            }
        }
    }
    
    public void onOAIDRecv(string oaid)
    {
        if (cbFunc != null && waiting)
        {
            cbFunc(oaid);
            waiting = false;
            timer = 0;
        }
    }
}

/**
 * oaid sdk定义的错误内容
 */
public enum OAIDSdkErrorCode
{
    INIT_INFO_RESULT_OK = 1008610,//调用成功，获取接口是同步的

    INIT_ERROR_MANUFACTURER_NOSUPPORT = 1008611,// 不支持的厂商

    INIT_ERROR_DEVICE_NOSUPPORT = 1008612,//不支持的设备

    INIT_ERROR_LOAD_CONFIGFILE = 1008613,//加载配置文件出错

    INIT_ERROR_RESULT_DELAY = 1008614,//调用成功，获取接口是 异步的

    INIT_ERROR_SDK_CALL_ERROR = 1008615,//sdk 调用出错
    
    INIT_ERROR_CERT_ERROR=1008616,//证书未初始化或证书无效
}

public class DeviceIDHelper
{        
    static string helperName = "DeviceHelper_Instance_Dont_Delete";
    static DeviceIDHelper _inst = null;
    public static DeviceIDHelper inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = new DeviceIDHelper();
            }
            return _inst;
        }
    }
    DeviceIDHelperForAndroid helper = null;
    public DeviceIDHelper()
    {
        var obj = GameObject.Find(helperName);
        if (null == obj)
        {
            helper = new GameObject(helperName).AddComponent<DeviceIDHelperForAndroid>();
            GameObject.DontDestroyOnLoad(helper.gameObject);
        } 
        else
        {
            helper = obj.GetComponent<DeviceIDHelperForAndroid>();
            
        }
    }

    
    /**
     * get OAID
     */
    public void GetDeviceOAID(Action<string> action)
    {
        helper.cbFunc = action;
#if UNITY_ANDROID
        List<string> result = new List<string>();
        try
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                
                using (var jc = new AndroidJavaClass("com.unity.androidplugin.OAIDHelper"))
                {
                    AndroidJavaObject oaHelper = jc.CallStatic<AndroidJavaObject>("inst");
                    helper.waitResult();
                    var res = (OAIDSdkErrorCode)oaHelper.Call<int>("getDeviceIds", context, true,false,false);
                    string err = "";
                    switch (res)
                    {
                        case OAIDSdkErrorCode.INIT_ERROR_DEVICE_NOSUPPORT: //不支持的设备
                            Debug.LogError("  不支持的设备    "); 
                            break;
                        case OAIDSdkErrorCode.INIT_ERROR_LOAD_CONFIGFILE: //加载配置文件出错
                            Debug.LogError("  加载配置文件出错    "); 
                            break;
                        case OAIDSdkErrorCode.INIT_ERROR_MANUFACTURER_NOSUPPORT: //不支持的设备厂商
                            Debug.LogError("  不支持的设备厂商    "); 
                            break;
                        case OAIDSdkErrorCode.INIT_ERROR_SDK_CALL_ERROR: //反射调用出错
                            Debug.LogError("  sdk 调用出错    ");
                            break;
                        case OAIDSdkErrorCode.INIT_INFO_RESULT_OK:
                            break;
                    }

                    if ( res != OAIDSdkErrorCode.INIT_INFO_RESULT_OK && res != OAIDSdkErrorCode.INIT_ERROR_RESULT_DELAY)
                    {
                        helper.cbFunc?.Invoke(null);
                        helper.cbFunc = null;
                    }
                }
            }
        }
        catch (Exception err)
        {
            Debug.LogError("  不支持的设备    "); 
            helper.cbFunc?.Invoke(null);
            helper.cbFunc = null;
        }
#endif
    }
}