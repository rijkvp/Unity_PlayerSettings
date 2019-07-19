using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class PlayerSettings : MonoBehaviour
{
#region Variables
    [HideInInspector]public List<SettingItem> settings;
    public static PlayerSettings instance;
    private const string FILE_NAME = "playersettings.xml";
#endregion

#region UI Save/Load
    void Awake()
    {
        Setup();
    }
    void Setup()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError("More then 1 instance of PlayerSettings!");
            Destroy(this);
        }
        Load();
    }
    public void Load()
    {
        string path = Application.persistentDataPath + "/" +FILE_NAME;
        SettingsContainer loadData = SettingsContainer.Load(path);
        settings = loadData.settings;

        SettingInputField[] fields = FindObjectsOfType<SettingInputField>();
        foreach (var field in fields)
        {
            field.Load();
        }

        SettingToggle[] toggles = FindObjectsOfType<SettingToggle>();
        foreach (var toggle in toggles)
        {
            toggle.Load();
        }
    }
    public void Save()
    {
        string path = Application.persistentDataPath + "/" +FILE_NAME;
        SettingsContainer saveData = new SettingsContainer();
        saveData.settings = settings;
        saveData.Save(path);

        if (PlayerVideoSettings.instance)
            PlayerVideoSettings.instance.ApplyVideoSettings();
    }
#endregion

#region  UI Get/Set Setting
    public void SetSetting(string key, SettingType type, string value)
    {
        foreach(var item in settings)
        {
            if (key == item.Key)
            {
                if (type != item.Type)
                {
                    Debug.LogError("The types of the same key are not the same!");
                    return;
                }
                item.Value = value;
                return;
            }
        }

        Debug.LogError("The key '" + key + "' does not exists! \n Make sure they are added to the player settings and don't forget to save them!");
    }
    public string GetSetting(string key, SettingType type)
    {
        foreach(var item in settings)
        {
            if (key == item.Key)
            {
                if (type != item.Type)
                {
                    Debug.LogError("The types of the same key are not the same!");
                    return "ERROR 1";
                }
                return item.Value;
            }
        }

        Debug.LogError("Trying to load a setting with a key that doesn't exists!");
        return "ERROR 2";
    }
#endregion

#region Getters
    private string GetSettingStr(string key)
    {
        foreach(var item in settings)
        {
            if (key == item.Key)
            {
                return item.Value;
            }
        }
        Debug.LogError("The key '" + key + "' does not exists! \n Make sure they are added to the player settings and don't forget to save them!");
        return null;
    }
    public KeyCode GetKeyCode(string key)
    {
        string str = GetSettingStr(key);
        if (str == null)
            return KeyCode.None;

        KeyCode output;
        if (!System.Enum.TryParse(str, out output))
        {
            Debug.LogError(key + " is not an KeyCode!");
            return KeyCode.None;
        } 
        return output;
    }
    
    public float GetFloat(string key)
    {
        string str = GetSettingStr(key);
        if (str == null)
            return 0.0f;

        float output;
        if (!float.TryParse(str, out output))
        {
            Debug.LogError(key + " is not an float!");
            return 0.0f;
        } 
        return output;
    }
    public int GetInt(string key)
    {
        string str = GetSettingStr(key);
        if (str == null)
            return 0;

        int output;
        if (!System.Int32.TryParse(str, out output))
        {
            Debug.LogError(key + " is not an int!");
            return 0;
        } 
        return output;
    }
    public bool GetBool(string key)
    {
        string str = GetSettingStr(key);
        if (str == null)
            return false;

        bool output;
        if (!System.Boolean.TryParse(str, out output))
        {
            Debug.LogError(key + " is not an bool!");
            return false;
        } 
        return output;
    }
    public string GetString(string key)
    {
        string str = GetSettingStr(key);
        if (str == null)
            return null;
 
        return str;
    }
#endregion
}
public enum SettingType
{
    String,
    Int,
    Float,
    Bool,
    KeyCode
}

[System.Serializable]
public class SettingItem
{
    [XmlAttribute("key")] public string Key;
    [XmlAttribute("type")]public SettingType Type;
    [XmlAttribute("value")]public string Value;
    /* 
    public void SetKey(string key)
    {
        Key = key;
    }
    public string GetKey()
    {
        return Key;
    }
    public SettingItem(string defaultKey)
    {
        Key = defaultKey;
        Type = SettingType.Bool;
    }*/
}