using Assets.Scripts;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static string LevelName;
    public static bool WithProgressBar;
    public static float TimeBeforeOpen;

    private static float m_currentLevelTime;

    public static void LoadLevel(string levelName, bool withProgressBar = false, float timeBeforeOpen = 1.0f)
    {
        LevelName = levelName;
        WithProgressBar = withProgressBar;
        TimeBeforeOpen = timeBeforeOpen;

        SceneManager.LoadScene("SceneLoader", LoadSceneMode.Additive);
    }

    public static void LoadNextLevel()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;

        string nextLevelPath = SceneUtility.GetScenePathByBuildIndex(curSceneIndex + 1);
        string levelName;

        if (nextLevelPath != string.Empty)
        {
            levelName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(curSceneIndex + 1));
        }
        else
        {
            levelName = "MainMenu";
        }

        LoadLevel(levelName, timeBeforeOpen : 1.0f);
    }

    public static void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().name, timeBeforeOpen : 1.0f);
    }

    public static List<string> GetLevels()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        List<string> scenes = new List<string>();
        for (int i = 0; i < sceneCount; i++)
        {
            scenes.Add(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            
        }

        List<string> levelScenes = new List<string>();

        foreach (string scene in scenes)
        {
            if (scene.Contains("Level"))
            {
                levelScenes.Add(scene);
            }
        }

        return levelScenes;
    }

    public static string GetLevelRecordAsString(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName))
        {
            return FormatTime.FormatLevelTime(PlayerPrefs.GetFloat(levelName));
        }
        else
        {
            return "None";
        }
    }

    public static string GetCurrentLevelRecordAsString()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        return GetLevelRecordAsString(currentLevelName);
    }

    public static float GetLevelRecordAsFloat(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName))
        {
            float time = PlayerPrefs.GetFloat(levelName);
            return (PlayerPrefs.GetFloat(levelName));
        }
        else
        {
            return 0.0f;
        }
    }

    public static float GetCurrentLevelRecordAsFloat()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        return GetLevelRecordAsFloat(currentLevelName);
    }

    public static bool IsNewRecord()
    {
        float current = GetCurrentLevelRecordAsFloat();
        return (GetCurrentLevelRecordAsFloat() == 0.0f || GetCurrentLevelRecordAsFloat() > m_currentLevelTime);
    }

    public static void SetCurrentLevelTime(float time)
    {
        m_currentLevelTime = time;
        ProcessWithNewLevelTime();
    }

    private static void ProcessWithNewLevelTime()
    {
        EventManager.TriggerEvent(EventsName.OpenEndLevelView);
        if (IsNewRecord())
        {
            EventManager.TriggerEvent(EventsName.SaveRecord, m_currentLevelTime);
        }
    }

    public static string GetCurrentLevelTimeAsString()
    {
        return FormatTime.FormatLevelTime(m_currentLevelTime);
    }

}
