using UnityEngine;
using UnityEngine.UI;

namespace PlayerSettings
{
    public class SettingDropdown : MonoBehaviour
    { 
        public string key = "";

        void Start()
        {
            Dropdown dropdown = GetComponent<Dropdown>();

            dropdown.onValueChanged.AddListener(delegate { SetSetting(dropdown.value); });

            Load();
        }

        public void Load()
        {
            GetComponent<Dropdown>().value = System.Convert.ToInt32(SettingsManager.instance.GetSetting(key, SettingType.Int));
        }

        public void SetSetting(int value)
        {
            SettingsManager.instance.SetSetting(key, SettingType.Int, value.ToString());
        }
    }
}
