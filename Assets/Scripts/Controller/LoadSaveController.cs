using TrickyRocket.Manager;
using UnityEngine;

namespace TrickyRocket
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

