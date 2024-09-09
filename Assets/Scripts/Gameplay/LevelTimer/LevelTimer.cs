using UnityEngine;
using Zenject;

public class LevelTimer : IFixedTickable
{
    public float MiliSec { get; private set; }
    public float Sec { get; private set; }
    public float Min { get; private set; }

    private float _time;
    private LevelTimerView _levelTimerView;

    public LevelTimer(LevelTimerView levelTimerView)
    {
        _levelTimerView = levelTimerView;
    }

    public void FixedTick()
    {
        _time += Time.fixedDeltaTime;

        MiliSec = _time % 1000;
        Sec = _time % 60;
        Min = Sec % 60;

        _levelTimerView.UpdateText(MiliSec, Sec, Min);
    }
}