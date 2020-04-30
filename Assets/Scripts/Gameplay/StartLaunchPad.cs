using TrickyRocket.Const;
using TrickyRocket.Manager;
using UnityEngine;

namespace TrickyRocket.Gameplay
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

