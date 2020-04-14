using ProjectBoost.Manager;
using ProjectBoost.View;
using UnityEngine;

namespace ProjectBoost.Controller
{
    public class LevelSelectionController : MonoBehaviour
    {
        [SerializeField] private LevelSelectionView m_levelSelectionView = default;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            System.Collections.Generic.List<string> truc = LevelUtility.GetAvailableLevels();
            m_levelSelectionView.Populate(LevelUtility.GetAvailableLevels());
        }

        public void LoadLevel(string levelName)
        {
            m_levelSelectionView.DisableAllViewsTrigger();
            LevelManager.LoadLevel(levelName);
        }
    }
}