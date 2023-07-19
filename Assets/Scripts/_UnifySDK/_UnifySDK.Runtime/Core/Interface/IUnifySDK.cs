namespace UnifySDK
{
    public enum UnifySDKType
    {
        Analytics,
        Crash,
    }

    public interface  IUnifySDK
    {
        int Priority { get ; }//根据优先级初始化
        
        UnifySDKType MyEnum {get;}
        
        
        void OnInit();
        
        /// <summary>
        /// 设置App版本号
        /// </summary>
        /// <param name="youAppVersion">App版本号</param>
        void SetAppVersion(string youAppVersion);
        
        /// <summary>
        /// 设置用户标识
        /// </summary>
        /// <param name="userIdentifier">用户标识</param>
        void SetUserIdentifier(string userIdentifier);
        
        /// <summary>
        /// 设置渠道号
        /// </summary>
        /// <param name="channelID">渠道号</param>
        void SetChannelID(string channelID);
        
        void OnDestroy();
    }
}

