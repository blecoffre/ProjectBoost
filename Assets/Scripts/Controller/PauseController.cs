using ProjectBoost.Const;
using ProjectBoost.Manager;
using UnityEngine;

namespace ProjectBoost
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject m_pauseContainer = default;

        private bool m_isGamePaused = false;

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                FlipFlopPauseGame();
            }
        }

        public void FlipFlopPauseGame()
        {
            m_isGamePaused = !m_isGamePaused;
            m_pauseContainer?.SetActive(m_isGamePaused);

            EventManager.TriggerEvent(EventsName.GamePause, m_isGamePaused);
        }
    }
}

