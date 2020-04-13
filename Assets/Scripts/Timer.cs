using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_timerText = default;

    private float m_timer;
    private TimeSpan m_timeSpan;

    private UnityAction<object> m_levelRunningAction;
    private bool m_isLevelRunning = false;
    private bool m_asSentForSave = false;

    void Start()
    {
        m_timer = 0.0f;
        FormatTime();

        m_levelRunningAction += SetLevelIsRunning;
        EventManager.StartListening(EventsName.StartPlaying, m_levelRunningAction);
        EventManager.StartListening(EventsName.StopPlaying, m_levelRunningAction);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventsName.StartPlaying, m_levelRunningAction);
        EventManager.StopListening(EventsName.StopPlaying, m_levelRunningAction);
    }

    void Update()
    {
        if (m_isLevelRunning)
        {
            m_timer += Time.deltaTime;
            FormatTime();
        }
        else if(m_timer > 0 && !m_asSentForSave) //Player has score a time it still not save
        {
            SendForSave();
        }
    }

    private void SetLevelIsRunning(object isLevelRunning)
    {
        m_isLevelRunning = (bool)isLevelRunning;
    }

    private void FormatTime()
    {
        m_timeSpan = TimeSpan.FromSeconds(m_timer);
        if (m_timeSpan.TotalMinutes > 1)
        {
            m_timerText?.SetText(string.Format("{0}:{1}.{2}", (int)m_timeSpan.TotalMinutes, m_timeSpan.Seconds, m_timeSpan.Milliseconds));
        }
        else
        {
            m_timerText?.SetText(string.Format("{0}.{1}", m_timeSpan.Seconds, m_timeSpan.Milliseconds));
        }
    }

    private void SendForSave()
    {
        EventManager.TriggerEvent(EventsName.SaveScoreTime, m_timerText.text);
        m_asSentForSave = true;
    }
}
