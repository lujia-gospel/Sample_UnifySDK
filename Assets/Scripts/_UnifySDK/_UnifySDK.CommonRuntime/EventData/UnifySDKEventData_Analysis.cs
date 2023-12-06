using System.Collections.Generic;

namespace UnifySDK.Event.Analysis
{
    [UnifySDKEventData]
    public struct RegisterData
    {
        public string account;
        public string customParam;
    }
    
    [UnifySDKEventData]
    public struct LoginData
    {
        public string account;
        public string serverId;
        public string customParam;
    } 
    
    [UnifySDKEventData]
    public struct RechargeData
    {
        public string eventName;
        public string orderID;
        public string paymentType;
        public string currencyType;
        public float currencyAmount;
        public string customParam;
    }
    [UnifySDKEventData]
    public struct OrderData
    {
        public string eventName;
        public string orderID;
        public string currencyType;
        public float currencyAmount;
    } 
    [UnifySDKEventData]
    public struct CustomEventData
    {
        public string eventName;
        public string customParam;
    }
    
    [UnifySDKEventData]
    public struct TrackViewDurationData
    {
        public string pageID;
        public long duration;
        public string customParam;
    }
    
    [UnifySDKEventData]
    public struct TrackAdShowData
    {
        public string adPlatform;
        public string adId;
        public bool playSuccess;
        public string customParam;
    }  
    
    [UnifySDKEventData]
    public struct TrackAdClickData
    {
        public string adPlatform;
        public string adId;
        public string customParam;
    } 
    
    [UnifySDKEventData]
    public struct TrackAppDurationData
    {
        public long duration;
        public string customParam;
    }
}