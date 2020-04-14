using UnityEngine;

namespace ProjectBoost.Gameplay
{
    public class RestartLevel : MonoBehaviour
    {
        public void Restart()
        {
            LevelUtility.LoadCurrentLevel();
        }
    }
}

