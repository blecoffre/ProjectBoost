using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static string LevelName;
    public static bool WithProgressBar;
    public static float TimeBeforeOpen;

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

    public static string GetLevelRecord(string levelName)
    {
        if (PlayerPrefs.HasKey(levelName))
        {
            return PlayerPrefs.GetString(levelName);
        }
        else
        {
            return "None";
        }
    }
}
