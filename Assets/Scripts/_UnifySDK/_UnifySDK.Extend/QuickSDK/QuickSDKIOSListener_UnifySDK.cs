#if UNITY_IOS
using quickgame;
using UnifySDK.Event;
using UnifySDK.Event.Account;
using UnifySDK.Event.Purchase;

namespace UnifySDK
{
    public class QuickSDKIOSListener_UnifySDK : QuickListener
    { 
        public override void onInitSuccess()
        {
            UnifySDKEventSystem.Instance.Publish(new InitSuccessData());  
        }

        public override void onInitFailed(quickgame.ErrorMsg message)
        {
            UnifySDKEventSystem.Instance.Publish(new InitFailedData{errMsg = message.errMsg});
        }

        public override void onLoginSuccess(quickgame.UserInfo userInfo)
        {
            var data = new LoginSuccessData();
            data.uid = userInfo.uid; //sdk用户唯一标识
            data.token = userInfo.token;
            data.userName = userInfo.userName;
            UnifySDKEventSystem.Instance.Publish(data);
        }

        public override void onLoginFailed(quickgame.ErrorMsg errMsg)
        {
            UnifySDKEventSystem.Instance.Publish(new InitFailedData{errMsg =  errMsg.errMsg});
        }

        public override void onLogoutSuccess()
        {
            UnifySDKEventSystem.Instance.Publish(new LogoutSuccessData());
        }

        public override void onPaySuccess(quickgame.PayResult payResult)
        {
            var data= new PurchaseSuccessData();
            data.orderId = payResult.tranactionId; 
            data.cpOrderId = "";
            data.extraParam= payResult.message;
            UnifySDKEventSystem.Instance.Publish(data);
        }

        public override void onPayFailed(quickgame.PayResult payResult)
        {
            var data= new PurchaseFailedData();
            data.orderId = payResult.tranactionId; 
            data.cpOrderId = "";
            data.extraParam= payResult.message;
            UnifySDKEventSystem.Instance.Publish(data);
        }   

        public override void onPayCancel(quickgame.PayResult payResult)
        {
            var data = new PurchaseCancelData();
            data.orderId = payResult.tranactionId; 
            data.cpOrderId = "";
            data.extraParam= payResult.message;
            UnifySDKEventSystem.Instance.Publish(data);
        }

        public override void onRestoreSuccess()
        { 
            UnifySDKEventSystem.Instance.Publish(new RestoreSuccessData());
        }
    }
}
#endif

