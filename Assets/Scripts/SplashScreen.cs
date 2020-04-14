using ProjectBoost.Const;
using ProjectBoost.Manager;
using ProjectBoost.UIExtension;
using UnityEngine;

namespace ProjectBoost
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] private PopInAndOutUI m_titlePop;
        [SerializeField] private float m_timeBeforeOpenMenu = 3.0f;
        void Start()
        {
            m_titlePop.PlayPopIn();
            SceneUtility.LoadSceneAsync(SceneNames.MainMenu, true, m_timeBeforeOpenMenu);
        }
    }
}

