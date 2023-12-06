namespace UnifySDK.Event.Crash
{
    [UnifySDKEventData]
    public struct ScriptExceptionConfigData
    {
        /// <summary>
        /// 错误的标题
        /// </summary>
        public string errorTitle;
        
        /// <summary>
        /// 错误堆栈信息
        /// </summary>
        public string stacktrace;
        
        /// <summary>
        /// 语言
        /// </summary>
        public string language;
    }  
    [UnifySDKEventData]
    public struct LoggingData
    {
     
        /// <summary>
        /// 需要获取log行数
        /// </summary>
        public int lines;
        
        /// <summary>
        /// 需要过滤关键字
        /// </summary>
        public string filter;
    }
    
    
}
