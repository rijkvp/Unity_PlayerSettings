using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("playersettings")]
public class SettingsContainer
{
    [XmlArray("settings"), XmlArrayItem("setting")]
 	public List<SettingItem> settings = new List<SettingItem>();
    public void Save(string path)
 	{
 		var serializer = new XmlSerializer(typeof(SettingsContainer));
 		using(var stream = new FileStream(path, FileMode.Create))
 		{
 			serializer.Serialize(stream, this);
 		}
 	}
 
 	public static SettingsContainer Load(string path)
 	{
 		var serializer = new XmlSerializer(typeof(SettingsContainer));
 		using(var stream = new FileStream(path, FileMode.Open))
 		{
 			return serializer.Deserialize(stream) as SettingsContainer;
 		}
 	}
 
    public static SettingsContainer LoadFromText(string text) 
 	{
 		var serializer = new XmlSerializer(typeof(SettingsContainer));
 		return serializer.Deserialize(new StringReader(text)) as SettingsContainer;
 	}
}