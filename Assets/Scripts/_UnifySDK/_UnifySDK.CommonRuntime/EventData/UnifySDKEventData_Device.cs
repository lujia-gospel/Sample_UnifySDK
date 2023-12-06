using System;

namespace UnifySDK.Event.Device
{
    [UnifySDKEventData]
    public struct DeviceInfoData
    {
        public string OAID;
        public string VAID;
        public string IMEI;
        public string SSAID;//Android ID(SSAID) 是 Android 设备里不依赖于硬件的一种「半永久标识符」，在系统生命周期内不会改变，但系统重置或刷机后会发生变化，其作用域为一组有关联的应用。
        public string UDID;//Device ID 是一种统称，与硬件相关的 ID 都可以称之为 Device ID，一般是一种不可重置的永久标识符，作用域为设备。
        public string UUID;
        public string GUID;
        public string AAID;//AAID 与 IDFA 作用相同——IDFA 是 iOS 平台内的广告跟踪 ID，AAID 则用于 Android 平台。
        public string IDFA;//AAID 与 IDFA 作用相同——IDFA 是 iOS 平台内的广告跟踪 ID，AAID 则用于 Android 平台。
        
        public string errorMsg;
    }
    
    [UnifySDKEventData]
    public struct TranslateData
    {
        public string targetLand;
        public string textInput;
        public Action<string> callBack;
    }
}