using UnityEngine;

namespace TrickyRocket
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(Light))]
    public class VolumetricLigthMesh : MonoBehaviour
    {
        public float m_maxOpacity = 0.25f;

        private MeshFilter m_filter;
        private Light m_light;

        private Mesh m_mesh;

        private void Start()
        {
            m_filter = GetComponent<MeshFilter>();
            m_light = GetComponent<Light>();

            if (m_light.type != LightType.Spot)
                Debug.LogError("Attached Volumetric Light Mesh to a non-supported light type");
        }

        private void Update()
        {
            m_mesh = BuildMesh();

            m_filter.mesh = m_mesh;
        }

        private Mesh BuildMesh()
        {
            m_mesh = new Mesh();

            float farPosition = Mathf.Tan(m_light.spotAngle * 0.5f * Mathf.Deg2Rad) * m_light.range;
            m_mesh.vertices = new Vector3[] { new Vector3(0,0,0),
                                              new Vector3(farPosition, farPosition, m_light.range),
                                              new Vector3(-farPosition, farPosition, m_light.range),
                                              new Vector3(-farPosition, -farPosition, m_light.range),
                                              new Vector3(farPosition, -farPosition, m_light.range)
            };

            m_mesh.colors = new Color[] {
                new Color(m_light.color.r,m_light.color.g,m_light.color.b, m_light.color.a * m_maxOpacity),
                new Color(m_light.color.r,m_light.color.g,m_light.color.b, m_light.color.a * 0),
                new Color(m_light.color.r,m_light.color.g,m_light.color.b, m_light.color.a * 0),
                new Color(m_light.color.r,m_light.color.g,m_light.color.b, m_light.color.a * 0),
                new Color(m_light.color.r,m_light.color.g,m_light.color.b, m_light.color.a * 0),
            };

            m_mesh.triangles = new int[]
            {
                0,1,2,
                0,2,3,
                0,3,4,
                0,4,1
            };

            return m_mesh;
        }
    }
}

