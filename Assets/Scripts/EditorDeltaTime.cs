using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EditorDeltaTime : MonoBehaviour
{
    //TODO : Find a way to smooth DeltaTime
    private static EditorDeltaTime instance = null;
    public static EditorDeltaTime Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (EditorDeltaTime)FindObjectOfType(typeof(EditorDeltaTime));
                if (instance == null)
                    instance = (new GameObject("EditorDeltaTime")).AddComponent<EditorDeltaTime>();
            }
            return instance;
        }
    }

    public double DeltaTime = 0f;
    private double m_lastTimeSinceStartup = 0f;

    private void Update()
    {
        SetEditorDeltaTime();
    }

    private void SetEditorDeltaTime()
    {
        if (m_lastTimeSinceStartup <= Mathf.Epsilon)
        {
            m_lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        }

        DeltaTime = EditorApplication.timeSinceStartup - m_lastTimeSinceStartup;
        m_lastTimeSinceStartup = EditorApplication.timeSinceStartup;
    }
}
