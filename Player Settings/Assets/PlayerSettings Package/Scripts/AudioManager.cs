using UnityEngine;
using UnityEngine.Audio;

namespace PlayerSettings
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton

        public static AudioManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("Multiple instances of AudioManager!");
        }
        
        #endregion

        [Header("Volume Keys")]
        public string volumeKey = "volume";

        [Header("Audio Mixer")]
        public AudioMixer audioMixer;
        public string audioMixerVolumeKey = "volume";

        private static int volume = 100;
                   
        private void Start()
        {
            LoadAudioSettings();
        }

        public static void LoadAudioSettings()
        {
            volume = SettingsManager.GetInt(instance.volumeKey);

            float actualVolume = volume - 80;
            instance.audioMixer.SetFloat(instance.audioMixerVolumeKey, actualVolume);
        }

        public static int GetVolume()
        {
            return volume;
        }
    }
}
