using UnityEngine;
using TMPro;
using UnityEngine.Events;
using ProjectBoost.UIExtension;
using ProjectBoost.Const;
using ProjectBoost.Manager;

namespace ProjectBoost.View
{
    public class EndLevelView : MonoBehaviour
    {
        [SerializeField] private PopInAndOutUI m_pop = default;
        [SerializeField] private GameObject m_container = default;
        [SerializeField] private TextMeshProUGUI m_yourTimeText = default;
        [SerializeField] private TextMeshProUGUI m_currentRecord = default;
        [SerializeField] private GameObject m_newRecordContainer = default;

        private UnityAction m_levelCompleteAction = null;
        private UnityAction m_playerDieAction = null;

        private void Start()
        {
            m_levelCompleteAction += InitializeLevelComplete;
            EventManager.StartListening(EventsName.OpenEndLevelView, m_levelCompleteAction);

            m_playerDieAction += InitializePlayerDie;
            EventManager.StartListening(EventsName.PlayerDie, m_playerDieAction);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventsName.OpenEndLevelView, m_levelCompleteAction);
            EventManager.StopListening(EventsName.PlayerDie, m_playerDieAction);
        }

        private void InitializeLevelComplete()
        {
            string record = string.Empty;
            if (LevelManager.IsNewRecord())
                record = LevelManager.GetCurrentLevelTimeAsString();
            else
                record = LevelManager.GetCurrentLevelRecordAsString();

            m_currentRecord?.SetText(FormatTime.FormatEndLevelRecord(record));

            m_yourTimeText?.SetText(FormatTime.FormatEndLevelTime(LevelManager.GetCurrentLevelTimeAsString()));
            m_newRecordContainer?.SetActive(LevelManager.IsNewRecord());

            m_container?.SetActive(true);
            m_pop.PlayPopIn();
        }

        private void InitializePlayerDie()
        {
            m_currentRecord?.SetText(FormatTime.FormatEndLevelRecord(LevelManager.GetCurrentLevelRecordAsString()));

            m_yourTimeText?.SetText("FAIL");
            m_newRecordContainer?.SetActive(false);

            m_container?.SetActive(true);
            m_pop.PlayPopIn();
        }

        public void BackToMenu()
        {
            SceneUtility.LoadSceneAsync(SceneNames.MainMenu);
        }

        public void ReloadCurrentLevel()
        {
            LevelManager.ReloadCurrentLevel();
        }

        public void OpenNextLevel()
        {
            LevelManager.LoadNextLevel();
        }
    }

}
