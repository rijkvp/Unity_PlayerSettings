using UnityEngine;

namespace PlayerSettings
{
    [RequireComponent(typeof(Camera))]
    public class VideoSettingsCamera : MonoBehaviour
    {
        public int targetFrameRate = 240;
        public RenderingPath renderingPath = RenderingPath.Forward;

        void Awake()
        {
            Application.targetFrameRate = targetFrameRate;

            Camera cam = GetComponent<Camera>();
            cam.renderingPath = renderingPath;
        }
    }
}