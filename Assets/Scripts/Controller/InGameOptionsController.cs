using TrickyRocket.Const;
using TrickyRocket.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TrickyRocket
{
    public class InGameOptionsController : MonoBehaviour
    {
        //This is an exception,
        //Allowed to modify UI due to the small size of this script
        [SerializeField] private Button m_nextLevelButton = default;

        private void OnEnable()
        {
            if (m_nextLevelButton)
                m_nextLevelButton.interactable = LevelManager.IsNextLevelUnlocked();
        }

        public void BackToMenu()
        {
            SceneUtility.LoadSceneAsync(SceneNames.MainMenu);
        }

        public void ReloadCurrentLevel()
        {
            LevelManager.ReloadCurrentLevel();
        }

        public void OpenNextLevel()
        {
            LevelManager.LoadNextLevel();
        }
    }
}

