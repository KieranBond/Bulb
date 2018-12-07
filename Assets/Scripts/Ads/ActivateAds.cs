using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAds : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
#if UNITY_ANDROID
        string appID = AppAdInfo.m_androidAppID;
#elif UNITY_STANDALONE
        string appID = AppAdInfo.m_androidTestingAppID;
#else
        string appID = AppAdInfo.m_unknown;
#endif

        MobileAds.Initialize(appID);

    }
}
