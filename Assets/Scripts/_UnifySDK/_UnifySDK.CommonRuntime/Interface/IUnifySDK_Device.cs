using UnifySDK.Event;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Device
    {
        void GetOnDevice();
        
        public AEvent<DeviceOAIDData> OnDeviceOAID { get; set; }
    }
}