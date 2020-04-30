using TrickyRocket.Manager;
using System.Collections.Generic;
using UnityEngine;

namespace TrickyRocket.View
{
    class LevelSelectionView : MonoBehaviour
    {
        [SerializeField]
        private LevelBlocView m_levelBlocViewPrefab = default;
        [SerializeField]
        private Transform m_blocViewContainer = default;

        private LevelBlocView[] m_views;

        public void Populate(List<string> levels)
        {
            m_views = new LevelBlocView[levels.Count];

            for (int i = 0; i < levels.Count; i++)
            {
                LevelBlocView view = Instantiate(m_levelBlocViewPrefab, m_blocViewContainer);
                view.Initialize(levels[i], LevelManager.GetLevelRecordAsString(levels[i]), LevelManager.IsLevelUnlocked(levels[i]));
                m_views[i] = view;
            }
        }

        /// <summary>
        /// Disable Event Trigger scripts attached to Buttons
        /// </summary>
        public void DisableAllViewsTrigger()
        {
            for (int i = 0; i < m_views.Length; i++)
            {
                m_views[i].Disable();
            }
        }
    }
}
