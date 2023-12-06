using System;
using com.ultrasdk.unity;
using com.ultrasdk.unity.Entry;
using UnifySDK.Event;
using UnifySDK.Event.Account;
using UnityEngine;

namespace UnifySDK
{
    public class UltraSDK_Mono : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log("OnEnable  "); 
            UltraSDK.Instance.AndroidExtra()?.AddExitListener(OnExitCallBack);
        }

        private void OnDisable()
        {
            Debug.Log("OnEnable  "); 
            UltraSDK.Instance.AndroidExtra()?.RemoveExitListener(OnExitCallBack);
        } 
        
        private void OnExitCallBack(UltraSDKResult result, string msg)
        {
            if (result == UltraSDKResult.UltraSDKResultSuccess)
            {
                Debug.Log("onExit Success");
                UnifySDKEventSystem.Instance.Publish(new ExitSuccessData {errMsg = msg});
            }
            else
            {
                Debug.Log("onExit Faliure");
                UnifySDKEventSystem.Instance.Publish(new ExitFailedData {errMsg = msg});
            }
        }
    }
}