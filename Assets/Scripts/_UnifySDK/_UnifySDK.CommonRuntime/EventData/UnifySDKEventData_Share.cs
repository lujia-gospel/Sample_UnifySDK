namespace UnifySDK.Event.Share
{
    [UnifySDKEventData]
    public struct SharePhotoData
    {
        public string imageName;
        public string imagePath;
        public string caption;
        public string tag;
    }
    
    [UnifySDKEventData]
    public struct SharePhotoResultData
    {
        public enum CodeType
        {
            Succeed,
            Failed,
            Cancel,
        }
        public CodeType codeType;
    }
    
    [UnifySDKEventData]
    public struct ShareUrlData
    {
        public string url;
        public string quote;
        public string tag;
    }
    [UnifySDKEventData]
    public struct ShareUlrResultData
    {
        public enum CodeType
        {
            Succeed,
            Failed,
            Cancel,
        }
        public CodeType codeType;
    }

}