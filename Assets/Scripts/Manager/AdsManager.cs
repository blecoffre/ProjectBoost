using UnityEngine;
using EasyMobile;
using System.Collections;

namespace TrickyRocket.Manager
{
    public class AdsManager : MonoBehaviour
    {
        public static AdsManager Instance = null;

        private int m_countBeforeNextAds = 0;

        [SerializeField]
        private int m_howManyCountBeforeShowNewAd = 5;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitAds();
        }

        private void InitAds()
        {
            //Load an ad in advance to allow ample time for the ad to be loaded
            Advertising.LoadInterstitialAd();

            Advertising.InterstitialAdCompleted += InterstitialAdCompletedHandler;
        }

        private void OnDisable()
        {
            Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
        }

        public void IncrementCountBeforeNextAds()
        {
            m_countBeforeNextAds += 1;
            Debug.Log("Count before next ad : " + m_countBeforeNextAds);
        }

        public void ShowInterstitialAd()
        {
            StartCoroutine(TryShowInterstitialAd());
        }

        private IEnumerator TryShowInterstitialAd()
        {
            while (!Advertising.IsInterstitialAdReady())
            {
                yield return new WaitForEndOfFrame();
            }

            Advertising.ShowInterstitialAd();
        }

        public void ResetCount()
        {
            m_countBeforeNextAds = 0;
        }

        public bool IsTimeForInterstitialAd()
        {
            Debug.Log(m_countBeforeNextAds == m_howManyCountBeforeShowNewAd);
            return (m_countBeforeNextAds >= m_howManyCountBeforeShowNewAd);
        }

        private void InterstitialAdCompletedHandler(InterstitialAdNetwork network, AdPlacement location)
        {
            ResetCount();
            Advertising.LoadInterstitialAd();
        }
    }
}

