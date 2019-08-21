using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSettings
{
    [RequireComponent(typeof(Button))]
    public class SettingButton : MonoBehaviour, ISettingUIElement
    {
        public string button = "";
        public string recordingText = "[RECORDING]";

        private Text buttonText;
        private bool recording;

        void Awake()
        {
            Button button = GetComponent<Button>();
            buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
            button.onClick.AddListener(delegate { OnClick(); });

            SettingsManager.RegisterUIElement(this);
        }

        void OnEnable()
        {
            Load();
        }

        public void Load()
        {
            buttonText.text = SettingsManager.GetSetting(button, SettingType.Button);
        }

        public void OnClick()
        {
            if (!recording)
            {
                buttonText.text = recordingText;
                recording = true;
            }
        }

        void Update()
        {
            if (recording)
            {
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(keyCode))
                    {
                        recording = false;
                        buttonText.text = keyCode.ToString();
                        SettingsManager.SetSetting(button, SettingType.Button, keyCode.ToString());
                    }
                }
            }
        }
    }
}