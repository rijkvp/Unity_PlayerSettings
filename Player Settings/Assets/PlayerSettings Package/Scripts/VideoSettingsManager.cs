using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace PlayerSettings
{
    public class VideoSettingsManager : MonoBehaviour
    {
        public bool enableEditing = false;

        [Header("UI")]
        public Dropdown qualityDropdown;
        public Dropdown resolutionDropdown;

        [Header("Setting Keys")]
        public string fullscreenKey = "fullscreen";
        public string qualityLevelKey = "qualitylevel";
        public string resolutionLevelKey = "resolutionlevel";

        public static VideoSettingsManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Debug.LogError("More then one instance of PlayerVideoSettings!");
                Destroy(this);
            }

            if (!enableEditing)
                return;

            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(QualitySettings.names.ToList());

            List<string> options = new List<string>();
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                string optionStr = Screen.resolutions[i].width.ToString() + " x " + Screen.resolutions[i].height.ToString();
                options.Add(optionStr);
            }

            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);
        }
        private void Start()
        {
            LoadVideoSettings();
        }
        public void LoadVideoSettings()
        {
            bool fullScreen = SettingsManager.instance.GetBool(fullscreenKey);
            Screen.fullScreen = fullScreen; 
            QualitySettings.SetQualityLevel(SettingsManager.instance.GetInt(qualityLevelKey));
            Resolution resolution = Screen.resolutions[SettingsManager.instance.GetInt(resolutionLevelKey)];
            Screen.SetResolution(resolution.width, resolution.height, fullScreen);
        }
    }
}