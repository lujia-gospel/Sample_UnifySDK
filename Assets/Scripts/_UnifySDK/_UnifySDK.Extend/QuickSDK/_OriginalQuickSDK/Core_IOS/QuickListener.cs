#if UNITY_IOS
using UnityEngine;
using System.Collections;

namespace quickgame
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
    }
    
    // 支付信息，支付回调中使用
    public class PayResult
    {
        public string message;
        public string code;
        public string productId; 
        public string tranactionId;
    }

    // QuickListener
    public abstract class QuickListener : MonoBehaviour
    {
        //callback
		public abstract void onInitSuccess();

		public abstract void onInitFailed(ErrorMsg message);

        public abstract void onLoginSuccess(UserInfo userInfo);

		public abstract void onLoginFailed(ErrorMsg errMsg);

        public abstract void onLogoutSuccess();

        public abstract void onPaySuccess(PayResult payResult);

        public abstract void onPayFailed(PayResult payResult);

        public abstract void onPayCancel(PayResult payResult);

        public abstract void onRestoreSuccess();


        //callback end


		public void onInitSuccess(string msg)
		{
			onInitSuccess();
		}

		public void onInitFailed(string msg)
		{
			var data = SimpleJSON.JSONNode.Parse(msg);
			ErrorMsg errMsg = new ErrorMsg();
			errMsg.errMsg =  data["msg"].Value;
			
			onInitFailed(errMsg);
		}

        public void onLoginSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            UserInfo userInfo = new UserInfo();
            userInfo.uid = data["userId"].Value;
            userInfo.token = data["userToken"].Value;
            userInfo.userName = data["userName"].Value;
            userInfo.errMsg = data["msg"].Value;

            onLoginSuccess(userInfo);
        }

        public void onLoginFailed(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
			ErrorMsg errMsg = new ErrorMsg();
			errMsg.errMsg = data["msg"].Value;
			
			onLoginFailed(errMsg);
        }

        public void onLogoutSuccess(string msg)
        {
            onLogoutSuccess();
        }



        public void onPaySuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            PayResult result = new PayResult();
            result.code = data["code"].Value;
            result.message = data["message"].Value;
            result.productId = data["productId"].Value;
            result.tranactionId = data["tranactionId"].Value;
            
            onPaySuccess(result);
        }

        public void onPayFailed(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            PayResult result = new PayResult();
            result.code = data["code"].Value;
            result.message = data["message"].Value;
            result.productId = data["productId"].Value;
            result.tranactionId = data["tranactionId"].Value;
            onPayFailed(result);
        }

        public void onPayCancel(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);
            PayResult result = new PayResult();
            result.code = data["code"].Value;
            result.message = data["message"].Value;
            result.productId = data["productId"].Value;
            result.tranactionId = data["tranactionId"].Value;
            onPayCancel(result);
        }

        public void onRestoreSuccess(string msg)
        {
            var data = SimpleJSON.JSONNode.Parse(msg);

            onRestoreSuccess();
        }

		
        

    }
}
#endif
