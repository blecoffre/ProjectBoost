using UnityEngine;

namespace ProjectBoost.Manager
{
    public class SaveManager
    {

        public static void SaveLevelTimeRecordToPlayerPrefs(object scoredTime)
        {
            string levelName = LevelUtility.GetCurrentLevelName();
            PlayerPrefs.SetFloat(levelName, (float)scoredTime);

            PlayerPrefs.Save();
        }

        public static float GetLevelRecord(string levelName)
        {
            if (PlayerPrefs.HasKey(levelName))
            {
                return PlayerPrefs.GetFloat(levelName);
            }

            return 0.0f;
        }
    }
}
