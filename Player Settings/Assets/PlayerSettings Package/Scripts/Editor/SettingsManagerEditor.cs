#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

namespace PlayerSettings
{
    [CustomEditor(typeof(SettingsManager))]
    public class SettingsManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SettingsManager playerSettings = (SettingsManager)target;

            if (playerSettings.settings == null)
                playerSettings.settings = new List<SettingItem>();

            if (EditorApplication.isPlaying)
                EditorGUILayout.LabelField("[PLAY MODE] Settings (Key/Type/Value)", EditorStyles.boldLabel);
            else
            {
                EditorGUILayout.LabelField("Settings (Key/Type/DefaultValue)", EditorStyles.boldLabel);
            }


            for (int i = 0; i < playerSettings.settings.Count; i++)
            {
                GUILayout.BeginHorizontal();

                if (EditorApplication.isPlaying)
                {
                    EditorGUILayout.LabelField(playerSettings.settings[i].Key + "      " + playerSettings.settings[i].Type.ToString() + "     " + playerSettings.settings[i].Value);
                }
                else
                {
                    playerSettings.settings[i].Key = EditorGUILayout.TextField(playerSettings.settings[i].Key);

                    playerSettings.settings[i].Type = (SettingType)EditorGUILayout.EnumPopup(playerSettings.settings[i].Type);

                    switch (playerSettings.settings[i].Type)
                    {
                        case SettingType.Bool:
                            if (playerSettings.settings[i].Value == "")
                                playerSettings.settings[i].Value = false.ToString();
                            bool parsedValue;
                            if (!Boolean.TryParse(playerSettings.settings[i].Value, out parsedValue))
                            {
                                playerSettings.settings[i].Value = false.ToString();
                            }

                            playerSettings.settings[i].Value = (EditorGUILayout.Toggle(parsedValue)).ToString();
                            break;
                        case SettingType.Int:
                            int parsedValue2;
                            if (!Int32.TryParse(playerSettings.settings[i].Value, out parsedValue2))
                            {
                                playerSettings.settings[i].Value = 0.ToString();
                            }

                            playerSettings.settings[i].Value = (EditorGUILayout.IntField(parsedValue2)).ToString();
                            break;
                        case SettingType.String:
                            playerSettings.settings[i].Value = EditorGUILayout.TextField(playerSettings.settings[i].Value);
                            break;
                        case SettingType.Float:
                            float parsedValue3;
                            if (!float.TryParse(playerSettings.settings[i].Value, out parsedValue3))
                            {
                                playerSettings.settings[i].Value = 0.0f.ToString();
                            }

                            playerSettings.settings[i].Value = (EditorGUILayout.FloatField(parsedValue3)).ToString();
                            break;
                        case SettingType.Button:
                            KeyCode parsedValue4;
                            if (!Enum.TryParse(playerSettings.settings[i].Value, true, out parsedValue4))
                            {
                                playerSettings.settings[i].Value = KeyCode.None.ToString();
                            }

                            playerSettings.settings[i].Value = ((KeyCode)EditorGUILayout.EnumPopup(parsedValue4)).ToString();
                            break;
                        default:
                            break;
                    }


                    if (GUILayout.Button("X"))
                    {
                        playerSettings.settings.RemoveAt(i);
                    }
                }
                GUILayout.EndHorizontal();
            }
            if (!EditorApplication.isPlaying)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("ADD"))
                {
                    playerSettings.settings.Add(new SettingItem());
                }
                if (GUILayout.Button("CLEAR ALL"))
                {
                    playerSettings.settings.Clear();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("SAVE"))
                {
                    playerSettings.Save();
                }
                if (GUILayout.Button("LOAD"))
                {
                    playerSettings.Load();
                }
                GUILayout.EndHorizontal();
            }


            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
    }
}

#endif