using ProjectBoost.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectBoost.Gameplay
{
    public class MobileButtonHeldDown :MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool m_buttonPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            m_buttonPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_buttonPressed = false;
        }
    }
}

