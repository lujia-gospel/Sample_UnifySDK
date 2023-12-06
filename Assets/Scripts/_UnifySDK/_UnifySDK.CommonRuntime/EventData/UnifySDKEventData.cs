namespace UnifySDK
{
    [UnifySDKEventData("初始化")]
    public struct InitSDKData
    {
        public UnifySDKType[] InitSDKs;

        public InitSDKData(params UnifySDKType[] sdks)
        {
            InitSDKs = sdks;
        }
    } 

    [UnifySDKEventData("初始化成功")]
    public struct InitSuccessData {  }

    [UnifySDKEventData("初始化失败")]
    public struct InitFailedData
    {
        public string errMsg;
    }
}