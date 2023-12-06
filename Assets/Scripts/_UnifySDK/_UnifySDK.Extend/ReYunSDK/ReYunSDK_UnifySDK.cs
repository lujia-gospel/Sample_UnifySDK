using System;
using System.Collections.Generic;
using UnifySDK.Event.Analysis;

namespace UnifySDK
{
    #region BaseUnifySDKFactory

    public class ReYunSDK_UnifySDKFactory : BaseUnifySDKFactory
    {
        protected override UnifySDKType SDKType
        {
            get => UnifySDKType.ReYunSDK;
        }

        public override IUnifySDK Create()
        {
            var sdkConfig = GetSDKConfig<ReYunSDK_UnifySDKConfig>();
            return new ReYunSDK_UnifySDK(sdkConfig, SDKType);
        }
    }

    #endregion

    #region BaseUnifySDK

    public partial class ReYunSDK_UnifySDK : BaseUnifySDK<ReYunSDK_UnifySDKConfig>
    {
        public ReYunSDK_UnifySDK(ReYunSDK_UnifySDKConfig config, UnifySDKType type) : base(config, type)
        {
        }

        public override void OnInit()
        {
            base.OnInit();

            if (UnifySDKManager.Instance.CheckContainSDK(UnifySDKType.QuickSDK))
            {
                UnifySDKManager.Instance.UnifySDKDic[UnifySDKType.QuickSDK].OnInitSuccess.Handler +=
                    (_data, eventArgs) =>
                    {
                        string appKey = Config.Appkey,
                            channelId = Config.ChannelId,
                            caid1 = null,
                            caid2 = null,
                            oaid = null,
                            assetFileName = null,
                            oaidLibraryString = null,
                            startupParams = null,
                            installParams = null;
                        if (UnifySDKManager.Instance.IUnifySDK_Device != null)
                        {
                            UnifySDKManager.Instance.IUnifySDK_Device.GetDeviceInfo((data) =>
                            {
#if UNITY_ANDROID &&!UNITY_EDITOR
                                if (UnifySDKManager.Instance.IUnifySDK_Account != null)
                                    channelId = UnifySDKManager.Instance.IUnifySDK_Account.GetCPSChannelID();
                                oaid = data.OAID;

                                if (channelId == "c")
                                    SetConfig = UnifySDKConfig<ReYunSDK_UnifySDKConfig>.GetConfig()
                                        .GetSettingsForTargetPlatform("Android_C") as ReYunSDK_UnifySDKConfig;
                                appKey = Config.Appkey;
#endif
                                Tracking.Instance.init(appKey, channelId, caid1, caid2,
                                    oaid, assetFileName, oaidLibraryString, Convert(startupParams),
                                    Convert(installParams));
                                UDebug.Logic.LogError($"当前热云appkey:{appKey}");
                            });
                        }
                        else
                        {
                            Tracking.Instance.init(appKey, channelId, caid1, caid2,
                                oaid, assetFileName, oaidLibraryString, Convert(startupParams),
                                Convert(installParams));
                        }
                    };
            }
        }

        public override string GetDeviceId()
        {
            return Tracking.Instance.getDeviceId();
        }
    }

    #endregion

    #region IUnifySDK_Analysis

    public partial class ReYunSDK_UnifySDK : IUnifySDK_Analysis
    {
        private Dictionary<string, object> customParam;

        public void Register(RegisterData data)
        {
            Tracking.Instance.register(data.account, Convert(data.customParam));
        }

        public void Login(LoginData data)
        {
            Tracking.Instance.login(data.account, data.serverId, Convert(data.customParam));
        }

        public void Recharge(RechargeData data)
        {
            Tracking.Instance.setryzf(data.orderID, data.paymentType, data.currencyType, data.currencyAmount,
                Convert(data.customParam));
        }

        public void Order(OrderData data)
        {
            Tracking.Instance.setDD(data.orderID, data.currencyType, data.currencyAmount, customParam);
        }

        public void SetCustomEvent(CustomEventData data)
        {
            Tracking.Instance.setEvent(data.eventName, Convert(data.customParam));
        }

        public void SetPrintLog(bool print)
        {
            Tracking.Instance.setPrintLog(print);
        }

        public void SetTrackViewDuration(TrackViewDurationData data)
        {
            throw new NotImplementedException();
        }

        public void SetTrackAdShow(TrackAdShowData data)
        {
            Tracking.Instance.setTrackAdShow(data.adPlatform, data.adId, data.playSuccess, Convert(data.customParam));
        }

        public void SetTrackAdClick(TrackAdClickData data)
        {
            Tracking.Instance.setTrackAdClick(data.adPlatform, data.adId, Convert(data.customParam));
        }

        public void SetTrackAppDuration(TrackAppDurationData data)
        {
            Tracking.Instance.setTrackAppDuration(data.duration, Convert(data.customParam));
        }

        public string getAttribution()
        {
            return "";
        }

        public Dictionary<string, object> Convert(string selfParam)
        {
            if (String.IsNullOrEmpty(selfParam))
            {
                return null;
            }

            if (customParam == null)
            {
                customParam = new Dictionary<string, object>();
            }

            customParam["selfParam"] = selfParam;
            return customParam;
        }
    }

    #endregion
}