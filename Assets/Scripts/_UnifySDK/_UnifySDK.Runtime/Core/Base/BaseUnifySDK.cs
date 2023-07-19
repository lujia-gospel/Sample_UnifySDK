using Unity.VisualScripting;

namespace UnifySDK 
{
    public abstract class BaseUnifySDK<T> : IUnifySDK where T : BaseUnifySDKConfig  
    {
        private const int m_DefaultPriority=100;
        //根据优先级初始化
        public int Priority
        {
            get => m_DefaultPriority;
        }

        protected T Config;
        
        protected  BaseUnifySDK(T t)
        {
            Config = t;
        }
        
        public UnifySDKType MyEnum {get;}
        public virtual void OnInit(){}

        /// <summary>
        /// 设置App版本号
        /// </summary>
        /// <param name="youAppVersion">App版本号</param>
        public virtual void SetAppVersion(string youAppVersion){}

        /// <summary>
        /// 设置用户标识
        /// </summary>
        /// <param name="userIdentifier">用户标识</param>
        public virtual void SetUserIdentifier(string userIdentifier){}
        
        /// <summary>
        /// 设置渠道号
        /// </summary>
        /// <param name="channelID">渠道号</param>
        public virtual void SetChannelID(string channelID){}
        
        public virtual void OnDestroy(){}
        
    }
}


