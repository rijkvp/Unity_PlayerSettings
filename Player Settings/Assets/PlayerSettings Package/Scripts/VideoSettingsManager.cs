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
        public Dropdown aaDropdown;
        public Dropdown vsyncDropdown;

        [Header("Setting Keys")]
        public string fullscreenKey = "fullscreen";
        public string qualityLevelKey = "qualitylevel";
        public string resolutionLevelKey = "resolutionlevel";
        public string aaLevelKey = "antialiasinglevel";
        public string vsyncCountKey = "vsync_count";

        public static VideoSettingsManager instance;

        private static string[] aaOptions = { "Disabled", "2x multi sampling", "4x multi sampling", "8x multi sampling" };
        private static string[] vsyncOptions = { "Disabled", "Every V blank", "Every second V blank" };

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

            // Quality
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(QualitySettings.names.ToList());

            // Resolutions
            List<string> options = new List<string>();
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                string optionStr = Screen.resolutions[i].width.ToString() + " x " + Screen.resolutions[i].height.ToString();
                options.Add(optionStr);
            }
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(options);

            // AA
            aaDropdown.ClearOptions();
            aaDropdown.AddOptions(aaOptions.ToList());

            // Vsync
            vsyncDropdown.ClearOptions();
            vsyncDropdown.AddOptions(vsyncOptions.ToList());
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
            QualitySettings.vSyncCount = SettingsManager.instance.GetInt(vsyncCountKey);
            int aaLevel = SettingsManager.instance.GetInt(aaLevelKey);
            int actualAA = 0;
            switch (aaLevel)
            {
                case 0:
                    actualAA = 0;
                    break;
                case 1:
                    actualAA = 2;
                    break;
                case 2:
                    actualAA = 4;
                    break;
                case 3:
                    actualAA = 8;
                    break;
            }
            QualitySettings.antiAliasing = actualAA;
        }
    }
}