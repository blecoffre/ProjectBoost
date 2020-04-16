using ProjectBoost.Manager;
using UnityEngine;

namespace ProjectBoost
{
    public class LoadSaveController : MonoBehaviour
    {
        void Start()
        {
            LoadBasicInformations();
        }

        private void LoadBasicInformations()
        {
            SoundManager.SetMusicState(SaveManager.GetMusicState());
            SoundManager.SetSoundState(SaveManager.GetSoundState());

            //TODO : Load Lock / Unlock Levels
        }
    }
}

