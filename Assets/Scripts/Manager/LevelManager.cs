using ProjectBoost.Const;

namespace ProjectBoost.Manager
{
    public class LevelManager
    {
        private static float m_currentLevelTime;

        public static void LoadLevel(string levelName)
        {
            LevelUtility.LoadLevel(levelName);
        }

        public static void LoadNextLevel()
        {
            LevelUtility.LoadNextLevel();
        }

        public static void ReloadCurrentLevel()
        {
            LevelUtility.LoadCurrentLevel();
        }

        public static string GetLevelRecordAsString(string levelName)
        {
            float levelRecord = SaveManager.GetLevelRecord(levelName);

            if (levelRecord > 0.0f)
            {
                return FormatTime.FormatLevelTime(levelRecord);
            }
            else
            {
                return "None";
            }
        }

        public static string GetCurrentLevelRecordAsString()
        {
            string currentLevelName = LevelUtility.GetCurrentLevelName();
            return GetLevelRecordAsString(currentLevelName);
        }

        public static float GetLevelRecordAsFloat(string levelName)
        {
            return SaveManager.GetLevelRecord(levelName);
        }

        public static float GetCurrentLevelRecordAsFloat()
        {
            string currentLevelName = LevelUtility.GetCurrentLevelName();
            return GetLevelRecordAsFloat(currentLevelName);
        }

        public static string GetCurrentLevelTimeAsString()
        {
            return FormatTime.FormatLevelTime(m_currentLevelTime);
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

        /// <summary>
        /// Check if it's a new record and save on PlayerPref
        /// </summary>
        private static void ProcessWithNewLevelTime()
        {
            EventManager.TriggerEvent(EventsName.LevelComplete);
            if (IsNewRecord())
            {
                SaveManager.SaveLevelTimeRecordToPlayerPrefs(m_currentLevelTime);
            }
        }

        public static void UnlockNextLevel()
        {
            string nextLevelName = LevelUtility.GetNextLevelName();

            if (nextLevelName != "No More Levels")
                SaveManager.SaveLevelUnlocked(nextLevelName);
        }

        public static bool IsLevelUnlocked(string levelName)
        {
            return SaveManager.CheckIfLevelIsUnlocked(levelName);
        }

        public static bool IsNextLevelUnlocked()
        {
            string nextLevelName = LevelUtility.GetNextLevelName();

            if (nextLevelName != "No More Levels")
                return SaveManager.CheckIfLevelIsUnlocked(nextLevelName);

            return false;
        }
    }
}
