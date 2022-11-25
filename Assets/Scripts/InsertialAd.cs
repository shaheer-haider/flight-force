// using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class InsertialAd : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";

    // skip add this time using boolean
    public static bool isAddRunning = false;
    string _adUnitId;

    [SerializeField] string _gameId;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = false;
    bool isAddLoaded = false;
    public static InsertialAd instance;
    void Awake()
    {
        if (Advertisement.isInitialized)
        {
            isAddLoaded = PlayerPrefs.GetInt("isAddLoaded", 0) == 1;
        }
        else
        {
            isAddLoaded = false;
        }
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
        // Advertisement.AddListener(this);
        if (!Advertisement.isInitialized)
        {
            InitializeAds();
        }
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        if (!isAddLoaded)
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        int adsCounter = PlayerPrefs.GetInt("Ads", 0);
        if (adsCounter < 2)
        {
            PlayerPrefs.SetInt("Ads", adsCounter + 1);
            return;
        }
        PlayerPrefs.SetInt("Ads", 0);
        isAddRunning = true;
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        ShowAd();
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Advertisement.Show(_adUnitId, this);
        print("Showing Ad: " + _adUnitId);
    }

    // Implement Load Listener Listener interface methods: 
    // IUnityAdsLoadListener start
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        isAddLoaded = true;
        print("Ad loaded: " + adUnitId);
        PlayerPrefs.SetInt("isAddLoaded", 1);
        // ShowAd();
    }
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        PlayerPrefs.SetInt("isAddLoaded", 0);
        isAddLoaded = false;
        isAddRunning = false;
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
    }

    // IUnityAdsLoadListener end


    // Implement Show Listener interface methods: 
    // IUnityAdsShowListener start
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        isAddRunning = false;
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        PlayerPrefs.SetInt("isAddLoaded", 0);
        isAddLoaded = false;
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        print("OnUnityAdsShowComplete");
        isAddRunning = false;
        PlayerPrefs.SetInt("isAddLoaded", 0);
        isAddLoaded = false;
    }

    // intialize ads
    public void InitializeAds()
    {
        print("Initializing ads");
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }


    // Implement Initialization Listener interface methods:
    // IUnityAdsInitializationListener start
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load(_adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
