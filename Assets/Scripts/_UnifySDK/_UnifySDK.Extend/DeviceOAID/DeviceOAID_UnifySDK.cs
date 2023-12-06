using System;
using UnifySDK.Event;
using UnifySDK.Event.Device;
// using UniLang;

namespace UnifySDK
{
    #region BaseUnifySDKFactory

    public class DeviceOAID_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override UnifySDKType SDKType
        {
            get => UnifySDKType.DeviceOAID;
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<DeviceOAID_UnifySDKConfig>();
            return new DeviceOAID_UnifySDK(sdkConfig, SDKType);
        }
    }

    #endregion
    
    #region BaseUnifySDK
    public partial class DeviceOAID_UnifySDK : BaseUnifySDK<DeviceOAID_UnifySDKConfig>
    {
        public DeviceOAID_UnifySDK(DeviceOAID_UnifySDKConfig config,UnifySDKType type) : base(config,type) {}

        public override void OnInit()
        {
            base.OnInit();
        }
    }
    
    #endregion
    
    #region IUnifySDK_Device
    public partial class DeviceOAID_UnifySDK : IUnifySDK_Device
    {
        public void GetDeviceInfo(Action<DeviceInfoData> callBack)
        {
            DeviceIDHelper.inst.GetDeviceOAID(oaid =>
            {
                
                if (string.IsNullOrEmpty(oaid))
                {
                    callBack(new DeviceInfoData() { OAID = null,errorMsg = "OAID is null"});
                }
                else
                {
                    callBack(new DeviceInfoData() { OAID = oaid});
                }
            });

        }

        public void Translate(TranslateData data)
        {
            // Translator.Do("auto", data.targetLand, data.textInput, data.callBack);
        }
    }
    
    #endregion
}
