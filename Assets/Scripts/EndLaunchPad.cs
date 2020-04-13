using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLaunchPad : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventManager.TriggerEvent(EventsName.StopPlaying, false);
        }
    }
}
