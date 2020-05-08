using UnityEngine;

namespace TrickyRocket.Gameplay
{
    [DisallowMultipleComponent]
    //[ExecuteInEditMode]
    public class Rotator : MonoBehaviour
    {
        private enum Axis
        {
            XAxis, YAxis, ZAxis
        }

        [SerializeField]
        private Quaternion m_startingRotation = Quaternion.identity;
        [SerializeField]
        private float m_angle = 180;
        [Header("The first checked is used for the rotation")]
        [SerializeField]
        private Axis m_axis = Axis.XAxis;

        [SerializeField]
        private float m_period = 2f; //Time to make a full movement cycle

        private const float tau = Mathf.PI * 2; //about 6.28
        private Vector3 m_rotationVector = Vector3.zero;


        private void Start()
        {
            transform.rotation = m_startingRotation;
            switch (m_axis)
            {
                case Axis.XAxis:
                    m_rotationVector = transform.forward;
                    break;
                case Axis.YAxis:
                    m_rotationVector = transform.up;
                    break;
                case Axis.ZAxis:
                    m_rotationVector = transform.right;
                    break;
            }
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (m_period <= Mathf.Epsilon)
            {
                return;
            }

            float cycles = Time.time / m_period; //grows continually from 0

            float angle = Mathf.Sin(cycles * tau) * m_angle;
            transform.rotation = m_startingRotation * Quaternion.AngleAxis(angle, m_rotationVector);
        }
    }
}

