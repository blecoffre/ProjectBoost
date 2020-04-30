using UnityEngine;

namespace TrickyRocket.Gameplay
{
    public class RestartLevel : MonoBehaviour
    {
        public void Restart()
        {
            LevelUtility.LoadCurrentLevel();
        }
    }
}

