using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LevelBlocView : MonoBehaviour
{
    [SerializeField] private Button m_button;

    [SerializeField] private TextMeshProUGUI m_levelNameText = default;
    [SerializeField] private TextMeshProUGUI m_bestTimeText = default;

    private string m_levelName;

    public void Initialize(string levelName, string record)
    {
        m_levelName = levelName;
        m_levelNameText?.SetText(m_levelName);
        m_bestTimeText?.SetText(record);

        m_button.onClick.AddListener(OpenLevel);
    }

    private void OpenLevel()
    {
        FindObjectOfType<LevelSelectorController>()?.LoadLevel(m_levelName);
    }

    public void Disable()
    {
        EventTrigger trigger = GetComponentInChildren<EventTrigger>();
        if (trigger) 
            trigger.enabled = false;
    }
}
