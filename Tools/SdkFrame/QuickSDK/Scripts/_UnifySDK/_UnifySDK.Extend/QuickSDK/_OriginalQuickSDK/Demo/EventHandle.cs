using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using quicksdk;
using System;

public class EventHandle : QuickSDKListener {

	public GameObject messageBox;
	public GameObject mExitDialogCanvas;
	void showLog(string title, string message)
	{
		Debug.Log ("title: " + title + ", message: " + message);
	}
	// Use this for initialization
	void Start () {
		Debug.Log ("lyy  start " );
  

        QuickSDK.getInstance ().setListener (this);
	
		mExitDialogCanvas = GameObject.Find ("ExitDialog");
		if (mExitDialogCanvas != null) {
			mExitDialogCanvas.SetActive (false);
		}

	}

    // Update is called once per frame
    //void Update () {
    //if(Input.GetKeyDown(KeyCode.Escape))  
    //{  

    //}  
    //}


    public void callShowPrivace()
    {
        QuickSDK.getInstance().showPrivace();
    }

    public void reInit()
	{
		QuickSDK.getInstance().reInit();
        
	}
	public void onLogin()
	{
		//ongetDeviceId();
		QuickSDK.getInstance ().login ();
	}

	public void onLogout()
	{
		QuickSDK.getInstance ().logout ();

	}


	public void onPay()
	{
		OrderInfo orderInfo = new OrderInfo();
		GameRoleInfo gameRoleInfo = new GameRoleInfo();
		
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
		
		gameRoleInfo.gameRoleBalance = "0";
		gameRoleInfo.gameRoleID = "11111";
		gameRoleInfo.gameRoleLevel = "1";
		gameRoleInfo.gameRoleName = "钱多多";
		gameRoleInfo.partyName = "同济会";
		gameRoleInfo.serverID = "1";
		gameRoleInfo.serverName = "火星服务器";
		gameRoleInfo.vipLevel = "1";
		gameRoleInfo.roleCreateTime = "roleCreateTime";
		QuickSDK.getInstance ().pay (orderInfo, gameRoleInfo);
	}
	public void onEnterYunKeFuCenter()
	{
		GameRoleInfo gameRoleInfo = new GameRoleInfo();
		gameRoleInfo.gameRoleBalance = "0";
		gameRoleInfo.gameRoleID = "11111";
		gameRoleInfo.gameRoleLevel = "1";
		gameRoleInfo.gameRoleName = "钱多多";
		gameRoleInfo.partyName = "同济会";
		gameRoleInfo.serverID = "1";
		gameRoleInfo.serverName = "火星服务器";
		gameRoleInfo.vipLevel = "1";
		gameRoleInfo.roleCreateTime = "roleCreateTime";
		QuickSDK.getInstance ().enterYunKeFuCenter (gameRoleInfo);
	}

		public void onCallSDKShare()
	{
		ShareInfo shareInfo = new ShareInfo();
		shareInfo.title = "这是标题";
		shareInfo.content = "这是描述";
		shareInfo.imgPath = "https://www.baidu.com/";
		shareInfo.imgUrl = "https://www.baidu.com/";
		shareInfo.url = "https://www.baidu.com/";
		shareInfo.type = "url_link";
		shareInfo.shareTo = "0";
		shareInfo.extenal = "extenal";
		QuickSDK.getInstance ().callSDKShare (shareInfo);
	}
	
	public void onCreatRole(){
		//注：GameRoleInfo的字段，如果游戏有的参数必须传，没有则不用传
		GameRoleInfo gameRoleInfo = new GameRoleInfo();
		
		gameRoleInfo.gameRoleBalance = "0";
		gameRoleInfo.gameRoleID = "000001";
		gameRoleInfo.gameRoleLevel = "1";
		gameRoleInfo.gameRoleName = "钱多多";
		gameRoleInfo.partyName = "同济会";
		gameRoleInfo.serverID = "1";
		gameRoleInfo.serverName = "火星服务器";
		gameRoleInfo.vipLevel = "1";
		gameRoleInfo.roleCreateTime = "roleCreateTime";//UC与1881渠道必传，值为10位数时间戳

		gameRoleInfo.gameRoleGender = "男";//360渠道参数
		gameRoleInfo.gameRolePower="38";//360渠道参数，设置角色战力，必须为整型字符串
		gameRoleInfo.partyId="1100";//360渠道参数，设置帮派id，必须为整型字符串

		gameRoleInfo.professionId = "11";//360渠道参数，设置角色职业id，必须为整型字符串
		gameRoleInfo.profession = "法师";//360渠道参数，设置角色职业名称
		gameRoleInfo.partyRoleId = "1";//360渠道参数，设置角色在帮派中的id
		gameRoleInfo.partyRoleName = "帮主"; //360渠道参数，设置角色在帮派中的名称
		gameRoleInfo.friendlist = "无";//360渠道参数，设置好友关系列表，格式请参考：http://open.quicksdk.net/help/detail/aid/190


		QuickSDK.getInstance ().createRole(gameRoleInfo);//创建角色
	}
	
	public void onEnterGame(){
		QuickSDK.getInstance().callFunction(FuncType.QUICK_SDK_FUNC_TYPE_REAL_NAME_REGISTER);
		//注：GameRoleInfo的字段，如果游戏有的参数必须传，没有则不用传
		GameRoleInfo gameRoleInfo = new GameRoleInfo();
		
		gameRoleInfo.gameRoleBalance = "0";
		gameRoleInfo.gameRoleID = "11111";
		gameRoleInfo.gameRoleLevel = "1";
		gameRoleInfo.gameRoleName = "钱多多";
		gameRoleInfo.partyName = "同济会";
		gameRoleInfo.serverID = "1";
		gameRoleInfo.serverName = "火星服务器";
		gameRoleInfo.vipLevel = "1";
		gameRoleInfo.roleCreateTime = "roleCreateTime";//UC与1881渠道必传，值为10位数时间戳
		
		gameRoleInfo.gameRoleGender = "男";//360渠道参数
		gameRoleInfo.gameRolePower="38";//360渠道参数，设置角色战力，必须为整型字符串
		gameRoleInfo.partyId="1100";//360渠道参数，设置帮派id，必须为整型字符串
		
		gameRoleInfo.professionId = "11";//360渠道参数，设置角色职业id，必须为整型字符串
		gameRoleInfo.profession = "法师";//360渠道参数，设置角色职业名称
		gameRoleInfo.partyRoleId = "1";//360渠道参数，设置角色在帮派中的id
		gameRoleInfo.partyRoleName = "帮主"; //360渠道参数，设置角色在帮派中的名称
		gameRoleInfo.friendlist = "无";//360渠道参数，设置好友关系列表，格式请参考：http://open.quicksdk.net/help/detail/aid/190

		
		QuickSDK.getInstance ().enterGame (gameRoleInfo);//开始游戏
		Application.LoadLevel("scene4");
	}
	
	public void onUpdateRoleInfo()
	{
		//注：GameRoleInfo的字段，如果游戏有的参数必须传，没有则不用传
		GameRoleInfo gameRoleInfo = new GameRoleInfo();
		
		gameRoleInfo.gameRoleBalance = "0";
		gameRoleInfo.gameRoleID = "11111";
		gameRoleInfo.gameRoleLevel = "1";
		gameRoleInfo.gameRoleName = "钱多多";
		gameRoleInfo.partyName = "同济会";
		gameRoleInfo.serverID = "1";
		gameRoleInfo.serverName = "火星服务器";
		gameRoleInfo.vipLevel = "1";
		gameRoleInfo.roleCreateTime = "roleCreateTime";//UC与1881渠道必传，值为10位数时间戳
		
		gameRoleInfo.gameRoleGender = "男";//360渠道参数
		gameRoleInfo.gameRolePower="38";//360渠道参数，设置角色战力，必须为整型字符串
		gameRoleInfo.partyId="1100";//360渠道参数，设置帮派id，必须为整型字符串
		
		gameRoleInfo.professionId = "11";//360渠道参数，设置角色职业id，必须为整型字符串
		gameRoleInfo.profession = "法师";//360渠道参数，设置角色职业名称
		gameRoleInfo.partyRoleId = "1";//360渠道参数，设置角色在帮派中的id
		gameRoleInfo.partyRoleName = "帮主"; //360渠道参数，设置角色在帮派中的名称
		gameRoleInfo.friendlist = "无";//360渠道参数，设置好友关系列表，格式请参考：http://open.quicksdk.net/help/detail/aid/190
		
		QuickSDK.getInstance ().updateRole(gameRoleInfo);
	}

	public void onNext(){
		Application.LoadLevel ("scene3");
    }

    public void onCallFunctionWithParamTest()
    {
        //QuickSDK.getInstance().callFunctionWithParams(FuncType.QUICK_SDK_FUNC_TYPE_URL, "https://hotfix.public.manasisrefrain.com/Resources/GaCha/8003/8003.html");
    }

	public void onExit(){
		if(QuickSDK.getInstance().isChannelHasExitDialog ()){
			QuickSDK.getInstance().exit();
		}else{
			//游戏调用自身的退出对话框，点击确定后，调用QuickSDK的exit()方法
			mExitDialogCanvas.SetActive(true);
		}
	}
		
	public void onExitCancel(){
		mExitDialogCanvas.SetActive (false);
	}
	public void onExitConfirm(){
		mExitDialogCanvas.SetActive (false);
		QuickSDK.getInstance().exit ();
	}

	public void onShowToolbar()
	{
		QuickSDK.getInstance ().showToolBar (ToolbarPlace.QUICK_SDK_TOOLBAR_BOT_LEFT);
	}

	public void onHideToolbar()
	{
		QuickSDK.getInstance ().hideToolBar ();
	}

	public void onEnterUserCenter()
	{
		QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_ENTER_USER_CENTER);
	}

	public void onEnterBBS()
	{
		QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_ENTER_BBS);
	}
	public void onEnterCustomer()
	{
		QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_ENTER_CUSTOMER_CENTER);
	}
    public void onGetGoodsInfos()
    {
        showLog("onGetGoodsInfos", "onGetGoodsInfos 方法已被调用");
        QuickSDK.getInstance().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_QUERY_GOODS_INFO);
    }
    public void onUserId()
	{
		string uid = QuickSDK.getInstance ().userId();
		showLog("userId", uid);
	}
	public void ongetDeviceId()
	{
		string deviceId = QuickSDK.getInstance().getDeviceId();
		showLog("deviceId", deviceId);
	}
	public void onChannelType()
	{
		int type = QuickSDK.getInstance ().channelType ();
		showLog("channelType", ""+type);
	}
	public void onFuctionSupport(int type)
	{
		bool supported = QuickSDK.getInstance ().isFunctionSupported ((FuncType)type);
		showLog("fuctionSupport", supported?"yes":"no");
	}
	public void onGetConfigValue(string key)
	{
		string value = QuickSDK.getInstance ().getConfigValue (key);
		showLog("onGetConfigValue", key + ": "+value);
	}

	public void onOk()
	{
		messageBox.SetActive (false);
	}

	public void onPauseGame()
	{
		Time.timeScale = 0;
		QuickSDK.getInstance ().callFunction (FuncType.QUICK_SDK_FUNC_TYPE_PAUSED_GAME);
	}

	public void onResumeGame()
	{
		Time.timeScale = 1;
	}
		
	//************************************************************以下是需要实现的回调接口*************************************************************************************************************************
	//callback
	public override void onInitSuccess()
	{
		showLog("onInitSuccess", "");
		//QuickSDK.getInstance ().login (); //如果游戏需要启动时登录，需要在初始化成功之后调用
	}

	public override void onInitFailed(ErrorMsg errMsg)
	{
		showLog("onInitFailed", "msg: " + errMsg.errMsg);
	}

	public override void onLoginSuccess(UserInfo userInfo)
	{
		showLog ("onLoginSuccess", "uid: " + userInfo.uid + " ,username: " + userInfo.userName + " ,userToken: " + userInfo.token + ", msg: " + userInfo.errMsg);
		Application.LoadLevel ("scene2");
        String code = QuickSDK.getInstance().callFunctionWithResult(0);
        Debug.Log("callFunctionWithResult is " + code);

    }

	public override void onSwitchAccountSuccess(UserInfo userInfo){
		//切换账号成功，清除原来的角色信息，使用获取到新的用户信息，回到进入游戏的界面，不需要再次调登录
		showLog ("onLoginSuccess", "uid: " + userInfo.uid + " ,username: " + userInfo.userName + " ,userToken: " + userInfo.token + ", msg: " + userInfo.errMsg);
		Application.LoadLevel ("scene2");
	}

	public override void onLoginFailed (ErrorMsg errMsg)
	{
		showLog("onLoginFailed", "msg: "+ errMsg.errMsg);
	}

	public override void onLogoutSuccess ()
	{
		showLog("onLogoutSuccess", "");
		//注销成功后回到登陆界面
		Application.LoadLevel("scene1");
	}



	public override void onPaySuccess (PayResult payResult)
	{
		showLog("onPaySuccess", "orderId: "+payResult.orderId+", cpOrderId: "+payResult.cpOrderId+" ,extraParam"+payResult.extraParam);
	}

	public override void onPayCancel (PayResult payResult)
	{
		showLog("onPayCancel", "orderId: "+payResult.orderId+", cpOrderId: "+payResult.cpOrderId+" ,extraParam"+payResult.extraParam);
	}

	public override void onPayFailed (PayResult payResult)
	{
		showLog("onPayFailed", "orderId: "+payResult.orderId+", cpOrderId: "+payResult.cpOrderId+" ,extraParam"+payResult.extraParam);
	}

	public override void onExitSuccess ()
	{
		showLog ("onExitSuccess", "");
        //退出成功的回调里面调用  QuickSDK.getInstance ().exitGame ();  即可实现退出游戏，杀进程。为避免与渠道发生冲突，请不要使用  Application.Quit ();
        QuickSDK.getInstance ().exitGame ();
	}

    public override void onSucceed(string infos)
    {
        showLog("onSucceed", infos);
    }

    public override void onFailed(string message)
    {
        showLog("onFailed", "msg: " + message);
    }

    public override void onPrivaceAgree()
    {
        QuickSDKImp.getInstance().init();
    }

    public override void onPrivaceRefuse()
    {
        showLog("onPrivaceRefuse", "onPrivaceRefuse");
    }
}

