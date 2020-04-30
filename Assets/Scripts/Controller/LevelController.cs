using TrickyRocket.Const;
using TrickyRocket.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TrickyRocket.Controller
{
    public class LevelController : MonoBehaviour
    {

        private UnityAction m_loadNextLevelAction = null;
        private UnityAction m_reloadLevelAction = null;
        private UnityAction<object> m_setCurrentTimeAction = null;
        private UnityAction m_unlockLevelAction = null;

        private void Start()
        {
            m_loadNextLevelAction += LoadNextLevel;
            EventManager.StartListening(EventsName.LoadNextLevel, m_loadNextLevelAction);

            m_reloadLevelAction += ReloadLevel;
            EventManager.StartListening(EventsName.ReloadLevel, m_reloadLevelAction);

            m_setCurrentTimeAction += SetCurrentLevelTime;
            EventManager.StartListening(EventsName.SendScoreTime, m_setCurrentTimeAction);

            m_unlockLevelAction += UnlockNextLevel;
            EventManager.StartListening(EventsName.UnlockNextLevel, m_unlockLevelAction);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventsName.LoadNextLevel, m_loadNextLevelAction);
            EventManager.StopListening(EventsName.ReloadLevel, m_reloadLevelAction);
            EventManager.StopListening(EventsName.SendScoreTime, m_setCurrentTimeAction);
            EventManager.StopListening(EventsName.UnlockNextLevel, m_unlockLevelAction);
        }

        private void LoadNextLevel()
        {
            LevelManager.LoadNextLevel();
        }

        private void ReloadLevel()
        {
            LevelManager.ReloadCurrentLevel();
        }

        private void SetCurrentLevelTime(object time)
        {
            LevelManager.SetCurrentLevelTime((float)time);
        }

        private void UnlockNextLevel()
        {
            LevelManager.UnlockNextLevel();
        }
    }
}