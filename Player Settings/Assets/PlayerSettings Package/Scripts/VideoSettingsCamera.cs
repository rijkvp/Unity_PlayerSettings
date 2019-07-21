using UnityEngine;

namespace PlayerSettings
{
    [RequireComponent(typeof(Camera))]
    public class VideoSettingsCamera : MonoBehaviour
    {
        [Header("Override Settings")]
        public int targetFrameRate = 240;
        public int vSyncCount = 0;
        public int antiAliasing = 0;

        [Header("Camera Settings")]
        public RenderingPath renderingPath = RenderingPath.Forward;

        void Awake()
        {
            Application.targetFrameRate = targetFrameRate;
            QualitySettings.antiAliasing = antiAliasing;
            QualitySettings.vSyncCount = vSyncCount;

            Camera cam = GetComponent<Camera>();
            cam.renderingPath = renderingPath;
        }
    }
}