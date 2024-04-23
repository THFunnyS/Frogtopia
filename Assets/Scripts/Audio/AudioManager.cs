using System;
using UnityEngine;

[AddComponentMenu("Audio/Audio Manager Component")]
public class AudioManager : SingletonPersistent<AudioManager>
{
    public static AudioSettingsModel settings = null;

    public static float LimitTimeSound = 10f;

    [Header("Sound")]
    public GameObject SoundButton;
    public GameObject PalyerGrassWalk;


    public Action OnAudioSettingsChanged;

    protected override void Awake()
    {
        base.Awake();

        InitializeManager();
    }

    private void InitializeManager()
    {
        if (settings == null)
            settings = new AudioSettingsModel();

        // loadSettings();
    }

    public void loadSettings()
    {
        settings.music = System.Convert.ToBoolean(PlayerPrefs.GetString("music", "true"));
        settings.sounds = System.Convert.ToBoolean(PlayerPrefs.GetString("sounds", "true"));
    }
    
    public void saveSettings()
    {
        PlayerPrefs.SetString("music", settings.music.ToString());
        PlayerPrefs.SetString("sounds", settings.sounds.ToString());
        PlayerPrefs.Save();
    }

    public void toggleSounds(bool enabled)
    {
        settings.sounds = enabled;
        saveSettings();

        if (OnAudioSettingsChanged != null)
            OnAudioSettingsChanged();
    }

    public void toggleMusic(bool enabled)
    {
        settings.music = enabled;
        saveSettings();

        if (OnAudioSettingsChanged != null)
            OnAudioSettingsChanged();
    }

    public static void PlaySound(GameObject Sound)
    {
        GameObject sound = Instantiate(Sound);
        Destroy(sound, LimitTimeSound);
    }

    public static GameObject PlaySoundLoop(GameObject Sound)
    {
        return Instantiate(Sound);
    }
}