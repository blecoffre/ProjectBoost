using UnityEngine;

namespace TrickyRocket.Gameplay
{
    [DisallowMultipleComponent]

    public class Oscillator : MonoBehaviour
    {
        [Header("Please take in consideration the scale of the object this script is attached to")]
        [SerializeField]
        private Vector3 m_targetPosition = new Vector3(10f, 10f, 10f);
        [SerializeField]
        private float m_movementSpeed = 2f; //Time to make a full movement cycle
        private Vector3 m_startingPosition;

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
            transform.position = Vector3.Lerp(m_startingPosition, m_targetPosition, (Mathf.Sin(m_movementSpeed * Time.time) + 1.0f) / 2.0f);
        }
    }
}
