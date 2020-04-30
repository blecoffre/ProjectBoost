using EasyMobile;
using UnityEngine;
using TMPro;

namespace TrickyRocket.Test
{
    public class AdsExample : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_interstitialStatus = default;
        void Awake()
        {
            Debug.Log("Init AdsExample");
            if (!RuntimeManager.IsInitialized())
                RuntimeManager.Init();
        }

        private void Start()
        {
            Debug.Log("Start");
            Advertising.LoadInterstitialAd();
        }

        private void Update()
        {
            Debug.Log("Update Loop");
            if (Advertising.IsInterstitialAdReady())
            {
                Debug.Log("Advertising is ready");
                m_interstitialStatus?.SetText("Interstitial Ad ready");
                m_interstitialStatus.color = Color.green;
            }
            else
            {
                Debug.Log("Advertising is not ready");
                m_interstitialStatus.SetText("Interstital Ad not ready");
                m_interstitialStatus.color = Color.red;
            }
        }

        public void ShowInterstitialAd()
        {
            if (Advertising.IsInterstitialAdReady())
                Advertising.ShowInterstitialAd();
        }
    }
}

