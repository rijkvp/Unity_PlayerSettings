using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerVideoSettings : MonoBehaviour
{
    public Dropdown qualityDropdown;
    public Dropdown resolutionDropdown;

    [Header("Setting Keys")]
    public string fullscreenKey = "fullscreen";
    public string qualityLevelKey = "qualitylevel";
    public string resolutionLevelKey = "resolutionlevel";

    public static PlayerVideoSettings instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("More then 1 instance of PlayerVideoSettings!");
            Destroy(this);
        }

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
        ApplyVideoSettings();
    }
    public void ApplyVideoSettings()
    {
        bool fullScreen = PlayerSettings.instance.GetBool(fullscreenKey);
        Screen.fullScreen = fullScreen; 
        QualitySettings.SetQualityLevel(PlayerSettings.instance.GetInt(qualityLevelKey));
        Resolution resolution = Screen.resolutions[PlayerSettings.instance.GetInt(resolutionLevelKey)];
        Screen.SetResolution(resolution.width, resolution.height, fullScreen);
    }
}
