using UnityEngine;
using UnityEngine.UI;
using System;

namespace PlayerSettings
{
    [RequireComponent(typeof(Slider))]
    public class SettingSlider : MonoBehaviour
    {
        public string key = "";
        public SettingType type = SettingType.Float;

        void Start()
        {
            Slider slider = GetComponent<Slider>();

            slider.onValueChanged.AddListener(delegate { SetSetting(slider.value); });

            Load();

            switch (type)
            {
                case SettingType.Bool:
                    Debug.LogError("Bool type is not supported for Sliders! Use a Toggle & SettingToggle instead.");
                    break;
                case SettingType.Float:
                    break;
                case SettingType.Int:
                    break;
                case SettingType.Button:
                    Debug.LogError("KeyCode type is not supported for Sliders! Use a Button & SettingKey instead.");
                    break;
                case SettingType.String:
                    Debug.LogError("String type is not supported for Sliders! Use a InputField & SettingInputField instead.");
                    break;
                default:
                    break;
            }
        }

        public void Load()
        {
            if (type == SettingType.Int)
            {
                int intValue = Convert.ToInt32(SettingsManager.instance.GetSetting(key, type));
                float floatValue = (float)intValue * 0.01f;
                GetComponent<Slider>().value = floatValue;
            }
            else if (type == SettingType.Float)
            {
                GetComponent<Slider>().value = Convert.ToSingle(SettingsManager.instance.GetSetting(key, type));
            }
        }

        public void SetSetting(float value)
        {
            if (type == SettingType.Float)
            {
                SettingsManager.instance.SetSetting(key, type, value.ToString());
            }
            else if (type == SettingType.Int)
            {
                int intValue = (int)(value * 100.0f);
                SettingsManager.instance.SetSetting(key, type, intValue.ToString());
            }
        }
    }
}