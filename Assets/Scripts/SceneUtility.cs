using ProjectBoost.Const;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectBoost
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
