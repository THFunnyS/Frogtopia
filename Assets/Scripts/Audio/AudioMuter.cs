using UnityEngine;

[RequireComponent(typeof(AudioSource)), AddComponentMenu("Audio/Audio Muter Component")]
public class AudioMuter : MonoBehaviour
{
    [SerializeField] private bool isMusic = false;

    private float _baseVolume;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _baseVolume = _audioSource.volume;
    }

    private void Start()
    {
        AudioManager.inst.OnAudioSettingsChanged += _audioSettingsChanged;

        _audioSettingsChanged();
    }
    private void _audioSettingsChanged()
    {
        if (isMusic)
            _audioSource.volume = (AudioManager.settings.music) ? _baseVolume : 0F;
        if (!isMusic)
            _audioSource.volume = (AudioManager.settings.sounds) ? _baseVolume : 0F;
    }

    private void OnDestroy()
    {
        AudioManager.inst.OnAudioSettingsChanged -= _audioSettingsChanged;
    }

}
