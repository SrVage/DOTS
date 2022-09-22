using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Code.UI
{
    public class AdsRewardController:MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private MoneyController _moneyController;
        private const string AndroidAdUnitId = "4940045";
        private const string IOSAdUnitId = "4940044";
        public const string AdUnitIdAndroid = "Rewarded_Android";
        public const string AdUnitIdIOS = "Rewarded_iOS";
        private string _adUnitId = null;
        
        void Awake()
        {   
#if UNITY_IOS
            Advertisement.Initialize(IOSAdUnitId, true, this);
            _adUnitId = AdUnitIdIOS;
#elif UNITY_ANDROID
            Advertisement.Initialize(AndroidAdUnitId, true, this);
            _adUnitId = AdUnitIdAndroid;
#elif UNITY_EDITOR
            Advertisement.Initialize(AndroidAdUnitId, true, this);
            _adUnitId = AdUnitIdAndroid;
#endif
            _showAdButton.interactable = false;
        }
        
        public void ShowAd()
        {
            _showAdButton.interactable = false;
            Advertisement.Show(_adUnitId, this);
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Ad Loaded: " + placementId);
            if (placementId.Equals(_adUnitId))
            {
                _showAdButton.onClick.AddListener(ShowAd);
                _showAdButton.interactable = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                Advertisement.Load(_adUnitId, this);
                _moneyController.ChangeMoney(100);
            }
        }
        
        private void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void OnInitializationComplete()
        {
            LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Initialization failed: " + _adUnitId);
        }
    }
}