using ProjectBoost.Const;
using ProjectBoost.Manager;
using UnityEngine;

namespace ProjectBoost.Gameplay
{
    public class EndLaunchPad : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                EventManager.TriggerEvent(EventsName.StopPlaying, false);
            }
        }
    }
}
