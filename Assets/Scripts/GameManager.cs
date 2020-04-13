using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UnityAction<object> m_saveAction;

    private void Start()
    {
        m_saveAction += SaveTimeToPlayerPrefs;
        EventManager.StartListening(EventsName.SaveScoreTime, m_saveAction);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventsName.SaveScoreTime, m_saveAction);
    }

    private void SaveTimeToPlayerPrefs(object scoreTime)
    {
        string levelName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(levelName, scoreTime.ToString());

        PlayerPrefs.Save();
    }

    private void ReadTime(string levelName)
    {
        if (PlayerPrefs.HasKey("Level1"))
        {
            string timestamp = PlayerPrefs.GetString("Level1");
            Debug.Log(timestamp);
        }
    }
}
