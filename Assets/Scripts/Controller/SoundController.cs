using ProjectBoost.Manager;
using ProjectBoost.View;
using UnityEngine;

namespace ProjectBoost.Controller
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private SoundView m_view = default;

        private MusicSource[] m_musicSources;
        private SoundEffectSource[] m_soundEffectSources;

        private void Start()
        {
            m_view?.Initialize(SoundManager.GetMusicState(), SoundManager.GetSoundState());
            GetAllAudioSource();
            SetAllSourcesState();
        }

        private void GetAllAudioSource()
        {
            m_musicSources = FindObjectsOfType<MusicSource>();
            m_soundEffectSources = FindObjectsOfType<SoundEffectSource>();
        }

        private void SetAllSourcesState()
        {
            SetMusicSourcesState(SoundManager.GetMusicState());
            SetSoundEffectSourcesState(SoundManager.GetSoundState());
        }

        private void SetMusicSourcesState(bool mute)
        {
            foreach (MusicSource musicSource in m_musicSources)
            {
                musicSource.AudioSource.mute = mute;
            }

            m_view?.FlipFlopMusicSourceMute(mute);
        }

        private void SetSoundEffectSourcesState(bool mute)
        {
            foreach (SoundEffectSource sounndEffectSource in m_soundEffectSources)
            {
                sounndEffectSource.AudioSource.mute = mute;
            }

            m_view?.FlipFlopSoundEffectSourceMute(mute);
        }

        public void FlipFlopMusicSourceMute()
        {
            bool mute = !SoundManager.GetMusicState();
            SetMusicSourcesState(mute);

            SoundManager.SetMusicState(mute);
        }

        public void FlipFlopSoundEffectSourceMute()
        {
            bool mute = !SoundManager.GetSoundState();
            SetSoundEffectSourcesState(mute);

            SoundManager.SetSoundState(mute);
        }
    }
}

