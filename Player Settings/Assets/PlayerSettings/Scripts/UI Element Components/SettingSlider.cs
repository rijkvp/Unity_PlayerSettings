using UnityEngine;
using UnityEngine.UI;
using System;

namespace PlayerSettings
{
    [RequireComponent(typeof(Slider))]
    public class SettingSlider : MonoBehaviour, ISettingUIElement
    {
        public string key = "";
        public SettingType type = SettingType.Float;

        void Awake()
        {
            Slider slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate { OnValueChanged(slider.value); });

            SettingsManager.RegisterUIElement(this);

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

        void OnEnable()
        {
            Load();
        }

        public void Load()
        {
            if (type == SettingType.Int)
            {
                int intValue = Convert.ToInt32(SettingsManager.GetSetting(key, type));
                float floatValue = (float)intValue * 0.01f;
                GetComponent<Slider>().value = floatValue;
            }
            else if (type == SettingType.Float)
            {
                GetComponent<Slider>().value = Convert.ToSingle(SettingsManager.GetSetting(key, type));
            }
        }

        public void OnValueChanged(float value)
        {
            if (type == SettingType.Float)
            {
                SettingsManager.SetSetting(key, type, value.ToString());
            }
            else if (type == SettingType.Int)
            {
                int intValue = (int)(value * 100.0f);
                SettingsManager.SetSetting(key, type, intValue.ToString());
            }
        }
    }
}