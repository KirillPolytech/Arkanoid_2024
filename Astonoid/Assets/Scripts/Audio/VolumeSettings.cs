using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class VolumeSettings : IInitializable
{
    public const int Max = 20;
    public const int Min = -80;

    private const string Soundvol = "SoundVol";
    private const string Musicvol = "MusicVol";

    private readonly AudioMixer _audioMixer;

    public float CurrentMusicVolume
    {
        get
        {
            _audioMixer.GetFloat(Musicvol, out _currentVolume);
            return _currentVolume;
        }
    }
    
    public float CurrentSoundVolume
    {
        get
        {
            _audioMixer.GetFloat(Soundvol, out _currentVolume);
            return _currentVolume;
        }
    }

    private float _currentVolume;

    public VolumeSettings(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public void Initialize()
    {
        _audioMixer.SetFloat(Soundvol, -15);
        _audioMixer.SetFloat(Musicvol, -15);
    }

    public void ChangeSoundVol(float value) => _audioMixer.SetFloat(Soundvol, Mathf.Clamp(value, Min, Max));
    public void ChangeMusicVol(float value) => _audioMixer.SetFloat(Musicvol, Mathf.Clamp(value, Min, Max));
}