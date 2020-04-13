using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{

    private UnityAction m_loadNextLevelAction = null;
    private UnityAction m_reloadLevelAction = null;
    private UnityAction<object> m_setCurrentTimeAction = null;


    private void Start()
    {
        m_loadNextLevelAction += LoadNextLevel;
        EventManager.StartListening(EventsName.LoadNextLevel, m_loadNextLevelAction);

        m_reloadLevelAction += ReloadLevel;
        EventManager.StartListening(EventsName.ReloadLevel, m_reloadLevelAction);

        m_setCurrentTimeAction += SetCurrentLevelTime;
        EventManager.StartListening(EventsName.SendScoreTime, m_setCurrentTimeAction);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventsName.LoadNextLevel, m_loadNextLevelAction);
        EventManager.StopListening(EventsName.ReloadLevel, m_reloadLevelAction);
        EventManager.StopListening(EventsName.SendScoreTime, m_setCurrentTimeAction);
    }

    private void LoadNextLevel()
    {
        LevelManager.LoadNextLevel();
    }

    private void ReloadLevel()
    {
        LevelManager.ReloadLevel();
    }

    private void SetCurrentLevelTime(object time)
    {
           LevelManager.SetCurrentLevelTime((float)time);
    }
}
