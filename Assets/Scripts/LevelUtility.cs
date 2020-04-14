using ProjectBoost.Const;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace ProjectBoost
{
    class LevelUtility
    {
        public static List<string> GetAvailableLevels()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            List<string> scenes = new List<string>();
            for (int i = 0; i < sceneCount; i++)
            {
                scenes.Add(Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i)));

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

        public static void LoadNextLevel()
        {
            int curSceneIndex = SceneManager.GetActiveScene().buildIndex;

            string nextLevelPath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(curSceneIndex + 1);
            string levelName;

            if (nextLevelPath != string.Empty)
            {
                levelName = Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(curSceneIndex + 1));
            }
            else
            {
                levelName = SceneNames.MainMenu;
            }

            LoadLevel(levelName);
        }

        public static void LoadLevel(string levelName)
        {
            SceneUtility.LoadSceneAsync(levelName);
        }

        public static void LoadCurrentLevel()
        {
            SceneUtility.LoadSceneAsync(GetCurrentLevelName());
        }

        public static string GetCurrentLevelName()
        {
            return SceneManager.GetActiveScene().name;
        }
    }
}
