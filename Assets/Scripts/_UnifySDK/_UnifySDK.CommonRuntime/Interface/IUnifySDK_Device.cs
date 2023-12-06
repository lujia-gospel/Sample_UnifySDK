using System;
using UnifySDK.Event;
using UnifySDK.Event.Device;
namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Device
    {
        void GetDeviceInfo(Action<DeviceInfoData> callBack);

        void Translate(TranslateData data);
    }
}