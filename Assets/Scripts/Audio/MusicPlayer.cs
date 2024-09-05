using System.Collections;
using UnityEngine;
using Zenject;

public class MusicPlayer : MonoBehaviour
{
    private const float Speed = 0.5f;
    
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip gameplayClip;

    private VolumeSettings _volumeSettings;
    private float _lastVolume;

    [Inject]
    public void Construct(VolumeSettings volumeSettings)
    {
        _volumeSettings = volumeSettings;
    }

    public void ChangeAudioClip(string sceneName)
    {
        switch (sceneName)
        {
            case SceneNameStorage.MenuName:
                StartCoroutine(SmoothChangeClip(menuClip));
                break;
            default:
                StartCoroutine(SmoothChangeClip(gameplayClip));
                break;
        }
    }

    private IEnumerator SmoothChangeClip(AudioClip clip)
    {
        _lastVolume = _volumeSettings.CurrentMusicVolume;
        
        while (_volumeSettings.CurrentMusicVolume > VolumeSettings.Min + VolumeSettings.Max)
        {
            _volumeSettings.ChangeMusicVol(_volumeSettings.CurrentMusicVolume - Speed);
            yield return new WaitForFixedUpdate();
        }

        audioSource.clip = clip;

        audioSource.Play();

        while (_volumeSettings.CurrentMusicVolume < _lastVolume)
        {
            _volumeSettings.ChangeMusicVol(_volumeSettings.CurrentMusicVolume + Speed);
            yield return new WaitForFixedUpdate();
        }
    }
}