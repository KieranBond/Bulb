using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interstitial Ads are fullscreen ads, usually images or videos.
public class RequestInterstitialAd : MonoBehaviour
{
    public InterstitialAd m_ad { get; private set; }

    private void Start()
    {
        LoadAd();
    }

    public InterstitialAd LoadAd()
    {
#if UNITY_ANDROID
        //string adUnityID = AppAdInfo.m_androidInterstitialID;
        string adUnityID = AppAdInfo.m_androidTestingInterstitialID;
#elif UNITY_EDITOR || UNITY_STANDALONE
        string adUnityID = AppAdInfo.m_androidTestingInterstitialID;//Testing ID from Google
#else
        string adUnityID = AppAdInfo.m_unknown;
#endif
        //Create the interstitial
        m_ad = new InterstitialAd(adUnityID);

        //Create the request
        AdRequest request = new AdRequest.Builder().Build();

        //Load the request into the interstitial
        m_ad.LoadAd(request);

        while (!m_ad.IsLoaded())
        {

        }

        return m_ad;
    }

    public InterstitialAd RequestInterstitial()
    {
        if (!m_ad.IsLoaded())
        {
            //Loads a new ad
            AdRequest request = new AdRequest.Builder().Build();
            m_ad.LoadAd(request);
            while (!m_ad.IsLoaded())
            {

            }
        }

        return m_ad;
    }

    
}
