using System;
using UnityEngine;

[AddComponentMenu("Audio/Audio Manager Component")]
public class AudioManager : SingletonPersistent<AudioManager>
{
    public static AudioSettingsModel settings = null;

    public static float LimitTimeSound = 10f;

    [Header("Sound")]
    public GameObject SoundButton;
    public GameObject EagleAttack;
    public GameObject PlayerDamage;
    public GameObject TongueAttack;
    public GameObject Dash;
    public GameObject Jump;
    public GameObject Landing;
    public GameObject WeaponAttack;
    public GameObject StepSound;


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

        loadSettings();
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
    
    public void toggleSounds()
    {
        settings.sounds = !settings.sounds;
        saveSettings();
        if (OnAudioSettingsChanged != null)
            OnAudioSettingsChanged();
    }
    
    public void toggleMusic()
    {
        settings.music = !settings.music;
        saveSettings();
        if (OnAudioSettingsChanged != null)
            OnAudioSettingsChanged();
    }

    public static void PlaySound(GameObject Sound)
    {
        GameObject sound = Instantiate(Sound);
        Destroy(sound, LimitTimeSound);
    }
}
