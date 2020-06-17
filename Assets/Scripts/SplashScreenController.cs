using TrickyRocket.Const;
using TrickyRocket.Manager;
using TrickyRocket.View;
using UnityEngine;
using UnityEngine.Events;

namespace TrickyRocket.Controller
{
    public class SplashScreenController : MonoBehaviour
    {
        [SerializeField] private float m_timeBeforeOpenMenu = 3.0f;
        [SerializeField] private SplashScreenView m_splashScreenView = default;
        private UpdateCheckerController m_updateCheckerController;

        private UnityAction m_updateDoneAction = null;

        void Start()
        {
            SubscribeToUpdateDoneEvent();
            GetUpdateChecker();
            CheckForContentUpdateAndDownload();
        }

        private void OnDisable()
        {
            UnSubscribeToUpdateDoneEvent();
        }

        private void SubscribeToUpdateDoneEvent()
        {
            m_updateDoneAction += OpenMainMenuAsync;
            EventManager.StartListening(EventsName.UpdateDone, m_updateDoneAction);
        }

        private void UnSubscribeToUpdateDoneEvent()
        {
            EventManager.StopListening(EventsName.UpdateDone, m_updateDoneAction);
        }

        private void GetUpdateChecker()
        {
            m_updateCheckerController = GetComponent<UpdateCheckerController>();
            if (!m_updateCheckerController)
            {
                Debug.LogError("No Update Checker Controller found, closing game");
                Application.Quit();
            }
        }

        private void CheckForContentUpdateAndDownload()
        {
            m_updateCheckerController.CheckForContentUpdateAndDownload();
        }

        private void OpenMainMenuAsync()
        {
            SceneUtility.LoadSceneAsync(SceneNames.MainMenu, true, m_timeBeforeOpenMenu);
        }
    }
}

