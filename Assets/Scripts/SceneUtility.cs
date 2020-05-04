using TrickyRocket.Const;
using TrickyRocket.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrickyRocket
{
    class SceneUtility
    {
        public static string SceneName;
        public static bool WithProgressBar;
        public static float TimeBeforeOpen;

        public static bool IsCurrentlyLoadingScene = false;

        public static void LoadSceneAsync(string sceneName, bool withProgressBar = false, float timeBeforeOpen = 1.0f)
        {
            SceneName = sceneName;
            WithProgressBar = withProgressBar;
            TimeBeforeOpen = timeBeforeOpen;

            if (!InAppPurchasingManager.Instance.IsNoMoreAdsPurchased())
            {
                if (sceneName.ToLower().Contains("level")) //If loaded scene is a level, increment count before next ad
                    AdsManager.Instance.IncrementCountBeforeNextAds();

                if (AdsManager.Instance.IsTimeForInterstitialAd())
                    AdsManager.Instance.ShowInterstitialAd();
            }

            if (!IsCurrentlyLoadingScene)
                SceneManager.LoadScene(SceneNames.SceneLoader, LoadSceneMode.Additive);
        }

        public static void LoadScene(string sceneName)
        {
            if (!IsCurrentlyLoadingScene)
                SceneManager.LoadScene(sceneName);
        }
    }
}
