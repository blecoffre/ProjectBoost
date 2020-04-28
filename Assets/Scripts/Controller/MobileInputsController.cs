using ProjectBoost.Gameplay;
using UnityEngine;

namespace ProjectBoost.Controller
{
    public class MobileInputsController : MonoBehaviour
    {
        [SerializeField] private MobileButtonHeldDown m_leftArrow = default;
        [SerializeField] private MobileButtonHeldDown m_rightArrow = default;
        [SerializeField] private MobileButtonHeldDown m_thurst = default;

        private Rocket m_rocket;

        void Start()
        {
#if UNITY_ANDROID
            m_rocket = FindObjectOfType<Rocket>();
#else
            gameObject.SetActive(false);
#endif
        }

        private void Update()
        {
            if (m_rocket)
            {
                RotateLeft();
                RotateRight();
                Thrust();
            }
            else
            {
                Debug.LogError("Missing Rocket reference");
            }
        }

        public void RotateLeft()
        {
            if (!m_rightArrow.m_buttonPressed && m_leftArrow.m_buttonPressed)
                m_rocket.RotateLeft();
        }

        public void RotateRight()
        {
            if (!m_leftArrow.m_buttonPressed && m_rightArrow.m_buttonPressed)
                m_rocket.RotateRight();
        }

        public void Thrust()
        {
            if (m_thurst.m_buttonPressed)
                m_rocket.ApplyThrust();
            else
                m_rocket.StopApplyingThrust();
        }
    }
}

