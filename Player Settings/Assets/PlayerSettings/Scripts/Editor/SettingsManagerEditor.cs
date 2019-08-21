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
        const float KEY_WIDTH = 130;
        const float TYPE_WIDTH = 50;
        const float VALUE_WIDTH = 60;
        const float SMALL_BUTTON_WIDTH = 20;
        const float SPACING_WIDTH = 14;
        const float TOTAL_WIDTH = KEY_WIDTH + TYPE_WIDTH + VALUE_WIDTH + (3 * SMALL_BUTTON_WIDTH) + SPACING_WIDTH;
        const float HALF_WIDTH = TOTAL_WIDTH / 2;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SettingsManager playerSettings = (SettingsManager)target;

            if (playerSettings.settings == null)
                playerSettings.settings = new List<SettingItem>();



            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("KEY", EditorStyles.boldLabel, GUILayout.Width(KEY_WIDTH));
            EditorGUILayout.LabelField("TYPE", EditorStyles.boldLabel, GUILayout.Width(TYPE_WIDTH));
            EditorGUILayout.LabelField("VALUE", EditorStyles.boldLabel, GUILayout.Width(VALUE_WIDTH));

            EditorGUILayout.EndHorizontal();
            
            for (int i = 0; i < playerSettings.settings.Count; i++)
            {
                GUILayout.BeginHorizontal();

                if (EditorApplication.isPlaying)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(playerSettings.settings[i].Key, GUILayout.Width(KEY_WIDTH));
                    EditorGUILayout.LabelField(playerSettings.settings[i].Type.ToString(), GUILayout.Width(TYPE_WIDTH));
                    EditorGUILayout.LabelField(playerSettings.settings[i].Value, GUILayout.Width(VALUE_WIDTH));

                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    playerSettings.settings[i].Key = EditorGUILayout.TextField(playerSettings.settings[i].Key, GUILayout.Width(KEY_WIDTH));

                    playerSettings.settings[i].Type = (SettingType)EditorGUILayout.EnumPopup(playerSettings.settings[i].Type, GUILayout.Width(TYPE_WIDTH));

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

                            playerSettings.settings[i].Value = (EditorGUILayout.Toggle(parsedValue, GUILayout.Width(VALUE_WIDTH))).ToString();
                            break;
                        case SettingType.Int:
                            int parsedValue2;
                            if (!Int32.TryParse(playerSettings.settings[i].Value, out parsedValue2))
                            {
                                playerSettings.settings[i].Value = 0.ToString();
                            }

                            playerSettings.settings[i].Value = (EditorGUILayout.IntField(parsedValue2, GUILayout.Width(VALUE_WIDTH))).ToString();
                            break;
                        case SettingType.String:
                            playerSettings.settings[i].Value = EditorGUILayout.TextField(playerSettings.settings[i].Value, GUILayout.Width(VALUE_WIDTH));
                            break;
                        case SettingType.Float:
                            float parsedValue3;
                            if (!float.TryParse(playerSettings.settings[i].Value, out parsedValue3))
                            {
                                playerSettings.settings[i].Value = 0.0f.ToString();
                            }

                            playerSettings.settings[i].Value = (EditorGUILayout.FloatField(parsedValue3, GUILayout.Width(VALUE_WIDTH))).ToString();
                            break;
                        case SettingType.Button:
                            KeyCode parsedValue4;
                            if (!Enum.TryParse(playerSettings.settings[i].Value, true, out parsedValue4))
                            {
                                playerSettings.settings[i].Value = KeyCode.None.ToString();
                            }

                            playerSettings.settings[i].Value = ((KeyCode)EditorGUILayout.EnumPopup(parsedValue4, GUILayout.Width(VALUE_WIDTH))).ToString();
                            break;
                        default:
                            break;
                    }


                    if (GUILayout.Button("↑", GUILayout.Width(SMALL_BUTTON_WIDTH)))
                    {
                        var item = playerSettings.settings[i];
                        playerSettings.settings.RemoveAt(i);
                        playerSettings.settings.Insert(i - 1, item);
                    }

                    if (GUILayout.Button("↓", GUILayout.Width(SMALL_BUTTON_WIDTH)))
                    {
                        var item = playerSettings.settings[i];
                        playerSettings.settings.RemoveAt(i);
                        playerSettings.settings.Insert(i + 1, item);
                    }

                    if (GUILayout.Button("X", GUILayout.Width(SMALL_BUTTON_WIDTH)))
                    {
                        playerSettings.settings.RemoveAt(i);
                    }
                }
                GUILayout.EndHorizontal();
            }
            if (!EditorApplication.isPlaying)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("ADD", GUILayout.Width(HALF_WIDTH)))
                {
                    playerSettings.settings.Add(new SettingItem());
                }
                if (GUILayout.Button("CLEAR ALL", GUILayout.Width(HALF_WIDTH)))
                {
                    playerSettings.settings.Clear();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("SAVE", GUILayout.Width(HALF_WIDTH)))
                {
                    playerSettings.SaveSettingsEditor();
                }
                if (GUILayout.Button("LOAD", GUILayout.Width(HALF_WIDTH)))
                {
                    playerSettings.LoadSettingsEditor();
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