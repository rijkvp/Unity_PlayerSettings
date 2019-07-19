using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SettingToggle : MonoBehaviour
{
    public string key = "";

    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(delegate {SetSetting(toggle.isOn); });
    }
    public void Load()
    {
        GetComponent<Toggle>().isOn = System.Convert.ToBoolean(PlayerSettings.instance.GetSetting(key, SettingType.Bool));
    }
    public void SetSetting(bool input)
    {
        if (key != "" && key != null) 
        {
            PlayerSettings.instance.SetSetting(key, SettingType.Bool, input.ToString());
        }
    }
}
