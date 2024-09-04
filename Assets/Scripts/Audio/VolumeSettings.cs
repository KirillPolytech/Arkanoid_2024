using UnityEngine.Audio;
using Zenject;

public class VolumeSettings : IInitializable
{
    private const int Max = 20;
    private const int Min = -80;
    
    private const string Soundvol = "SoundVol";
    private const string Musicvol = "MusicVol";
    
    private readonly AudioMixer _audioMixer;
    
    public VolumeSettings(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }
    
    public void Initialize()
    {
        _audioMixer.SetFloat(Soundvol, 0);
        _audioMixer.SetFloat(Musicvol, 0);
    }

    public void ChangeSoundVol(float value) => _audioMixer.SetFloat(Soundvol, value);
    public void ChangeMusicVol(float value)=> _audioMixer.SetFloat(Musicvol, value);
}
