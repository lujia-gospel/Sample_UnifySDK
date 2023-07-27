using Tutorial;
using UnifySDK.Event;

namespace UnifySDK 
{
    public abstract class BaseUnifySDK<T> : IUnifySDK where T : BaseUnifySDKConfig  
    {
        //默认SDK根据优先级来获取  初始化也是根据优先级来
        public virtual int Priority { get=>0; }
        
        public virtual bool AutoInit  { get=>true; }
        
        public string SDKName { get;set; }

        protected T Config;
        
        protected BaseUnifySDK(T t,string sdkName)
        {
            SDKName = sdkName;
            Config = t;
            UnifySDKEventSystem.Instance.UnifySDKInitEvent(this);
        }

        public abstract void OnInit();

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

        /// <summary>
        /// 获取渠道号
        /// </summary>
        public virtual string GetChannelID()
        {
            return "null";
        }
        
        /// <summary>
        /// 获取渠道名
        /// </summary>
        public virtual string GetChannelName()
        {
            return "null";
        }


        /// <summary>
        /// 获取设备ID
        /// </summary>
        public virtual string GetDeviceId()
        {
            return "该SDK没有实现GetDeviceId方法";
        }

        public virtual void OnDestroy(){}
        
    }
}


