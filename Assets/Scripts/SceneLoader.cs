using TrickyRocket.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrickyRocket
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_loadingBarContainer = default;
        [SerializeField]
        private Image m_loadingBarFill;
        [SerializeField]

        private AsyncOperation asyncLoadScene;

        /// <summary>
        /// Load scene async and open it after a certain amount of time
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="withProgressBar"></param>
        /// <param name="timeAfterOpen"></param>


        private void Start()
        {
            StartCoroutine(LoadAsynchronously(SceneUtility.SceneName, SceneUtility.WithProgressBar, SceneUtility.TimeBeforeOpen));
        }

        private IEnumerator LoadAsynchronously(string levelName, bool withProgressBar, float timeBeforeOpen)
        {
            if (!withProgressBar)
                m_loadingBarContainer?.SetActive(false);

            float timer = 0f;

            AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                if (m_loadingBarFill && withProgressBar)
                {
                    float progress = Mathf.Clamp01(operation.progress / 0.9f);
                    m_loadingBarFill.fillAmount = progress;
                }

                timer += Time.deltaTime;


                if (timer > timeBeforeOpen)
                {
                    operation.allowSceneActivation = true;
                    SceneUtility.IsCurrentlyLoadingScene = false;
                }
                yield return null;
            }
            yield return null;
        }
    }

}
