using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using ProjectBoost.Controller;

public class LevelBlocView : MonoBehaviour
{
    [SerializeField] private GameObject m_unlockedContainer;
    [SerializeField] private GameObject m_lockContainer = default;
    [SerializeField] private TextMeshProUGUI m_levelNameText = default;
    [SerializeField] private TextMeshProUGUI m_bestTimeText = default;

    private string m_levelName;

    public void Initialize(string levelName, string record, bool isUnlock)
    {
        m_levelName = levelName;
        m_levelNameText?.SetText(m_levelName);
        m_bestTimeText?.SetText(record);

        if (isUnlock)
        {
            m_unlockedContainer?.SetActive(true);
            m_lockContainer?.SetActive(false);
        }
        else
        {
            m_unlockedContainer?.SetActive(false);
            m_lockContainer?.SetActive(true);
        }
    }

    public void OpenLevel()
    {
        FindObjectOfType<LevelSelectionController>()?.LoadLevel(m_levelName);
    }

    public void Disable()
    {
        EventTrigger trigger = GetComponentInChildren<EventTrigger>();
        if (trigger) 
            trigger.enabled = false;
    }
}
