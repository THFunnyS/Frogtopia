using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource)), AddComponentMenu("Audio/Audio Muter Component")]
public class AudioMuter : MonoBehaviour
{
    public bool isMusic = false;

    private AudioSource _audioSource;
    private float _baseVolume = 1f;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _baseVolume = _audioSource.volume;

        AudioManager.inst.OnAudioSettingsChanged += _audioSettingsChanged;

        _audioSettingsChanged();
    }

    private void OnDestroy()
    {
        AudioManager.inst.OnAudioSettingsChanged -= _audioSettingsChanged;
    }

    private void _audioSettingsChanged()
    {
        if (isMusic)
            _audioSource.volume = (AudioManager.settings.music) ? _baseVolume : 0F;
        if (!isMusic)
            _audioSource.volume = (AudioManager.settings.sounds) ? _baseVolume : 0F;
    }
}