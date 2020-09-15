using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

public class AppsFlyerInit : MonoBehaviour, IAppsFlyerConversionData {

#if UNITY_IOS || UNITY_ANDROID

    public string appId = string.Empty;
    public string devKey = string.Empty;

    //******************************//
    private bool isDebug = false;
    private bool getConversionData = false;
    //******************************//
    
    void Start()
    {
        Debug.Log("AppsFlyer_Unity: Start");
        AppsFlyer.setIsDebug(isDebug);
        Debug.Log("AppsFlyer_Unity: Start -> CALL: AppsFlyer.initSDK");
        AppsFlyer.initSDK(devKey, appId, getConversionData ? this : null);
        Debug.Log("AppsFlyer_Unity: Start -> CALL: AppsFlyer.startSDK");
        AppsFlyer.startSDK();
        Debug.Log("AppsFlyer_Unity: Start -> End");
        // AppsFlyer.setAppsFlyerKey ("8MAzUC3B2BHYVi2uYVHaSd");
        // AppsFlyer.setAppID (appId);
        // AppsFlyer.setIsDebug (true);
        // AppsFlyer.getConversionData ();
        // AppsFlyer.trackAppLaunch ();
    }
    public void onConversionDataSuccess(string conversionData)
    {
        AppsFlyer.AFLog("onConversionDataSuccess -> conversionData = ", conversionData);
        Dictionary<string, object> conversionDataDictionary = AppsFlyer.CallbackStringToDictionary(conversionData);
        // add deferred deeplink logic here
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("onConversionDataFail -> error = ", error);
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution -> attributionData = ", attributionData);
        Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
        // add direct deeplink logic here
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure -> error = ", error);
    }
#endif
}

