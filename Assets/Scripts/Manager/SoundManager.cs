
namespace TrickyRocket.Manager
{
    public class SoundManager
    {
        private static bool m_isMusicMute = false;
        private static bool m_isSoundMute = false;

        public static bool GetMusicState()
        {
            return m_isMusicMute;
        }

        public static bool GetSoundState()
        {
            return m_isSoundMute;
        }

        public static void SetMusicState(bool musicState)
        {
            m_isMusicMute = musicState;
            SaveManager.SaveMusicState(m_isMusicMute);
        }

        public static void SetSoundState(bool soundState)
        {
            m_isSoundMute = soundState;
            SaveManager.SaveSoundState(m_isSoundMute);
        }
    }
}

