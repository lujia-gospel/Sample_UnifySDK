using com.adjust.sdk;
using UnifySDK;
using UnifySDK.Event.Analysis;
using UnityEngine;

public partial class AdjustSDK_UnifySDK :IUnifySDK_Analysis
{
    private bool isEnabled;
    private  string AppToken = "2zzymr5gkigw";
    private bool isInitAdjustSdk = false;

    public void Register(RegisterData data)
    {
        InitSDK();
    }

    public void Login(LoginData data)
    {
         
    }

    public void Recharge(RechargeData data)
    {
        trackEventRevenue(data.eventName,data.currencyAmount,data.orderID);
    }

    public void Order(OrderData data)
    {
        trackEventRevenue(data.eventName,data.currencyAmount,data.orderID);
    }

    public void SetCustomEvent(CustomEventData data)
    {
        trackEvent(data.eventName);
    }

    public void SetPrintLog(bool print)
    {
        Adjust.setEnabled(print);
    }

    public string getAttribution()
    {
        if (isInitAdjustSdk == false)
        {
            return "SDKNotInit";
        }
        AdjustAttribution attribution = Adjust.getAttribution();
        if (attribution == null)
            return "SDKNotAvailable";
        Debug.Log("getAttribution" + attribution.ToString()); 
        return attribution.trackerName;
    }
    public void SetTrackViewDuration(TrackViewDurationData data)
    {
        Debug.Log("AdjustSetTrackViewDuration---------" + data.pageID);
    }

    public void SetTrackAdShow(TrackAdShowData data)
    {
        Debug.Log("AdjustSetTrackAdShow---------" + data.adId);
    }

    public void SetTrackAdClick(TrackAdClickData data)
    {
        Debug.Log("AdjustSetTrackAdClick---------" + data.adId);
    }

    public void SetTrackAppDuration(TrackAppDurationData data)
    {
        Debug.Log("AdjustSetTrackAppDuration---------" + data.duration);
    }
    public void InitSDK()
    { 
        //沙盒配置
        Debug.Log("Adjust---------" + AppToken);
        AdjustConfig adjustConfig = new AdjustConfig(AppToken, AdjustEnvironment.Production);
        adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
        adjustConfig.setNeedsCost(true);
        adjustConfig.setLogDelegate(msg => Debug.Log(msg));
        adjustConfig.setEventSuccessDelegate(EventSuccessCallback);
        adjustConfig.setEventFailureDelegate(EventFailureCallback);
        adjustConfig.setSessionSuccessDelegate(SessionSuccessCallback);
        adjustConfig.setSessionFailureDelegate(SessionFailureCallback);
        adjustConfig.setDeferredDeeplinkDelegate(DeferredDeeplinkCallback);
        adjustConfig.setAttributionChangedDelegate(AttributionChangedCallback);
        Adjust.start(adjustConfig); 
        isEnabled = true;
        Adjust.setEnabled(isEnabled);
        isInitAdjustSdk = true;
    }


    public void trackEvent(string EventName)
    {
        Debug.Log("AdjustTrackEvent" + EventName);
        AdjustEvent adjustEvent = new AdjustEvent(EventName);
        Adjust.trackEvent(adjustEvent);
    }
 

    public string getAttributionAll()
    {
        var attribution = Adjust.getAttribution();
        Debug.Log("getAttribution" + attribution.ToString());
        var AllAttr = "adid=" + attribution.adid +
                      ",network=" + attribution.network +
                      ",adgroup=" + attribution.adgroup +
                      ",campaign=" + attribution.campaign +
                      ",creative=" + attribution.creative +
                      ",clickLabel=" + attribution.clickLabel +
                      ",trackerName=" + attribution.trackerName +
                      ",trackerToken=" + attribution.trackerToken +
                      ",costType=" + attribution.costType +
                      ",costAmount=" + attribution.costAmount.ToString() +
                      ",costCurrency=" + attribution.costCurrency +
                      ",fbInstallReferrer=" + attribution.fbInstallReferrer;
        return AllAttr;
    }
    public void trackEventRevenue(string EventName, float venue,string orderId)
    {
        Debug.Log("AdjustTrackEventRevenue" + EventName + venue + orderId);
        AdjustEvent adjustEvent = new AdjustEvent(EventName);
        adjustEvent.setRevenue(venue, "USD");
        adjustEvent.setTransactionId(orderId);
        Adjust.trackEvent(adjustEvent);
    }

    public void TrackCallbackEvent(string EventName, float venue)
    {
        AdjustEvent adjustEvent = new AdjustEvent(EventName);
        adjustEvent.addCallbackParameter("key", "value");
        adjustEvent.addCallbackParameter("foo", "bar");
        Adjust.trackEvent(adjustEvent);
    }

    public void TrackPartnerEvent(string EventName, float venue)
    {
        AdjustEvent adjustEvent = new AdjustEvent(EventName);
        adjustEvent.addPartnerParameter("key", "value");
        adjustEvent.addPartnerParameter("foo", "bar");
        Adjust.trackEvent(adjustEvent);
    }

    public void setOfflineMode(bool isOffLine)
    {
        Adjust.setOfflineMode(isOffLine);
    }

    public void setEnabled(bool isEnabled)
    {
        Adjust.setEnabled(isEnabled);
    }

    public bool isAdjustEnabled()
    {
        return isEnabled = Adjust.isEnabled();
    }


    public void AttributionChangedCallback(AdjustAttribution attributionData)
    {
        Debug.Log("Attribution changed!");

        if (attributionData.trackerName != null)
        {
            Debug.Log("Tracker name: " + attributionData.trackerName);
        }

        if (attributionData.trackerToken != null)
        {
            Debug.Log("Tracker token: " + attributionData.trackerToken);
        }

        if (attributionData.network != null)
        {
            Debug.Log("Network: " + attributionData.network);
        }

        if (attributionData.campaign != null)
        {
            Debug.Log("Campaign: " + attributionData.campaign);
        }

        if (attributionData.adgroup != null)
        {
            Debug.Log("Adgroup: " + attributionData.adgroup);
        }

        if (attributionData.creative != null)
        {
            Debug.Log("Creative: " + attributionData.creative);
        }

        if (attributionData.clickLabel != null)
        {
            Debug.Log("Click label: " + attributionData.clickLabel);
        }

        if (attributionData.adid != null)
        {
            Debug.Log("ADID: " + attributionData.adid);
        }
    }

    public void EventSuccessCallback(AdjustEventSuccess eventSuccessData)
    {
        Debug.Log("Event tracked successfully!");
        // AdjustSdkManager.Get().OnTrackEvent_lua.Call(AdjustSdkManager.Get().AdjustCallBack_lua, "title", "body");
        if (eventSuccessData.Message != null)
        {
            Debug.Log("Message: " + eventSuccessData.Message);
        }

        if (eventSuccessData.Timestamp != null)
        {
            Debug.Log("Timestamp: " + eventSuccessData.Timestamp);
        }

        if (eventSuccessData.Adid != null)
        {
            Debug.Log("Adid: " + eventSuccessData.Adid);
        }

        if (eventSuccessData.EventToken != null)
        {
            Debug.Log("EventToken: " + eventSuccessData.EventToken);
        }

        if (eventSuccessData.CallbackId != null)
        {
            Debug.Log("CallbackId: " + eventSuccessData.CallbackId);
        }

        if (eventSuccessData.JsonResponse != null)
        {
            Debug.Log("JsonResponse: " + eventSuccessData.GetJsonResponse());
        }
    }

    public void EventFailureCallback(AdjustEventFailure eventFailureData)
    {
        Debug.Log("Event tracking failed!");

        if (eventFailureData.Message != null)
        {
            Debug.Log("Message: " + eventFailureData.Message);
        }

        if (eventFailureData.Timestamp != null)
        {
            Debug.Log("Timestamp: " + eventFailureData.Timestamp);
        }

        if (eventFailureData.Adid != null)
        {
            Debug.Log("Adid: " + eventFailureData.Adid);
        }

        if (eventFailureData.EventToken != null)
        {
            Debug.Log("EventToken: " + eventFailureData.EventToken);
        }

        if (eventFailureData.CallbackId != null)
        {
            Debug.Log("CallbackId: " + eventFailureData.CallbackId);
        }

        if (eventFailureData.JsonResponse != null)
        {
            Debug.Log("JsonResponse: " + eventFailureData.GetJsonResponse());
        }

        Debug.Log("WillRetry: " + eventFailureData.WillRetry.ToString());
    }

    public void SessionSuccessCallback(AdjustSessionSuccess sessionSuccessData)
    {
        Debug.Log("Session tracked successfully!");

        if (sessionSuccessData.Message != null)
        {
            Debug.Log("Message: " + sessionSuccessData.Message);
        }

        if (sessionSuccessData.Timestamp != null)
        {
            Debug.Log("Timestamp: " + sessionSuccessData.Timestamp);
        }

        if (sessionSuccessData.Adid != null)
        {
            Debug.Log("Adid: " + sessionSuccessData.Adid);
        }

        if (sessionSuccessData.JsonResponse != null)
        {
            Debug.Log("JsonResponse: " + sessionSuccessData.GetJsonResponse());
        }
    }

    public void SessionFailureCallback(AdjustSessionFailure sessionFailureData)
    {
        Debug.Log("Session tracking failed!");

        if (sessionFailureData.Message != null)
        {
            Debug.Log("Message: " + sessionFailureData.Message);
        }

        if (sessionFailureData.Timestamp != null)
        {
            Debug.Log("Timestamp: " + sessionFailureData.Timestamp);
        }

        if (sessionFailureData.Adid != null)
        {
            Debug.Log("Adid: " + sessionFailureData.Adid);
        }

        if (sessionFailureData.JsonResponse != null)
        {
            Debug.Log("JsonResponse: " + sessionFailureData.GetJsonResponse());
        }

        Debug.Log("WillRetry: " + sessionFailureData.WillRetry.ToString());
    }

    private void DeferredDeeplinkCallback(string deeplinkURL)
    {
        Debug.Log("Deferred deeplink reported!");

        if (deeplinkURL != null)
        {
            Debug.Log("Deeplink URL: " + deeplinkURL);
        }
        else
        {
            Debug.Log("Deeplink URL is null!");
        }
    }


}