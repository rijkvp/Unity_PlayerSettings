using UnityEngine;
using UnityEngine.UI;

namespace PlayerSettings
{
    public class SettingDropdown : MonoBehaviour, ISettingUIElement
    { 
        public string key = "";

        void Awake()
        {
            Dropdown dropdown = GetComponent<Dropdown>();
            dropdown.onValueChanged.AddListener(delegate { OnValueChanged(dropdown.value); });

            SettingsManager.RegisterUIElement(this);
        }

        void OnEnable()
        {
            Load();
        }

        public void Load()
        {
            GetComponent<Dropdown>().value = System.Convert.ToInt32(SettingsManager.GetSetting(key, SettingType.Int));
        }

        public void OnValueChanged(int value)
        {
            SettingsManager.SetSetting(key, SettingType.Int, value.ToString());
        }
    }
}
