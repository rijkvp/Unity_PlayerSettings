using UnityEngine;
using UnityEngine.UI;

namespace PlayerSettings
{
    [RequireComponent(typeof(InputField))]
    public class SettingInputField : MonoBehaviour, ISettingUIElement
    {
        public string key = "";
        public SettingType type = SettingType.String;

        void Awake()
        {
            InputField inputField = GetComponent<InputField>();
            inputField.onEndEdit.AddListener(delegate { OnEndEdit(inputField.text); });

            SettingsManager.RegisterUIElement(this);

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
                case SettingType.Button:
                    Debug.LogError("KeyCode type is not supported for InputFields! Use a Button & SettingKey instead.");
                    break;
                case SettingType.String:
                    inputField.contentType = InputField.ContentType.Standard;
                    break;
                default:
                    break;
            }
        }

        void OnEnable()
        {
            Load();
        }

        public void Load()
        {
            GetComponent<InputField>().text = SettingsManager.GetSetting(key, type);
        }

        public void OnEndEdit(string input)
        {
            if (key != "" && key != null && input != null && input != "")
            {
                SettingsManager.SetSetting(key, type, input);
            }
        }
    }
}