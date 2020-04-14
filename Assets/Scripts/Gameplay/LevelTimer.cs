using UnityEngine;
using TMPro;
using UnityEngine.Events;
using ProjectBoost.Manager;
using ProjectBoost.Const;

namespace ProjectBoost.Gameplay
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_timerText = default;

        private float m_timer;

        private UnityAction<object> m_levelRunningAction = null;
        private UnityAction m_playerDieAction = null;
        private bool m_isLevelRunning = false;
        private bool m_asSentForSave = false;

        void Start()
        {
            m_timer = 0.0f;
            SetTime();

            m_levelRunningAction += SetLevelIsRunning;
            EventManager.StartListening(EventsName.StartPlaying, m_levelRunningAction);
            EventManager.StartListening(EventsName.StopPlaying, m_levelRunningAction);

            m_playerDieAction += StopTimer;
            EventManager.StartListening(EventsName.PlayerDie, m_playerDieAction);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventsName.StartPlaying, m_levelRunningAction);
            EventManager.StopListening(EventsName.StopPlaying, m_levelRunningAction);
            EventManager.StopListening(EventsName.PlayerDie, m_playerDieAction);
        }

        void Update()
        {
            if (m_isLevelRunning)
            {
                m_timer += Time.deltaTime;
                SetTime();
            }
            else if (m_timer > 0 && !m_asSentForSave) //New Level Time is not save
            {
                SendTime();
            }
        }

        private void SetLevelIsRunning(object isLevelRunning)
        {
            m_isLevelRunning = (bool)isLevelRunning;
        }

        private void StopTimer()
        {
            m_isLevelRunning = false;
            m_asSentForSave = true;// Avoid sending time
        }

        private void SetTime()
        {
            m_timerText?.SetText(FormatTime.FormatLevelTime(m_timer));
        }

        private void SendTime()
        {
            EventManager.TriggerEvent(EventsName.SendScoreTime, m_timer);
            m_asSentForSave = true;
        }
    }
}