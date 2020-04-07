using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float m_rcsThrust = 100f;
    [SerializeField]
    private float m_mainThrust = 1000f;

    private Rigidbody m_rigidbody;
    private AudioSource m_audioSource;

    void Start()
    {
        GetAllRequiredComponents();
    }

    private void GetAllRequiredComponents()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (!m_rigidbody)
            Debug.LogError("Can't find rigidbody component on Player rocket !");

        m_audioSource = GetComponent<AudioSource>();
        if (!m_audioSource)
            Debug.LogError("Can't find audio source component on Player rocket !");

    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustForceThisFrame = m_mainThrust * Time.deltaTime;
            //Thrust
            m_rigidbody.AddRelativeForce(Vector3.up * thrustForceThisFrame);

            if (!m_audioSource.isPlaying)
                m_audioSource.Play();
        }
        else
        {
            if (m_audioSource.isPlaying)
                m_audioSource.Stop();
        }
    }

    private void Rotate()
    {
        m_rigidbody.freezeRotation = true; //take manual control of rotation

        float rotationSpeed = m_rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
        {
            //Left
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Right
            transform.Rotate(Vector3.back * rotationSpeed);
        }

        m_rigidbody.freezeRotation = false; //resume physic's control of rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Collision");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                Debug.Log("Your dead");
                break;
        }
    }
}
