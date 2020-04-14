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

        public static void SaveLevelUnlocked(string levelName)
        {
            string key = string.Format("{0}Unlock", levelName);
            PlayerPrefs.SetString(key, key); //Simply register the key as the value too, we dont need the content for later check
        }

        public static bool CheckIfLevelIsUnlocked(string levelName)
        {
            if (levelName == LevelUtility.GetAvailableLevels()[0])
                return true;

            string key = string.Format("{0}Unlock", levelName);
            if (PlayerPrefs.HasKey(key)) //If key exist, it always means player has unlocked this level, no need other check
            {
                return true;
            }

            return false;
        }
    }
}
