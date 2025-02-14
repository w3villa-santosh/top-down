using UnityEditor;
using UnityEngine;

namespace TopDown.Camera
{
    public class TopdownCameraHandller : MonoBehaviour
    {
        public Transform m_Target;
        public float m_Height = 10f;
        public float m_Distance = 20f;
        public float m_Angle = 45f;
        public float m_SmoothSpeed = 0.5f;

        private Vector3 refVelocity;

        private void Start()
        {
            HandleCamera();
        }

        private void Update()
        {
            HandleCamera();
        }

        public void HandleCamera()
        {
            if (!m_Target)
                return;
            //world postion
            Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
            //Debug.DrawLine(m_Target.position, worldPosition, Color.red);

            //quertinian angle
            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(m_Target.position, rotatedVector, Color.green);

            //Move object
            Vector3 flatTargetPosition = m_Target.position;
            flatTargetPosition.y = 0f;
            Vector3 finalPosition = flatTargetPosition + rotatedVector;
            //Debug.DrawLine(m_Target.position, finalPosition, Color.blue);
            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);
            transform.LookAt(flatTargetPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            if (m_Target)
            {
                Gizmos.DrawLine(transform.position, m_Target.position);
                Gizmos.DrawSphere(m_Target.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 1.5f);
        }
    }
}
