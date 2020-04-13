using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Assets.Scripts;

public class EndLevelView : MonoBehaviour
{
    [SerializeField] private GameObject m_container = default;
    [SerializeField] private TextMeshProUGUI m_yourTimeText = default;
    [SerializeField] private TextMeshProUGUI m_currentRecord = default;
    [SerializeField] private GameObject m_newRecordContainer = default;

    private UnityAction m_openViewAction = null;

    private void Start()
    {
        m_openViewAction += Initialize;
        EventManager.StartListening(EventsName.OpenEndLevelView, m_openViewAction);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventsName.OpenEndLevelView, m_openViewAction);
    }

    private void Initialize()
    {
        string record = string.Empty;
        if (LevelManager.IsNewRecord())
            record = LevelManager.GetCurrentLevelTimeAsString();
        else
            record = LevelManager.GetCurrentLevelRecordAsString();

        m_currentRecord?.SetText(FormatTime.FormatEndLevelRecord(record));

        SetCurrentTimeAndRecord();
        m_container?.SetActive(true);
    }

    private void SetCurrentTimeAndRecord()
    {
        m_yourTimeText?.SetText(FormatTime.FormatEndLevelTime(LevelManager.GetCurrentLevelTimeAsString()));
        m_newRecordContainer?.SetActive(LevelManager.IsNewRecord());
    }

    public void BackToMenu()
    {
        EventManager.TriggerEvent(EventsName.OpenMenu);
    }

    public void RetryCurrentLevel()
    {
        EventManager.TriggerEvent(EventsName.ReloadLevel);
    }

    public void OpenNextLevel()
    {
        EventManager.TriggerEvent(EventsName.LoadNextLevel);
    }
}
