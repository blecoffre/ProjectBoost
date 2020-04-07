using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    void Start()
    {
        GetAllRequiredComponents();
    }

    private void GetAllRequiredComponents()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (!m_rigidbody)
            Debug.LogError("Can't find rigidbody component on Player rocket !");
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Thrust
            m_rigidbody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //Left
            transform.Rotate(Vector3.forward * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Right
            transform.Rotate(Vector3.back * Time.deltaTime);
        }
    }
}
