using quicksdk;
using UnifySDK.Event;

namespace UnifySDK
{
    public class QuickSDKListener_UnifySDK : QuickSDKListener
    {
        public override void onInitSuccess()
        {
            UnifySDKEventSystem.Instance.Publish(new InitSuccessData());
        }
    
        public override void onInitFailed(ErrorMsg message)
        {
            UnifySDKEventSystem.Instance.Publish(new InitFailedData{errMsg = message.errMsg});
        }
    
        public override void onLoginSuccess(UserInfo userInfo)
        {
            var data = new LoginSuccessData();
            data.uid = userInfo.uid; //sdk用户唯一标识
            data.token = userInfo.token;
            data.userName = userInfo.userName;
            UnifySDKEventSystem.Instance.Publish(data);
        }
    
        public override void onSwitchAccountSuccess(UserInfo userInfo)
        {
            var data = new SwitchAccountSuccessData();
            data.uid = userInfo.uid; //sdk用户唯一标识
            data.token = userInfo.token;
            data.userName = userInfo.userName;
            UnifySDKEventSystem.Instance.Publish(data);
        }
    
        public override void onLoginFailed(ErrorMsg errMsg)
        {
            UnifySDKEventSystem.Instance.Publish(new LoginFailedData{errMsg = errMsg.errMsg});
        }
    
        public override void onLogoutSuccess()
        {
            UnifySDKEventSystem.Instance.Publish(new LogoutSuccessData());
        }
    
        public override void onSucceed(string infos)
        {
            throw new System.NotImplementedException();
        }
    
        public override void onFailed(string message)
        {
            throw new System.NotImplementedException();
        }
    
        public override void onPaySuccess(PayResult payResult)
        {
            var data= new PurchaseSuccessData();
            data.orderId = payResult.orderId; 
            data.cpOrderId = payResult.cpOrderId;
            data.extraParam= payResult.extraParam;
            UnifySDKEventSystem.Instance.Publish(new PurchaseSuccessData());
        }
    
        public override void onPayFailed(PayResult payResult)
        {
            var data= new PurchaseFailedData();
            data.orderId = payResult.orderId;
            data.cpOrderId = payResult.cpOrderId;
            data.extraParam= payResult.extraParam;
            UnifySDKEventSystem.Instance.Publish(new PurchaseFailedData());
        }
    
        public override void onPayCancel(PayResult payResult)
        {
            var data = new PurchaseCancelData();
            data.orderId = payResult.orderId;
            data.cpOrderId = payResult.cpOrderId;
            data.extraParam = payResult.extraParam;
            UnifySDKEventSystem.Instance.Publish(new PurchaseCancelData());
        }
    
        public override void onExitSuccess()
        {
            UnifySDKEventSystem.Instance.Publish(new ExitSuccessData());
        }
    
        public override void onPrivaceAgree()
        {  
            UnifySDKEventSystem.Instance.Publish(new PrivacyAgreeData());
        }
    
        public override void onPrivaceRefuse()
        {
            UnifySDKEventSystem.Instance.Publish(new PrivacyRefuseData());
        }
    }
}


