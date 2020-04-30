using TrickyRocket.Const;
using TrickyRocket.Manager;
using UnityEngine;

namespace TrickyRocket.Gameplay
{
    public class EndLaunchPad : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                EventManager.TriggerEvent(EventsName.StopPlaying, false);
                EventManager.TriggerEvent(EventsName.UnlockNextLevel);
            }
        }
    }
}
