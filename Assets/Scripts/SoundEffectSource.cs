using UnityEngine;

namespace ProjectBoost
{
    //Use as a "tag" to separate Music Source and Sound Effect Source
    //May have some extra functionnality later
    public class SoundEffectSource : MonoBehaviour
    {
        private AudioSource m_audioSource;
        public AudioSource AudioSource
        {
            get
            {
                return m_audioSource;
            }
            protected set
            {
                m_audioSource = value;
            }
        }

        private void Awake()
        {
            m_audioSource = GetComponent<AudioSource>();

            if (!m_audioSource) //If no audio source found on gameObject, simply remove this script
                Destroy(this);
        }
    }
}

