using ProjectBoost.Const;
using ProjectBoost.Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectBoost.Gameplay
{
    public class Rocket : MonoBehaviour
    {
        [Header("Main Rocket Controls Parameters")]
        [SerializeField]
        private float m_rcsThrust = 100f;
        [SerializeField]
        private float m_mainThrust = 1000f;

        [Header("AudioClips")]
        [SerializeField]
        private AudioClip m_mainEngineClip = default;
        [SerializeField]
        private AudioClip m_deathClip = default;
        [SerializeField]
        private AudioClip m_successClip = default;

        [Header("FX Particles Systems")]
        [SerializeField]
        private ParticleSystem m_mainEngineParticles = default;
        [SerializeField]
        private ParticleSystem m_deathParticles = default;
        [SerializeField]
        private ParticleSystem m_successParticles = default;

        [SerializeField] private string RocketPartAllowedToLand = default;

        private Rigidbody m_rigidbody;
        private AudioSource m_audioSource;

        private bool m_isCollisionEnabled = true;
        private bool m_isAlive = true;
        private Vector3 m_velocity;
        private bool m_noRotationApplied = true;

        private UnityAction<object> m_gamePausedAction = null;
        private bool m_isGamePaused = false;
        private RigidbodyConstraints m_rigidbodyConstraints;

        private void Start()
        {
            GetAllRequiredComponents();

            m_gamePausedAction += GamePaused;
            EventManager.StartListening(EventsName.GamePause, m_gamePausedAction);

            m_rigidbodyConstraints = m_rigidbody.constraints; //Get all constraints to reapplied them when game unpaused
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

        private void Update()
        {
            if (!m_isGamePaused)
            {
                m_velocity = m_rigidbody.velocity;

                ProcessInput(); //Put Inputs on update instead of FixedUpdate to improve Rotation Reactivity and Behaviour

                if (Debug.isDebugBuild)
                    Cheat();
            }
        }

        private void ProcessInput()
        {
            if (m_isAlive)
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
            if (m_rigidbody.velocity.y <= 0.2 && m_rigidbody.velocity.y >= -0.2) //block rotation if no velocity
                return;

            if (m_rigidbody && !m_noRotationApplied)
                m_rigidbody.angularVelocity = Vector3.zero; //remove rotation due to physics

            float rotationSpeed = m_rcsThrust * Time.deltaTime;

            if (Input.GetKey(KeyCode.Q))
            {
                //Left
                transform.Rotate(Vector3.forward * rotationSpeed);
                m_noRotationApplied = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //Right
                transform.Rotate(Vector3.back * rotationSpeed);
                m_noRotationApplied = false;
            }
            else
                m_noRotationApplied = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!m_isAlive || !m_isCollisionEnabled)
            {
                return;
            }

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    if (!SafeLanding(collision))
                        StartDeath();
                    break;
                case "Finish":
                    if (SafeLanding(collision))
                        StartSuccess();
                    else
                        StartDeath();
                    break;
                default:
                    StartDeath();
                    break;
            }
        }

        private void StartSuccess()
        {
            m_isAlive = false; //Disable controls
            PlayClip(m_successClip);
            PlayParticles(m_successParticles);
        }

        private void StartDeath()
        {
            EventManager.TriggerEvent(EventsName.PlayerDie);
            m_isAlive = false;
            PlayClip(m_deathClip);
            PlayParticles(m_deathParticles);
        }

        public float GetCollisionAngle(Transform hitobjectTransform, Collider collider, Vector3 contactPoint)
        {
            Vector3 collidertWorldPosition = new Vector2(hitobjectTransform.position.x, hitobjectTransform.position.y);
            Vector3 pointB = contactPoint - collidertWorldPosition;

            float theta = Mathf.Atan2(pointB.x, pointB.y);
            float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;
            return angle;
        }

        private bool SafeLanding(Collision collision)
        {
            if (string.IsNullOrEmpty(RocketPartAllowedToLand))
                Debug.LogError("RocketPartAllowedToLand is null or empty");


            Vector3 dir = (transform.up - collision.transform.up).normalized;

            if (collision.contacts[0].thisCollider.transform.name == RocketPartAllowedToLand && dir.y > -0.4f) //Force player to land more properly
            {
                return true;
            }

            return false;
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

        private void GamePaused(object isGamePaused)
        {
            if ((bool)isGamePaused)
                m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            else
                m_rigidbody.constraints = m_rigidbodyConstraints;

            m_isGamePaused = (bool)isGamePaused;
        }
    }
}