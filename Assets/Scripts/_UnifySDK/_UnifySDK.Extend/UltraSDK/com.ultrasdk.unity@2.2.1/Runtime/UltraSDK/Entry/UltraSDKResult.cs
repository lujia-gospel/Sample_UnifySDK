namespace com.ultrasdk.unity.Entry
{
    public enum UltraSDKResult
    {
        UltraSDKResultSuccess = 0, //成功
        UltraSDKResultFailed = -1, //失败
        UltraSDKResultCancel = -2 , //取消
    }

    public enum UltraSDKLoginInvalidResult
    {
        UltraSDKLoginInvalidKick = 0, //未成年人被踢下线
        UltraSDKLoginInvalidServer = -1, //登录失效,
    }
}