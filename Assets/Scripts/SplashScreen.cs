using TrickyRocket.Const;
using TrickyRocket.Manager;
using TrickyRocket.UIExtension;
using UnityEngine;

namespace TrickyRocket
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

