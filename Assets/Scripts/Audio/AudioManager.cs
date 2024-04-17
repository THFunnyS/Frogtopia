using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Audio Manager Component")]
public class AudioManager : SingletonPersistent<AudioManager>
{
    public static AudioSettingsModel settings = null;

    // Sounds
    public GameObject PrefabSoundBoom;

    public Action OnAudioSettingsChanged;

    protected override void Awake()
    {
        base.Awake();

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

    public void PlaySound(GameObject Prefab)
    {
        var sound = Instantiate(Prefab);
        Destroy(sound, 10f);
    }

    private WaitForSeconds OneSec = new WaitForSeconds(1);

    private IEnumerator SoundDestroy(GameObject sound)
    {
        yield return OneSec;
        sound.gameObject.SetActive(false);
    }
}
