using System.Collections;
using UnityEngine;

namespace ProjectBoost.UIExtension
{
    [ExecuteInEditMode]
    public class PopInAndOutUI : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_startScale;
        [SerializeField]
        private Vector3 m_endScale;
        [SerializeField]
        [Tooltip("In seconds")]
        private float m_animationTime;
        [SerializeField]
        private bool m_popIn = false;

        private float m_currentPassedTime = 0.0f;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (m_popIn)
                PlayPopIn();
            else
                PlayPopOut();
#endif
        }

        public void PlayPopIn()
        {
            m_currentPassedTime = 0.0f;
            StartCoroutine(PopAnimation(m_startScale, m_endScale));
        }

        public void PlayPopOut()
        {
            m_currentPassedTime = 0.0f;
            StartCoroutine(PopAnimation(m_endScale, m_startScale));
        }

        private IEnumerator PopAnimation(Vector3 startScale, Vector3 endScale)
        {
            SlerpScale(startScale, endScale);
            yield return new WaitForEndOfFrame();

            while (transform.localScale != endScale)
            {
                SlerpScale(startScale, endScale);
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        private void SlerpScale(Vector3 startScale, Vector3 endScale)
        {
            float deltaTime = 0.0f;
#if UNITY_EDITOR
            deltaTime = (float)EditorDeltaTime.Instance.DeltaTime;
#else
        deltaTime = Time.deltaTime;
#endif
            m_currentPassedTime += deltaTime / m_animationTime;
            transform.localScale = Vector3.Slerp(startScale, endScale, m_currentPassedTime);
        }
    }

}
