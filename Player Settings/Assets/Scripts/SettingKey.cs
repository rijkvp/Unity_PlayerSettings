using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SettingKey : MonoBehaviour
{
    public string key = "";
    private Text buttonText;
    private bool recording;
    void Start()
    {
        Button button = GetComponent<Button>();

        buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();

        button.onClick.AddListener(delegate {OnClick(); });

        Load();
    }
    public void Load()
    {
        buttonText.text = PlayerSettings.instance.GetSetting(key, SettingType.KeyCode);
    }
    public void OnClick()
    {
        if (!recording)
        {
            buttonText.text = "[Recording]";
            recording = true;
        }
    }
    void Update()
    {
        if (recording)
        {
            foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    recording = false;
                    buttonText.text = keyCode.ToString();
                    PlayerSettings.instance.SetSetting(key, SettingType.KeyCode, keyCode.ToString());
                }
            }
        }
    }
}
