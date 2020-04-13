using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private PopInAndOutUI m_titlePop;
    [SerializeField] private float m_timeBeforeOpenMenu = 3.0f;
    void Start()
    {
        m_titlePop.PlayPopIn();
        LevelManager.LoadLevel("MainMenu", true, m_timeBeforeOpenMenu);
    }
}
