using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class HitSoundPlayer : IFixedTickable
{
    private readonly AudioSource _audioSource;
    private readonly Settings _settings;

    private float _delay;
    private bool _canPlay;
    
    
    public HitSoundPlayer(AudioSource audioSource, Settings settings)
    {
        _audioSource = audioSource;
        _settings = settings;
    }

    public void FixedTick()
    {
        if (_delay > _settings.HitSoundDelay)
        {
            _canPlay = true;
            return;
        }
        
        _delay += Time.fixedDeltaTime;
    }

    public void Play()
    {
        if (!_canPlay)
            return;
        
        _audioSource.Play();
        
        _delay = 0;
        _canPlay = false;
    }
}
