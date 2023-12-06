using UnityEngine;
using System.Collections;

namespace quickgame_GAT
{
    // 错误信息
    public class ErrorMsg
    {
        public string errMsg;
    }
    
    // 用户信息，登录回调中使用
    public class UserInfo : ErrorMsg
    {
        public string uid;
        public string userName;
        public string token;
        public int Bindtype;
    }
    
    // 支付信息，支付回调中使用
    public class PayResult
    {
        public string productIdentifier;
        public string message;
        public string orderNo;
        public string CPderNo;
    }
    
    

    // QuickListener
    public abstract class QuickListener : MonoBehaviour
    {
        //callback
		public abstract void onInitSuccess(ErrorMsg message);

		public abstract void onInitFailed(ErrorMsg message);

        public abstract void onLoginSuccess(UserInfo userInfo);

		public abstract void onLoginFailed(ErrorMsg errMsg);

        public abstract void onLogoutSuccess(ErrorMsg errMsg);

        public abstract void onPurchaseSuc(PayResult payResult);

        public abstract void onPurchaseFail(PayResult payResult);

        public abstract void onPayCancel(PayResult payResult);

        public abstract void onRestoreSuccess();

        public abstract void onFbSharePhotoCallBack(ErrorMsg message, string status);
        
        public abstract void onFbShareUrl(ErrorMsg message, string status);
        //callback end
        public abstract void OnGetUserBindInfo(ErrorMsg message, string status);
        
        public abstract void onBindSuccess( ErrorMsg message,UserInfo info);
        
		public void onInitSuccess(string msg)
		{
            Debug.Log("onInitSuccess 到这了吗1111"+ msg);
            ErrorMsg errMsg = new ErrorMsg();
            errMsg.errMsg = msg;
            onInitSuccess(errMsg);
            Debug.Log("onInitSuccess 到这了吗2222" + msg);
        }

		public void onInitFailed(string msg)
		{
			ErrorMsg errMsg = new ErrorMsg();
			errMsg.errMsg = msg;
			
			onInitFailed(errMsg);
		}

        public void onLoginSuccess(string msg)
        {
            Debug.Log("onLoginSuccess 1");
            string[] sArray = msg.Split('_');
            Debug.Log("onLoginSuccess 2");
            UserInfo userInfo = new UserInfo();
            Debug.Log("onLoginSuccess 3");
            userInfo.uid = sArray[0];
            Debug.Log("onLoginSuccess 4");
            userInfo.token = sArray[1];
            Debug.Log("onLoginSuccess 5");
            onLoginSuccess(userInfo);
            Debug.Log("onLoginSuccess 6");

        }

        public void onLoginFailed(string msg)
        {
            Debug.Log("onLoginFailed 1");
            var data = SimpleJSON.JSONNode.Parse(msg);
			ErrorMsg errMsg = new ErrorMsg();
			errMsg.errMsg = data["msg"].Value;
			
			onLoginFailed(errMsg);
        }

        public void onLogoutSuccess(string msg)
        {
            Debug.Log("onLogoutSuccess 1");
            ErrorMsg errMsg = new ErrorMsg();
            errMsg.errMsg = msg;
            onLogoutSuccess(errMsg);
        }



        public void onPurchaseSuc(string msg)
        {

            string[] sArray = msg.Split('_');
            PayResult result = new PayResult {productIdentifier = sArray[0]};
            Debug.Log("onLoginSuccess 4");
            result.orderNo = sArray[1];
            Debug.Log("onLoginSuccess 5");
            result.CPderNo = sArray[2];
            onPurchaseSuc(result);
            Debug.Log("onLoginSuccess 6");
        }

        public void onPurchaseFail(string msg)
        {
            PayResult result = new PayResult();
            onPurchaseFail(result);

        }

        public void onPayCancel(string msg)
        {
            PayResult result = new PayResult();
            onPayCancel(result);
        }

        public void onRestoreSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);

            onRestoreSuccess();
        }
        
        public void onFbSharePhoto(string msg)
        {
            var sArray = msg.Split('_');

            var errMsg = new ErrorMsg {errMsg = sArray[0]};
            onFbSharePhotoCallBack(errMsg , sArray[1]);
        }
        
        public void onFbShareUrl(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            var sArray = msg.Split('_');

            var errMsg = new ErrorMsg {errMsg = sArray[0]};
            onFbShareUrl(errMsg , sArray[1]);
        }
        
        public void getUserBindInfo(string msg)
        {
            string data = SimpleJSON.JSONNode.Parse(msg);
            var errMsg = new ErrorMsg {errMsg = ""};
            OnGetUserBindInfo(errMsg , data);
        }
        
        
        public void onBindSuccess(string msg)
        {
            var sArray = msg.Split('_'); 
            var errMsg = new ErrorMsg {errMsg = ""};
            var userInfo = new UserInfo {uid = sArray[0], token = sArray[1], Bindtype = int.Parse(sArray[2])};  
            onBindSuccess( errMsg,userInfo);
        } 

    }
}
