namespace UnifySDK.Event.Account
{
    [UnifySDKEventData("登录成功")]
    public struct LoginSuccessData
    {
        public string uid;
        public string userName;
        public string token;
        public string channelId;
        public string channelName;
        public string errMsg;
    }

    [UnifySDKEventData("切换账号失败")]
    public struct SwitchAccountSuccessData
    {
        public string uid;
        public string userName;
        public string token;
        public string errMsg;
    }

    [UnifySDKEventData("切换账号成功")]
    public struct SwitchAccountFaileData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("切换账号取消")]
    public struct SwitchAccountCancelData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("登录失败")]
    public struct LoginFailedData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("登录取消")]
    public struct LoginCancelData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("账号退出成功")]
    public struct LogoutSuccessData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("账号退出失败")]
    public struct LogoutFailedData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("游戏退出成功")]
    public struct ExitSuccessData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("游戏退出失败")]
    public struct ExitFailedData
    {
        /// <summary>
        /// 报错信息
        /// </summary>
        public string errMsg;
    }

    [UnifySDKEventData("隐私协议")]
    public struct PrivacyAgreeData
    {
    }

    [UnifySDKEventData("隐私拒绝")]
    public struct PrivacyRefuseData
    {
    }
}