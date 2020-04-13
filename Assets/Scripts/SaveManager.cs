using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private UnityAction<object> m_saveAction = null;

    private void Start()
    {
        m_saveAction += SaveTimeToPlayerPrefs;
        EventManager.StartListening(EventsName.SaveRecord, m_saveAction);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventsName.SaveRecord, m_saveAction);
    }

    private void SaveTimeToPlayerPrefs(object scoreTime)
    {
        if (LevelManager.IsNewRecord())
        {
            string levelName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetFloat(levelName, (float)scoreTime);

            PlayerPrefs.Save();
        }
    }
}
