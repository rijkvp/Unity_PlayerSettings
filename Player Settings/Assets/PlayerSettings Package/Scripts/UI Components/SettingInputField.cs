using UnityEngine;
using UnityEngine.UI;

namespace PlayerSettings
{
    [RequireComponent(typeof(InputField))]
    public class SettingInputField : MonoBehaviour
    {
        public string key = "";
        public SettingType type = SettingType.String;

        void Start()
        {
            InputField inputField = GetComponent<InputField>();

            inputField.onEndEdit.AddListener(delegate { SetSetting(inputField.text); });

            Load();

            switch (type)
            {
                case SettingType.Bool:
                    Debug.LogError("Bool type is not supported for InputFields! Use a Toggle & SettingToggle instead.");
                    break;
                case SettingType.Float:
                    inputField.contentType = InputField.ContentType.DecimalNumber;
                    break;
                case SettingType.Int:
                    inputField.contentType = InputField.ContentType.IntegerNumber;
                    break;
                case SettingType.KeyCode:
                    Debug.LogError("KeyCode type is not supported for InputFields! Use a Button & SettingKey instead.");
                    break;
                case SettingType.String:
                    inputField.contentType = InputField.ContentType.Standard;
                    break;
                default:
                    break;
            }
        }

        public void Load()
        {
            GetComponent<InputField>().text = SettingsManager.instance.GetSetting(key, type);
        }

        public void SetSetting(string input)
        {
            if (key != "" && key != null && input != null && input != "")
            {
                SettingsManager.instance.SetSetting(key, type, input);
            }
        }
    }
}