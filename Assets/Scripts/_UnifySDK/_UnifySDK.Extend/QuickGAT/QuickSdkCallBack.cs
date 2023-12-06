using UnifySDK.Event;
using UnifySDK.Event.Account;
using UnifySDK.Event.Purchase;
using UnityEngine;

namespace UnifySDK
{
    public class QuickSdkCallBack : Singleton<QuickSdkCallBack>
    {
        public void OnInitSuccess(bool isSuccess)
        {
            if (isSuccess)
            {
                UnifySDKEventSystem.Instance.Publish(new InitSuccessData());
            }
            else
            {
                UnifySDKEventSystem.Instance.Publish(new InitFailedData {errMsg = ""});
            }
        }

        public void OnLoginFinished(int state, AndroidJavaObject userInfo)
        {
             
        }

        public void OnLogout()
        {
            UnifySDKEventSystem.Instance.Publish(new LogoutSuccessData());
        }

        public void OnPurchaseSuccess(string orderId, string orderNo, string goodsId)
        {
            var data = new PurchaseSuccessData();
            data.orderId = orderId;
            data.cpOrderId = orderNo;
            data.extraParam = goodsId;
            UnifySDKEventSystem.Instance.Publish(data);
        }


        public void OnPurchaseFail(string msg)
        {
            var data = new PurchaseFailedData();
            data.orderId = "";
            data.cpOrderId = "";
            data.extraParam = msg;
            UnifySDKEventSystem.Instance.Publish(data);
        }

        public void OnPurchaseCancel(string msg)
        {
            var data = new PurchaseCancelData();
            data.orderId = "";
            data.cpOrderId = "";
            data.extraParam = msg;
            UnifySDKEventSystem.Instance.Publish(data);
        }

        public void OnfbSharePhoto(string msg)
        {
        }

        public void onBindInfoChanged(string uid, bool success)
        {
        }

        public void onExitUserCenter(string msg)
        {
        }

        public void onGetFBToken(bool isSuccess, string token)
        {
        }

        public void onNewFBToken(string token)
        {
        }

        public void onFBMessageReceived(string title, string body)
        {
        }
    }

    public class FinishInitCallBack : AndroidJavaProxy
    {
        public FinishInitCallBack() : base("com.zrg.mini4wd.activitys.SDKCallBack")
        {
        }


        public void onInitFinished(bool var1)
        {
            QuickSdkCallBack.Get().OnInitSuccess(var1);
        }

        public void onLoginFinished(AndroidJavaObject QGUserData, AndroidJavaObject QGUserHolder)
        {
            int loginState = QGUserHolder.Call<int>("getStateCode");
            QuickSdkCallBack.Get().OnLoginFinished(loginState, QGUserData);
        }

        public void onLogout()
        {
            QuickSdkCallBack.Get().OnLogout();
        }

        public void onGooglePlaySub(string var1, string var2, bool var3, bool var4)
        {
        }

        public void onPaySuccess(string orderId, string orderNo, string goodsId, string extrasParams)
        {
             
            QuickSdkCallBack.Get().OnPurchaseSuccess(orderId, orderNo, goodsId);
        }

        public void onPayFailed(string orderId, string orderNo, string msg)
        {
            Debug.Log("-----onPayFailed-----" + msg);
            //AndroidUtility.Debug_LogInfo(AndroidUtility.LogType.Log, "购买失败---", orderId, orderNo, msg);
            QuickSdkCallBack.Get().OnPurchaseFail(msg);
        }

        public void onPayCancel(string orderId, string orderNo, string msg)
        {
            Debug.Log("-----onPayCancel-----" + msg);
            //AndroidUtility.Debug_LogInfo(AndroidUtility.LogType.Log, "购买取消---", orderId, orderNo, msg);
            QuickSdkCallBack.Get().OnPurchaseCancel(msg);
        }

        public void onShareSuccess(string msg)
        {
            Debug.Log("-----onShareSuccess-----" + msg);
            QuickSdkCallBack.Get().OnfbSharePhoto(msg);
        }

        public void onShareCancel(string msg)
        {
            Debug.Log("-----onShareCancel-----" + msg);
            QuickSdkCallBack.Get().OnfbSharePhoto(msg);
        }

        public void onShareFailed(string msg)
        {
            Debug.Log("-----onShareFailed-----" + msg);
            QuickSdkCallBack.Get().OnfbSharePhoto(msg);
        }

        public void onBindInfoChanged(string uid, bool success)
        {
            Debug.Log("-----onBindInfoChanged-----" + uid);
            QuickSdkCallBack.Get().onBindInfoChanged(uid, success);
        }

        public void onExitUserCenter(string msg)
        {
            Debug.Log("-----onExitUserCenter-----" + msg);
            QuickSdkCallBack.Get().onExitUserCenter(msg);
        }

        public void onGetFBToken(bool isSuccess, string token)
        {
            Debug.Log("-----onGetFBTokenisSuccess-----" + isSuccess);
            Debug.Log("-----onGetFBToken-----" + token);
            QuickSdkCallBack.Get().onGetFBToken(isSuccess, token);
        }

        public void onNewFBToken(string token)
        {
            Debug.Log("-----onNewFBToken-----" + token);
            QuickSdkCallBack.Get().onNewFBToken(token);
        }

        public void onFBMessageReceived(string title, string body)
        {
            Debug.Log("-----onFBMessageReceived-----" + title + "body:" + body);
            QuickSdkCallBack.Get().onFBMessageReceived(title, body);
        }
    }
}