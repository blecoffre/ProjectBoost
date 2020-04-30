using UnityEngine;
using UnityEngine.UI;

namespace TrickyRocket.View
{
    public class SoundView : MonoBehaviour
    {
        [SerializeField] private Image m_musicStateImage = default;
        [SerializeField] private Sprite m_musicMuteImage = default;
        [SerializeField] private Sprite m_musicUnmuteImage = default;

        [SerializeField] private Image m_soundStateImage = default;
        [SerializeField] private Sprite m_soundMuteImage = default;
        [SerializeField] private Sprite m_soundUnmuteImage = default;

        public void Initialize(bool isMusicMute, bool isSoundEffectMute)
        {
            SetMusicStateImage(isMusicMute);
            SetSoundEffectStateImage(isSoundEffectMute);
        }

        public void FlipFlopMusicSourceMute(bool mute)
        {
            SetMusicStateImage(mute);
        }

        public void FlipFlopSoundEffectSourceMute(bool mute)
        {
            SetSoundEffectStateImage(mute);
        }

        private void SetMusicStateImage(bool isMusicMute)
        {
            if (m_musicStateImage)
                m_musicStateImage.sprite = isMusicMute ? m_musicMuteImage : m_musicUnmuteImage;
        }

        private void SetSoundEffectStateImage(bool isSoundEffectMute)
        {
            if (m_soundStateImage)
                m_soundStateImage.sprite = isSoundEffectMute ? m_soundMuteImage : m_soundUnmuteImage;
        }
    }
}

