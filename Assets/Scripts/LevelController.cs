using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{

    private UnityAction m_loadNextLevelAction;
    private UnityAction m_reloadLevelAction;

    private void Start()
    {
        m_loadNextLevelAction += LoadNextLevel;
        EventManager.StartListening(EventsName.LoadNextLevel, m_loadNextLevelAction);

        m_reloadLevelAction += ReloadLevel;
        EventManager.StartListening(EventsName.ReloadLevel, m_reloadLevelAction);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventsName.LoadNextLevel, m_loadNextLevelAction);
        EventManager.StopListening(EventsName.ReloadLevel, m_reloadLevelAction);
    }

    private void LoadNextLevel()
    {
        LevelManager.LoadNextLevel();
    }

    private void ReloadLevel()
    {
        LevelManager.ReloadLevel();
    }
}
