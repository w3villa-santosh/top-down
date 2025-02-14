using UnityEditor;
using UnityEngine;

namespace TopDown.Camera
{
    [CustomEditor(typeof(TopdownCameraHandller))]
    public class TopdownCameraEditor : Editor
    {
        private TopdownCameraHandller m_TargetCamera;
        private void OnEnable()
        {
            m_TargetCamera = (TopdownCameraHandller)target;
        }

        private void OnSceneGUI()
        {
            Transform camTarget = m_TargetCamera.m_Target;
            Handles.color = new Color(1f, 0f, 0f, 0.15f);
            Handles.DrawSolidDisc(m_TargetCamera.transform.position, Vector3.up, m_TargetCamera.m_Distance);

            Handles.color = new Color(1f, 1f, 0f, 0.75f);
            Handles.DrawWireDisc(m_TargetCamera.transform.position, Vector3.up, m_TargetCamera.m_Distance);

            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            m_TargetCamera.m_Distance = Handles.ScaleSlider(m_TargetCamera.m_Distance, camTarget.position, -camTarget.forward, Quaternion.identity, m_TargetCamera.m_Distance, 1f);
            m_TargetCamera.m_Distance = Mathf.Clamp(m_TargetCamera.m_Distance, 10f, float.MaxValue);

            Handles.color = new Color(0f, 0f, 1f, 0.5f);
            m_TargetCamera.m_Height = Handles.ScaleSlider(m_TargetCamera.m_Height, camTarget.position, Vector3.up, Quaternion.identity, m_TargetCamera.m_Height, 1f);
            m_TargetCamera.m_Height = Mathf.Clamp(m_TargetCamera.m_Height, 10f, float.MaxValue);

            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 15;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperCenter;
            Handles.Label(camTarget.position+(-camTarget.forward * m_TargetCamera.m_Distance), "Distance", labelStyle);
            Handles.Label(camTarget.position + (Vector3.up * m_TargetCamera.m_Height), "Height", labelStyle);

        }
    }
}
