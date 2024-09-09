using UnityEngine;
using Zenject;

public class LevelTimer : IFixedTickable
{
    private readonly LevelTimerView _levelTimerView;
    
    public float MiliSec { get; private set; }
    public float Sec { get; private set; }
    public float Min { get; private set; }

    private float _time;

    public LevelTimer(LevelTimerView levelTimerView)
    {
        _levelTimerView = levelTimerView;
    }

    public void FixedTick()
    {
        _time += Time.fixedDeltaTime;

        MiliSec = (int)(_time % 1 * 1000);
        Sec = (int)(_time % 60);
        Min = (int)(Sec / 60 % 60);

        _levelTimerView.UpdateText(MiliSec, Sec, Min);
    }

    public void Reset()
    {
        
    }
}