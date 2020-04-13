using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [Header("Main Rocket Controls Parameters")]
    [SerializeField]
    private float m_rcsThrust = 100f;
    [SerializeField]
    private float m_mainThrust = 1000f;

    [Header("Time before loading/reloading scene")]
    [SerializeField]
    private float m_timeBeforeLoadingScene = 1.0f;

    [Header("AudioClips")]
    [SerializeField]
    private AudioClip m_mainEngineClip;
    [SerializeField]
    private AudioClip m_deathClip;
    [SerializeField]
    private AudioClip m_successClip;

    [Header("FX Particles Systems")]
    [SerializeField]
    private ParticleSystem m_mainEngineParticles;
    [SerializeField]
    private ParticleSystem m_deathParticles;
    [SerializeField]
    private ParticleSystem m_successParticles;

    private Rigidbody m_rigidbody;
    private AudioSource m_audioSource;

    private bool m_isCollisionEnabled = true;

    enum RocketState
    {
        Alive, Dying, Transcending
    }
    private RocketState m_state = RocketState.Alive;

    private void Start()
    {
        GetAllRequiredComponents();
    }

    private void GetAllRequiredComponents()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (!m_rigidbody)
            SimpleErrorHandlerManager.MissingComponentError(typeof(Rigidbody), gameObject.name);

        m_audioSource = GetComponent<AudioSource>();
        if (!m_audioSource)
            SimpleErrorHandlerManager.MissingComponentError(typeof(AudioSource), gameObject.name);

    }

    private void FixedUpdate()
    {
        ProcessInput();

        if(Debug.isDebugBuild)
            Cheat();
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
            StopApplyingThrust();
        }
    }
 

    private void ApplyThrust()
    {
        float thrustForceThisFrame = m_mainThrust * Time.deltaTime;
        //Thrust
        m_rigidbody?.AddRelativeForce(Vector3.up * thrustForceThisFrame);

        if (m_audioSource && !m_audioSource.isPlaying)
            PlayClip(m_mainEngineClip);

        PlayParticles(m_mainEngineParticles);
    }

    private void StopApplyingThrust()
    {
        m_audioSource?.Stop();
        m_mainEngineParticles?.Stop();
    }

    private void RespondToRotateInput()
    {
        if (m_rigidbody) 
            m_rigidbody.angularVelocity = Vector3.zero; //remove rotation due to physics

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

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_state != RocketState.Alive || !m_isCollisionEnabled)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Do funny stuff ?
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
        m_state = RocketState.Transcending;
        PlayClip(m_successClip);
        PlayParticles(m_successParticles);
    }

    private void StartDeath()
    {
        EventManager.TriggerEvent(EventsName.ReloadLevel);
        m_state = RocketState.Dying;
        PlayClip(m_deathClip);
        PlayParticles(m_deathParticles);
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip)
        {
            if (clip != m_mainEngineClip)
            {
                m_audioSource?.Stop();
            }

            m_audioSource?.PlayOneShot(clip);
        }
    }

    private void PlayParticles(ParticleSystem particles)
    {
        if (particles)
        {
            if (particles != m_mainEngineParticles)
            {
                m_mainEngineParticles?.Stop();
            }

            particles?.Play();
        }
    }

    private void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetSceneByBuildIndex(index).IsValid())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_isCollisionEnabled = !m_isCollisionEnabled;
        }
    }
}
