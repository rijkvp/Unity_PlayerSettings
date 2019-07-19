using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        GetComponent<Dropdown>().value = System.Convert.ToInt32(PlayerSettings.instance.GetSetting(key, SettingType.Int));
    }

    public void SetSetting(int value)
    {
        PlayerSettings.instance.SetSetting(key, SettingType.Int, value.ToString());
    }
}
