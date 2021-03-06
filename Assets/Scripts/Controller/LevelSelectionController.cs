﻿using TrickyRocket.Manager;
using TrickyRocket.View;
using UnityEngine;

namespace TrickyRocket.Controller
{
    public class LevelSelectionController : MonoBehaviour
    {
        [SerializeField] private LevelSelectionView m_levelSelectionView = default;

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_levelSelectionView.Populate(LevelUtility.GetAvailableLevels());
        }

        public void LoadLevel(string levelName)
        {
            m_levelSelectionView.DisableAllViewsTrigger();
            LevelManager.LoadLevel(levelName);
        }
    }
}