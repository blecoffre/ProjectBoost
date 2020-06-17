using TrickyRocket.UIExtension;
using UnityEngine;

namespace TrickyRocket.View
{
    public class SplashScreenView : MonoBehaviour
    {
        [SerializeField] private PopInAndOutUI m_titlePop;

        void Start()
        {
            m_titlePop.PlayPopIn();
        }
    }
}

