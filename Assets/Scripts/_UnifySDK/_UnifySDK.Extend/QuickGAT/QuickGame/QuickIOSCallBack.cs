using UnityEngine;
using System.Collections;
using quickgame_GAT;

public class QuickIOSCallBack : QuickListener
{
    //   void OnGUI() {  
    //	//Kinds of Buttons
    //	if(GUI.Button(getRectByNo(0),"Init"))
    //	{
    //		QKGame.txQKInit();
    //	}
    //       //Login
    //       if(GUI.Button(getRectByNo(1),"Login"))  
    //       {
    //		QKGame.txQKLogin(true);
    //       }
    //	//Center
    //	if(GUI.Button(getRectByNo(2),"Center"))  
    //       {  
    //		QKGame.txQKCenter();
    //       }
    //	//LogOut
    //	if(GUI.Button(getRectByNo(3),"Logout"))  
    //       {  
    //		QKGame.txQKLogout();
    //       }
    //       if (GUI.Button(getRectByNo(4), "AAAA"))
    //       {
    //           onInitSuccess("aaaaa");
    //       }
    //       //Pay
    //       if (GUI.Button(getRectByNo(6),"Pay 1 RMB"))  
    //       {  
    //		QKGame.txQKCongzi("com.yuanbao.1","元宝","1","123","url","extrasParams");
    //       }

    //   }

    // Rect getRectByNo(int no){
    //	return new Rect((Screen.width/2- ((2-no%3)*180)+70),70*(no/3)+200,160,60);
    //}


    // ReSharper disable Unity.PerformanceAnalysis
    private static void DebugLog(string log)
    {
        Debug.Log(log);
    }

    public override void onInitSuccess(ErrorMsg message)
    {
    }

    public override void onInitFailed(ErrorMsg message)
    {
    }

    public override void onLoginSuccess(UserInfo userInfo)
    {
        Debug.Log("onLoginSuccess 22222222222222" + userInfo.uid);
        Debug.Log("onLoginSuccess 444444444" + userInfo.token);


        string uid = userInfo.uid; //sdk用户唯一标识
        string token = userInfo.token;
        bool isGuest = false; //判断是否为游客登录
        string logintype = "";
        //登录方式 6:Facebook,8:Google,10:Twitter,11:Line,1:Guest,13:Email
    }

    public override void onLoginFailed(ErrorMsg errMsg)
    {
        DebugLog("onLoginFailed");
    }

    public override void onLogoutSuccess(ErrorMsg errMsg)
    {
        DebugLog("onLogoutSuccess");
    }

    public override void onPurchaseSuc(PayResult payResult)
    {
        DebugLog("onPaySuccess");
    }

    public override void onPurchaseFail(PayResult payResult)
    {
        DebugLog("onPayFailed");
    }

    public override void onPayCancel(PayResult payResult)
    {
        DebugLog("onPayCancel");
    }

    public override void onRestoreSuccess()
    {
        DebugLog("onRestoreSuccess");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void onFbSharePhotoCallBack(ErrorMsg message, string status)
    {
        DebugLog("onFbSharePhoto");
        switch (status)
        {
            case "0":
                DebugLog("FB分享成功");
                break;
            case "1":
                DebugLog("FB分享失败" + message.errMsg);
                break;
            case "2":
                DebugLog("FB用户取消了分享");
                break;
            default:
                DebugLog("FB分享");
                break;
        }
    }

    public override void onFbShareUrl(ErrorMsg message, string status)
    {
        status += 3;
        DebugLog("onFbShareUrl");
        switch (status)
        {
            case "3":
                DebugLog("FB分享成功");
                break;
            case "4":
                DebugLog("FB分享失败" + message.errMsg);
                break;
            case "5":
                DebugLog("FB用户取消了分享");
                break;
        }
    }

    public override void OnGetUserBindInfo(ErrorMsg message, string status)
    {
        DebugLog("getUserBindInfo");
    }

    public override void onBindSuccess(ErrorMsg message, UserInfo info)
    {
        DebugLog("onIOSBindSuccess");
    }
}