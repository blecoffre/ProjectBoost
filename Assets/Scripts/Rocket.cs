using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float m_rcsThrust = 100f;
    [SerializeField]
    private float m_mainThrust = 1000f;

    [SerializeField]
    private float m_timeBeforeLoadingScene = 1.0f;

    [SerializeField]
    private AudioClip m_mainEngineClip;
    [SerializeField]
    private AudioClip m_deathClip;
    [SerializeField]
    private AudioClip m_successClip;

    private Rigidbody m_rigidbody;
    private AudioSource m_audioSource;

    enum RocketState
    {
        Alive, Dying, Transcending
    }

    private RocketState m_state = RocketState.Alive;

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
        if(m_state == RocketState.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            m_audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        float thrustForceThisFrame = m_mainThrust * Time.deltaTime;
        //Thrust
        m_rigidbody.AddRelativeForce(Vector3.up * thrustForceThisFrame);

        if (!m_audioSource.isPlaying)
            PlayClip(m_mainEngineClip);
    }

    private void RespondToRotateInput()
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
        if (m_state != RocketState.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccess();
                break;
            default:
                StartDeath();
                break;
        }
    }

    private void StartSuccess()
    {
        Invoke("LoadNextLevel", m_timeBeforeLoadingScene);
        m_state = RocketState.Transcending;
        PlayClip(m_successClip);
    }

    private void StartDeath()
    {
        Invoke("ReloadLevel", m_timeBeforeLoadingScene);
        m_state = RocketState.Dying;
        PlayClip(m_deathClip);
    }

    AudioClip currentPlayingClip;
    private void PlayClip(AudioClip clip)
    {
        if (clip != m_mainEngineClip)
        {
            m_audioSource.Stop();
        }
       
        m_audioSource.PlayOneShot(clip);
    }

    private void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetSceneAt(index) != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
