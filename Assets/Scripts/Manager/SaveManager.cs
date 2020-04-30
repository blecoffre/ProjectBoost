using TrickyRocket.Const;
using System;
using UnityEngine;

namespace TrickyRocket.Manager
{
    public class SaveManager
    {
        #region Level
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
            string key = string.Format(PlayerPrefsKey.LevelUnlockedKey, levelName);
            PlayerPrefs.SetString(key, key); //Simply register the key as the value too, we dont need the content for later check
        }

        public static bool CheckIfLevelIsUnlocked(string levelName)
        {
            if (levelName == LevelUtility.GetAvailableLevels()[0])
                return true;

            string key = string.Format(PlayerPrefsKey.LevelUnlockedKey, levelName);
            if (PlayerPrefs.HasKey(key)) //If key exist, it always means player has unlocked this level, no need other check
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Sound
        public static void SaveMusicState(bool musicState)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.MusicStateKey, Convert.ToInt32(musicState));

            PlayerPrefs.Save();
        }

        public static void SaveSoundState(bool soundState)
        {
            PlayerPrefs.SetInt(PlayerPrefsKey.SoundStateKey, Convert.ToInt32(soundState));

            PlayerPrefs.Save();
        }

        public static bool GetMusicState()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.MusicStateKey))
            {
                return Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefsKey.MusicStateKey));
            }

            return true; //If can't find key, consider music in on
        }

        public static bool GetSoundState()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKey.SoundStateKey))
            {
                return Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefsKey.SoundStateKey));
            }

            return true; //If can't find key, consider music in on
        }
        #endregion
    }
}
