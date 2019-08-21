using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

namespace PlayerSettings
{
    // Interface for all UI element setting components
    public interface ISettingUIElement
    {
        void Load();
    }

    public enum SettingType { String, Int, Float, Bool, Button }

    [System.Serializable]
    public class SettingItem
    {
        [XmlAttribute("key")] public string Key;
        [XmlAttribute("type")] public SettingType Type;
        [XmlAttribute("value")] public string Value;
    }

    [XmlRoot("playersettings")]
    public class SettingsContainer
    {
        [XmlArray("settings"), XmlArrayItem("setting")]
        public List<SettingItem> settings = new List<SettingItem>();

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(SettingsContainer));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }

        public static SettingsContainer Load(string path)
        {
            var serializer = new XmlSerializer(typeof(SettingsContainer));

            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    return serializer.Deserialize(stream) as SettingsContainer;
                }
            }
            catch(FileNotFoundException exception)
            {
                Debug.Log(exception.FileName + " was not found! Saving settings for the first time..");
                SettingsManager.Save();                
                return Load(path);
            }
            catch (Exception e)
            {
                Debug.Log("ERROR While loading playersettings: " + e.Message);
                return null;
            }
        }

        public static SettingsContainer LoadFromText(string text)
        {
            var serializer = new XmlSerializer(typeof(SettingsContainer));
            return serializer.Deserialize(new StringReader(text)) as SettingsContainer;
        }
    }

    public class SettingsManager : MonoBehaviour
    {
        [HideInInspector] public List<SettingItem> settings = new List<SettingItem>();

        private static SettingsManager instance;

        private const string FILE_NAME = "playersettings.xml";

        private List<ISettingUIElement> uiElements = new List<ISettingUIElement>();

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Debug.LogError("More then one instance of PlayerSettings!");
                Destroy(this);
            }
        }

        void Start()
        {
            LoadSettings();    
        }

        public static void RegisterUIElement(ISettingUIElement element)
        {
            if (instance)
                instance.uiElements.Add(element);
            else
                Debug.LogError("There is no instance of SettingsManager created yet! " +
                    "Make sure to set the SettingsManager at the top of the execution order! (before the default time)");
        }

        public static void Load()
        {
            instance.LoadSettings();
        }

        public static void Save()
        {
            instance.SaveSettings();
        }


        public void LoadSettings()
        {
            string path = Application.persistentDataPath + "/" + FILE_NAME;
            SettingsContainer loadData = SettingsContainer.Load(path);
            settings = loadData.settings;

            // Display on the UI
            foreach(var element in uiElements)
            {
                element.Load();
            }
        }

        public void SaveSettings()
        {
            string path = Application.persistentDataPath + "/" + FILE_NAME;
            SettingsContainer saveData = new SettingsContainer();
            saveData.settings = settings;
            saveData.Save(path);

            if (VideoSettingsManager.instance)
                VideoSettingsManager.instance.LoadVideoSettings();

            if (AudioManager.instance)
                AudioManager.LoadAudioSettings();

            if (InputManager.instance)
                InputManager.LoadInputSettings();
        }

        public void LoadSettingsEditor()
        {
            string path = Application.persistentDataPath + "/" + FILE_NAME;
            SettingsContainer loadData = SettingsContainer.Load(path);
            settings = loadData.settings;
        }

        public void SaveSettingsEditor()
        {
            string path = Application.persistentDataPath + "/" + FILE_NAME;
            SettingsContainer saveData = new SettingsContainer();
            saveData.settings = settings;
            saveData.Save(path);
        }

        public static void SetSetting(string key, SettingType type, string value)
        {
            foreach (var item in instance.settings)
            {
                if (key == item.Key)
                {
                    if (type != item.Type)
                    {
                        Debug.LogError("The types " + item.Type.ToString() + " and " + type.ToString() + " of the setting key " + key + " are not the same!");
                        return;
                    }
                    item.Value = value;
                    return;
                }
            }

            Debug.LogError("The setting key '" + key + "' doesn't exists!");
        }

        public static string GetSetting(string key, SettingType type)
        {
            foreach (var item in instance.settings)
            {
                if (key == item.Key)
                {
                    if (type != item.Type)
                    {
                        Debug.LogError("The types " + item.Type.ToString() + " and " + type.ToString() +" of the setting key " + key +" are not the same!");
                        return "ERROR";
                    }
                    return item.Value;
                }
            }

            Debug.LogError("The setting key '" + key + "' doesn't exists!");
            return "ERROR";
        }



        private static string GetSettingStr(string key)
        {
            foreach (var item in instance.settings)
            {
                if (key == item.Key)
                {
                    return item.Value;
                }
            }
            Debug.LogError("The key '" + key + "' does not exists! \n Make sure they are added to the player settings and don't forget to save them!");
            return null;
        }

        public static KeyCode GetKeyCode(string key)
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

        public static float GetFloat(string key)
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

        public static int GetInt(string key)
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

        public static bool GetBool(string key)
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

        public static string GetString(string key)
        {
            string str = GetSettingStr(key);
            if (str == null)
                return null;

            return str;
        }
    }
}