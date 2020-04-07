using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField]
    private float m_period = 2f; //Time to make a full movement cycle

    private float m_movementFactor = 0; //0 for not moved, 1 for fully moved

    private Vector3 m_startingPosition;

    private const float tau = Mathf.PI * 2; //about 6.28


    void Start()
    {
        m_startingPosition = transform.position;
    }

    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        if (m_period <= Mathf.Epsilon)
        {   
            return;
        }

        float cycles = Time.time / m_period; //grows continually from 0

        float rawSinWave = Mathf.Sin(cycles * tau); //goes from -1 to +1

        m_movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = m_movementVector * m_movementFactor;
        transform.position = m_startingPosition + offset;
    }
}
