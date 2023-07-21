using UnifySDK.Event;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Account
    {
        public AEvent<UnifySDKEvent.InitSuccessData> OnInitSuccess { get; set; }
    }
}