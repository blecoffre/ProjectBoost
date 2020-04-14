using ProjectBoost.Const;
using ProjectBoost.Manager;
using UnityEngine;

namespace ProjectBoost.Gameplay
{
    public class StartLaunchPad : MonoBehaviour
    {
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                EventManager.TriggerEvent(EventsName.StartPlaying, true);
        }
    }
}

