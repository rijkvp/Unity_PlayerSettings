using UnityEngine;
using UnityEngine.UI;

namespace PlayerSettings
{
    [RequireComponent(typeof(Toggle))]
    public class SettingToggle : MonoBehaviour, ISettingUIElement
    {
        public string key = "";

        void Awake()
        {
            Toggle toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate { OnValueChanged(toggle.isOn); });

            SettingsManager.RegisterUIElement(this);
        }

        void OnEnable()
        {
            Load();
        }

        public void Load()
        {
            GetComponent<Toggle>().isOn = SettingsManager.GetBool(key);
        }

        public void OnValueChanged(bool value)
        {
            if (key != "" && key != null)
            {
                SettingsManager.SetSetting(key, SettingType.Bool, value.ToString());
            }
        }
    }
}