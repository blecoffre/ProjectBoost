using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorController : MonoBehaviour
{
    [SerializeField]
    private LevelBlocView m_levelBlocViewPrefab = default;
    [SerializeField]
    private Transform m_blocViewContainer = default;

    private LevelBlocView[] m_views;

    private void Start()
    {
        PopulateView();
    }

    private void PopulateView()
    {
        List<string> levels = LevelManager.GetLevels();
        m_views = new LevelBlocView[levels.Count];

        for (int i = 0; i < levels.Count; i++)
        {
            LevelBlocView view = Instantiate(m_levelBlocViewPrefab, m_blocViewContainer);
            view.Initialize(levels[i], LevelManager.GetLevelRecord(levels[i]));
            m_views[i] = view;
        }
    }
    
    public void LoadLevel(string levelName)
    {
        DisableAllViewsTrigger();
        LevelManager.LoadLevel(levelName);
    }

    /// <summary>
    /// Mute all sound and other effect on buttons
    /// </summary>
    private void DisableAllViewsTrigger()
    {
        for (int i = 0; i < m_views.Length; i++)
        {
            m_views[i].Disable();
        }
    }
}
