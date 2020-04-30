using TrickyRocket.Const;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace TrickyRocket
{
    class LevelUtility
    {
        private static List<string> m_levels;

        public static List<string> GetAvailableLevels()
        {
            if(m_levels == null || m_levels.Count == 0)
            {
                int sceneCount = SceneManager.sceneCountInBuildSettings;
                List<string> scenes = new List<string>();
                for (int i = 0; i < sceneCount; i++)
                {
                    scenes.Add(Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i)));
                }

                m_levels = new List<string>();

                foreach (string scene in scenes)
                {
                    if (scene.Contains("Level"))
                    {
                        m_levels.Add(scene);
                    }
                }
            }

            return m_levels;
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

        /// <summary>
        /// Need to check if returned value is different of "No More Levels" !
        /// </summary>
        /// <returns></returns>
        public static string GetNextLevelName()
        {
            if (m_levels.Count > (m_levels.IndexOf(GetCurrentLevelName()) + 1))
                return m_levels[m_levels.IndexOf(GetCurrentLevelName()) + 1];

            return "No More Levels";
        }
    }
}
