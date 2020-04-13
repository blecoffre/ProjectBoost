using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLaunchPad : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            EventManager.TriggerEvent(EventsName.StartPlaying, true);
    }
}
