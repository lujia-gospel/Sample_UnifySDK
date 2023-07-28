using quicksdk;
using UnifySDK;
using UnifySDK.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickDemo : MonoBehaviour
{
    public GameObject mExitDialogCanvas;

    public void Start()
    {
        BindCallBack();
        mExitDialogCanvas = GameObject.Find("ExitDialog");
        if (mExitDialogCanvas != null)
        {
            mExitDialogCanvas.SetActive(false);
        }
    }

    void showLog(string title, string message)
    {
        Debug.Log("title: " + title + ", message: " + message);
    }

    public void callShowPrivace()
    {
        UnifySDK.UnifySDKManager.Instance.IUnifySDK_Account.ShowPrivacy();
    }

    public void onLogin()
    {
        UnifySDK.UnifySDKManager.Instance.IUnifySDK_Account.Login();
    }

    public void onLogout()
    {
        UnifySDK.UnifySDKManager.Instance.IUnifySDK_Account.Logout();
    }


    public void onPay()
    {
        PurchaseOrderInfo orderInfo = new PurchaseOrderInfo();
        orderInfo.goodsID = "1";
        orderInfo.goodsName = "勾玉";
        orderInfo.goodsDesc = "10个勾玉";
        orderInfo.quantifier = "个";
        orderInfo.extrasParams = "extparma";
        orderInfo.count = 10;
        orderInfo.amount = 1;
        orderInfo.price = 0.1f;
        orderInfo.callbackUrl = "";
        orderInfo.cpOrderID = "cporderidzzw";


        UnifySDKManager.Instance.IUnifySDK_Purchase.Purchase(orderInfo);
        //QuickSDK.getInstance ().pay (orderInfo, gameRoleInfo);
    }
    // public void onEnterYunKeFuCenter()
    // {
    // 	GameRoleInfo gameRoleInfo = new GameRoleInfo();
    // 	gameRoleInfo.gameRoleBalance = "0";
    // 	gameRoleInfo.gameRoleID = "11111";
    // 	gameRoleInfo.gameRoleLevel = "1";
    // 	gameRoleInfo.gameRoleName = "钱多多";
    // 	gameRoleInfo.partyName = "同济会";
    // 	gameRoleInfo.serverID = "1";
    // 	gameRoleInfo.serverName = "火星服务器";
    // 	gameRoleInfo.vipLevel = "1";
    // 	gameRoleInfo.roleCreateTime = "roleCreateTime";
    // 	QuickSDK.getInstance ().enterYunKeFuCenter (gameRoleInfo);
    // }

    // 	public void onCallSDKShare()
    // {
    // 	ShareInfo shareInfo = new ShareInfo();
    // 	shareInfo.title = "这是标题";
    // 	shareInfo.content = "这是描述";
    // 	shareInfo.imgPath = "https://www.baidu.com/";
    // 	shareInfo.imgUrl = "https://www.baidu.com/";
    // 	shareInfo.url = "https://www.baidu.com/";
    // 	shareInfo.type = "url_link";
    // 	shareInfo.shareTo = "0";
    // 	shareInfo.extenal = "extenal";
    // 	QuickSDK.getInstance ().callSDKShare (shareInfo);
    // }

    public void onCreatRole()
    {
        //注：GameRoleInfo的字段，如果游戏有的参数必须传，没有则不用传
        PurchaseRoleInfo gameRoleInfo = new PurchaseRoleInfo();

        gameRoleInfo.gameRoleBalance = "0";
        gameRoleInfo.gameRoleID = "000001";
        gameRoleInfo.gameRoleLevel = "1";
        gameRoleInfo.gameRoleName = "钱多多";
        gameRoleInfo.partyName = "同济会";
        gameRoleInfo.serverID = "1";
        gameRoleInfo.serverName = "火星服务器";
        gameRoleInfo.vipLevel = "1";
        gameRoleInfo.roleCreateTime = "roleCreateTime"; //UC与1881渠道必传，值为10位数时间戳

        gameRoleInfo.gameRoleGender = "男"; //360渠道参数
        gameRoleInfo.gameRolePower = "38"; //360渠道参数，设置角色战力，必须为整型字符串
        gameRoleInfo.partyId = "1100"; //360渠道参数，设置帮派id，必须为整型字符串

        gameRoleInfo.professionId = "11"; //360渠道参数，设置角色职业id，必须为整型字符串
        gameRoleInfo.profession = "法师"; //360渠道参数，设置角色职业名称
        gameRoleInfo.partyRoleId = "1"; //360渠道参数，设置角色在帮派中的id
        gameRoleInfo.partyRoleName = "帮主"; //360渠道参数，设置角色在帮派中的名称
        gameRoleInfo.friendlist = "无"; //360渠道参数，设置好友关系列表，格式请参考：http://open.quicksdk.net/help/detail/aid/190
        gameRoleInfo.SubmitType = 2;
        UnifySDKManager.Instance.IUnifySDK_Purchase.SubmitPurchaseRoleInfo(gameRoleInfo);
    }

    public void onEnterGame()
    {
        PurchaseRoleInfo gameRoleInfo = new PurchaseRoleInfo();

        gameRoleInfo.gameRoleBalance = "0";
        gameRoleInfo.gameRoleID = "11111";
        gameRoleInfo.gameRoleLevel = "1";
        gameRoleInfo.gameRoleName = "钱多多";
        gameRoleInfo.partyName = "同济会";
        gameRoleInfo.serverID = "1";
        gameRoleInfo.serverName = "火星服务器";
        gameRoleInfo.vipLevel = "1";
        gameRoleInfo.roleCreateTime = "roleCreateTime"; //UC与1881渠道必传，值为10位数时间戳

        gameRoleInfo.gameRoleGender = "男"; //360渠道参数
        gameRoleInfo.gameRolePower = "38"; //360渠道参数，设置角色战力，必须为整型字符串
        gameRoleInfo.partyId = "1100"; //360渠道参数，设置帮派id，必须为整型字符串

        gameRoleInfo.professionId = "11"; //360渠道参数，设置角色职业id，必须为整型字符串
        gameRoleInfo.profession = "法师"; //360渠道参数，设置角色职业名称
        gameRoleInfo.partyRoleId = "1"; //360渠道参数，设置角色在帮派中的id
        gameRoleInfo.partyRoleName = "帮主"; //360渠道参数，设置角色在帮派中的名称
        gameRoleInfo.friendlist = "无"; //360渠道参数，设置好友关系列表，格式请参考：http://open.quicksdk.net/help/detail/aid/190
        gameRoleInfo.SubmitType = 1;

        UnifySDKManager.Instance.IUnifySDK_Purchase.SubmitPurchaseRoleInfo(gameRoleInfo); //开始游戏
        SceneManager.LoadScene("QuickDemoScene4");
    }

    public void onUpdateRoleInfo()
    {
        PurchaseRoleInfo gameRoleInfo = new PurchaseRoleInfo();
        //注：GameRoleInfo的字段，如果游戏有的参数必须传，没有则不用传

        gameRoleInfo.gameRoleBalance = "0";
        gameRoleInfo.gameRoleID = "11111";
        gameRoleInfo.gameRoleLevel = "1";
        gameRoleInfo.gameRoleName = "钱多多";
        gameRoleInfo.partyName = "同济会";
        gameRoleInfo.serverID = "1";
        gameRoleInfo.serverName = "火星服务器";
        gameRoleInfo.vipLevel = "1";
        gameRoleInfo.roleCreateTime = "roleCreateTime"; //UC与1881渠道必传，值为10位数时间戳

        gameRoleInfo.gameRoleGender = "男"; //360渠道参数
        gameRoleInfo.gameRolePower = "38"; //360渠道参数，设置角色战力，必须为整型字符串
        gameRoleInfo.partyId = "1100"; //360渠道参数，设置帮派id，必须为整型字符串

        gameRoleInfo.professionId = "11"; //360渠道参数，设置角色职业id，必须为整型字符串
        gameRoleInfo.profession = "法师"; //360渠道参数，设置角色职业名称
        gameRoleInfo.partyRoleId = "1"; //360渠道参数，设置角色在帮派中的id
        gameRoleInfo.partyRoleName = "帮主"; //360渠道参数，设置角色在帮派中的名称
        gameRoleInfo.friendlist = "无"; //360渠道参数，设置好友关系列表，格式请参考：http://open.quicksdk.net/help/detail/aid/190
        gameRoleInfo.SubmitType = 3;

        UnifySDKManager.Instance.IUnifySDK_Purchase.SubmitPurchaseRoleInfo(gameRoleInfo); //开始游戏
    }

    public void onNext()
    {
        SceneManager.LoadScene("QuickDemoScene3");
    }

    public void onCallFunctionWithParamTest()
    {
        //QuickSDK.getInstance().callFunctionWithParams(FuncType.QUICK_SDK_FUNC_TYPE_URL, "https://hotfix.public.manasisrefrain.com/Resources/GaCha/8003/8003.html");
    }

    public void onExit()
    {
        // if(QuickSDK.getInstance().isChannelHasExitDialog ()){
        // 	QuickSDK.getInstance().exit();
        // }else{
        // 	//游戏调用自身的退出对话框，点击确定后，调用QuickSDK的exit()方法
        // 	mExitDialogCanvas.SetActive(true);
        // }
        mExitDialogCanvas.SetActive(true);
    }

    public void onExitCancel()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.CanQuitGame();
        mExitDialogCanvas.SetActive(false);
    }

    public void onExitConfirm()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.QuitGame();
        mExitDialogCanvas.SetActive(false);
    }

    public void onShowToolbar()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.ShowMenu();
    }

    public void onHideToolbar()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.DismissMenu();
    }

    public void onEnterUserCenter()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.ShowUserCenter();
    }

    // public void onEnterBBS()
    // {
    // 	QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_ENTER_BBS);
    // }
    // public void onEnterCustomer()
    // {
    // 	QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_ENTER_CUSTOMER_CENTER);
    // }
    public void onGetGoodsInfos()
    {
        UnifySDKManager.Instance.IUnifySDK_Purchase.QueryGoods();
    }

    //    public void onUserId()
    // {
    // 	string uid = QuickSDK.getInstance ().userId();
    // 	showLog("userId", uid);
    // }
    public void ongetDeviceId()
    {
        string deviceId = UnifySDKManager.Instance.IUnifySDK.GetDeviceId();
        showLog("deviceId", deviceId);
    }

    public void onChannelType()
    {
        int type = int.Parse(UnifySDKManager.Instance.IUnifySDK.GetChannelID());
        showLog("channelType", "" + type);
    }
    // public void onFuctionSupport(int type)
    // {
    // 	bool supported = QuickSDK.getInstance ().isFunctionSupported ((FuncType)type);
    // 	showLog("fuctionSupport", supported?"yes":"no");
    // }
    // public void onGetConfigValue(string key)
    // {
    // 	string value = QuickSDK.getInstance ().getConfigValue (key);
    // 	showLog("onGetConfigValue", key + ": "+value);
    // }

    // public void onOk()
    // {
    // 	messageBox.SetActive (false);
    // }

    // public void onPauseGame()
    // {
    // 	Time.timeScale = 0;
    // 	QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_PAUSED_GAME);
    // }

    public void onResumeGame()
    {
        Time.timeScale = 1;
    }

    //************************************************************以下是需要实现的回调接口*************************************************************************************************************************
    //callback

    public void BindCallBack()
    {
        onInitSuccess();
        onInitFailed();
        onLoginSuccess();
        onSwitchAccountSuccess();
        onLogoutSuccess();
        onLoginFailed();
        onPaySuccess();
        onPayCancel();
        onPayFailed();
        onExitSuccess();
        onPrivaceAgree();
        onPrivaceRefuse();
    }

    public void onInitSuccess()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnInitSuccess.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnInitSuccess.Handler += (objs, eventArgs) =>
        {
            showLog("onInitSuccess", "");
        };
    }

    public void onInitFailed()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnInitFailed.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnInitFailed.Handler += (_data, eventArgs) =>
        {
            showLog("onInitFailed", "msg: " + ((InitFailedData)_data).errMsg);
        };
    }

    public void onLoginSuccess()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnLoginSuccess.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnLoginSuccess.Handler += (_data, eventArgs) =>
        {
            var st = QuickSDK.getInstance().getOaid();
            showLog("QuickSDK.getInstance().getOaid();", "uid: " + st);
            // var data = (LoginSuccessData)_data;
            // showLog ("onLoginSuccess", "uid: " + data.uid + " ,username: " + data.userName + " ,userToken: " + data.token + ", msg: " + data.errMsg);
            // SceneManager.LoadScene ("QuickDemoScene2");
        };
    }

    public void onSwitchAccountSuccess()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnSwitchAccountSuccess.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnSwitchAccountSuccess.Handler += (_data, eventArgs) =>
        {
            //切换账号成功，清除原来的角色信息，使用获取到新的用户信息，回到进入游戏的界面，不需要再次调登录
            var data = (SwitchAccountSuccessData)_data;
            showLog("onSwitchAccountSuccess",
                "uid: " + data.uid + " ,username: " + data.userName + " ,userToken: " + data.token + ", msg: " +
                data.errMsg);
            SceneManager.LoadScene("QuickDemoScene2");
        };
    }

    public void onLoginFailed()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnLoginFailed.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnLoginFailed.Handler += (_data, eventArgs) =>
        {
            var data = (LoginFailedData)_data;
            showLog("onLoginFailed", "msg: " + data.errMsg);
        };
    }

    public void onLogoutSuccess()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnLogoutSuccess.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnLogoutSuccess.Handler += (_data, eventArgs) =>
        {
            showLog("onLogoutSuccess", "");
            //注销成功后回到登陆界面
            SceneManager.LoadScene("QuickDemoScene1");
        };
    }


    public void onPaySuccess()
    {
        UnifySDKManager.Instance.IUnifySDK_Purchase.OnPurchaseSuccess.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Purchase.OnPurchaseSuccess.Handler += (_data, eventArgs) =>
        {
            var data = (PurchaseSuccessData)_data;
            showLog("onPaySuccess",
                "orderId: " + data.orderId + ", cpOrderId: " + data.cpOrderId + " ,extraParam" + data.extraParam);
        };
    }

    public void onPayCancel()
    {
        UnifySDKManager.Instance.IUnifySDK_Purchase.OnPurchaseCancel.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Purchase.OnPurchaseCancel.Handler += (_data, eventArgs) =>
        {
            var data = (PurchaseCancelData)_data;
            showLog("onPayCancel",
                "orderId: " + data.orderId + ", cpOrderId: " + data.cpOrderId + " ,extraParam" + data.extraParam);
        };
    }

    public void onPayFailed()
    {
        UnifySDKManager.Instance.IUnifySDK_Purchase.OnPurchaseFailed.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Purchase.OnPurchaseFailed.Handler += (_data, eventArgs) =>
        {
            var data = (PurchaseCancelData)_data;
            showLog("onPayFailed",
                "orderId: " + data.orderId + ", cpOrderId: " + data.cpOrderId + " ,extraParam" + data.extraParam);
        };
    }

    public void onExitSuccess()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnExitSuccess.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnExitSuccess.Handler += (_data, eventArgs) =>
        {
            showLog("onExitSuccess", "");
            //退出成功的回调里面调用  QuickSDK.getInstance ().exitGame ();  即可实现退出游戏，杀进程。为避免与渠道发生冲突，请不要使用  Application.Quit ();
            Application.Quit();
        };
    }

    // public  void onSucceed(string infos)
    // {
    // 
    //     showLog("onSucceed", infos);
    // }
    //
    // public  void onFailed(string message)
    // {
    //     showLog("onFailed", "msg: " + message);
    // }

    public void onPrivaceAgree()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnPrivacyAgree.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnPrivacyAgree.Handler += (_data, eventArgs) =>
        {
            showLog("onPrivaceAgree", "onPrivaceAgree");
        };
    }

    public void onPrivaceRefuse()
    {
        UnifySDKManager.Instance.IUnifySDK_Account.OnPrivacyRefuse.ClearEventHandler();
        UnifySDKManager.Instance.IUnifySDK_Account.OnPrivacyRefuse.Handler += (_data, eventArgs) =>
        {
            showLog("onPrivaceRefuse", "onPrivaceRefuse");
        };
    }
}