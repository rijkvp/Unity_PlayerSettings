using UnityEngine;

namespace PlayerSettings
{
    public class InputManager : MonoBehaviour
    {
        #region Singleton

        public static InputManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("Multiple instances of InputManager!");
        }

        #endregion

        [Header("Movement Keys")]
        public string forwardSettingKey = "forward_key";
        public string backwardSettingKey = "backward_key";
        public string rightSettingKey = "right_key";
        public string leftSettingKey = "left_key";
        public string jumpSettingKey = "jump_key";

        [Header("Mouse Sensitivity Keys")]
        public string sensitivitySettingKey = "mouse_sensitivity";
        public string zoomSensitivityKey = "zoom_sensitivity";

        private static KeyCode forwardKeyCode;
        private static KeyCode backwardKeyCode;
        private static KeyCode rightKeyCode;
        private static KeyCode leftKeyCode;
        private static KeyCode jumpKeyCode;

        private static float sensitivity;
        private static float zoomSensitivity;

        private void Start()
        {
            LoadInputSettings();
        }

        public static void LoadInputSettings()
        {
            forwardKeyCode = SettingsManager.GetKeyCode(instance.forwardSettingKey);
            backwardKeyCode = SettingsManager.GetKeyCode(instance.backwardSettingKey);
            rightKeyCode = SettingsManager.GetKeyCode(instance.rightSettingKey);
            leftKeyCode = SettingsManager.GetKeyCode(instance.leftSettingKey);
            jumpKeyCode = SettingsManager.GetKeyCode(instance.jumpSettingKey);

            sensitivity = SettingsManager.GetFloat(instance.sensitivitySettingKey);
            zoomSensitivity = SettingsManager.GetFloat(instance.zoomSensitivityKey);
        }

        public static bool GetJumpButtonDown()
        {
            return Input.GetKeyDown(jumpKeyCode);
        }

        public static Vector2 GetMovementVector()
        {
            return new Vector2(GetHorizontalAxis(), GetVerticalAxis());
        }

        public static float GetHorizontalAxis()
        {
            return ConvertToAxis(rightKeyCode, leftKeyCode);
        }

        public static float GetVerticalAxis()
        {
            return ConvertToAxis(forwardKeyCode, backwardKeyCode);
        }

        public static float GetMouseX(bool isZooming)
        {
            if (isZooming)
                return Input.GetAxis("Mouse X") * zoomSensitivity;
            else
                return Input.GetAxis("Mouse X") * sensitivity;
        }

        public static float GetMouseY(bool isZooming)
        {
            if (isZooming)
                return Input.GetAxis("Mouse Y") * zoomSensitivity;
            else
                return Input.GetAxis("Mouse Y") * sensitivity;
        }

        private static float ConvertToAxis(KeyCode positiveKey, KeyCode negativeKey)
        {
            if (Input.GetKey(positiveKey) && Input.GetKey(negativeKey))
                return 0.0f;
            else if (Input.GetKey(positiveKey))
                return 1.0f;
            else if (Input.GetKey(negativeKey))
                return -1.0f;
            else
                return 0.0f;
        }
    }

}