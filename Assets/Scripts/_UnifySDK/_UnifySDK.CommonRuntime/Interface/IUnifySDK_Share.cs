using UnifySDK.Event;
using UnifySDK.Event.Share;
namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Share
    {
        void SharePhoto(SharePhotoData data);

        void ShareUrl(ShareUrlData data);

        void CaptureScreenshotSharePhoto();
        #region Listener
        
        [UnifySDKEvent(typeof(AEvent<SharePhotoResultData>))]
        AEvent<SharePhotoResultData> OnSharePhotoComplete { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<ShareUlrResultData>))]
        AEvent<ShareUlrResultData> OnShareUlrComplete { get; set; }
        #endregion
    }
}