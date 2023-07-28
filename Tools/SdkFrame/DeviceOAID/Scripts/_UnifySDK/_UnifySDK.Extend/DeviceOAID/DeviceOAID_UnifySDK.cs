using System.Collections;
using System.Collections.Generic;
using UnifySDK.Event;
using UnityEngine;

namespace UnifySDK
{
    #region BaseUnifySDKFactory

    public class DeviceOAID_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override string SDKName
        {
            get => "DeviceOAID";
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<DeviceOAID_UnifySDKConfig>();
            return new DeviceOAID_UnifySDK(sdkConfig, SDKName);
        }
    }

    #endregion
    
    #region BaseUnifySDK
    public partial class DeviceOAID_UnifySDK : BaseUnifySDK<DeviceOAID_UnifySDKConfig>
    {
        public DeviceOAID_UnifySDK(DeviceOAID_UnifySDKConfig config,string sdkName) : base(config,sdkName) {}

        public override void OnInit()
        {
            
        }
    }
    
    #endregion
    
    #region IUnifySDK_Device
    public partial class DeviceOAID_UnifySDK : IUnifySDK_Device
    {
        public void GetOnDevice()
        {
            DeviceIDHelper.inst.GetDeviceOAID(OAID =>
            {
                if (string.IsNullOrEmpty(OAID))
                {
                    UnifySDKEventSystem.Instance.Publish(new DeviceOAIDData{ OAID = null,errorMsg = "OAID is null"});
                }
                else
                {
                    UnifySDKEventSystem.Instance.Publish(new DeviceOAIDData{ OAID = OAID});
                }
            });
        }
        
        [UnifySDKEvent(typeof(AEvent<DeviceOAIDData>))]
        public AEvent<DeviceOAIDData> OnDeviceOAID { get; set; }
    }
    
    #endregion
}
