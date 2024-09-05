using System;
using Arkanoid.Settings;
using Zenject;

public class Health
{
    public int CurrentHealth { get; private set; }

    [Inject]
    public Health(Settings settings)
    {
        CurrentHealth = settings.Health;
    }

    public void LoseHealth()
    {
        if (CurrentHealth < 0)
            throw new Exception("Health below zero");

        CurrentHealth--;
    }
}