using System;
using Arkanoid.Settings;
using Zenject;

public class Health
{
    public int CurrentHealth { get; private set; }

    private readonly Settings _settings;

    [Inject]
    public Health(Settings settings)
    {
        _settings = settings;

        Reset();
    }

    public void LoseHealth()
    {
        if (CurrentHealth < 0)
            throw new Exception("Health below zero");

        CurrentHealth--;
    }

    public void Reset()
    {
        CurrentHealth = _settings.Health;
    }
}