namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK
    {
        int Priority { get ; }//根据优先级初始化

        string SDKName{ get ; set; }

        bool AutoInit { get; }

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
        
        /// <summary>
        /// 获取渠道ID
        /// </summary>
        string GetChannelID();
        
        /// <summary>
        /// 获取渠道名
        /// </summary>
        string GetChannelName();

        /// <summary>
        /// 获取设备ID
        /// </summary>
        string GetDeviceId();
   
        
        void OnDestroy();
    }
}

