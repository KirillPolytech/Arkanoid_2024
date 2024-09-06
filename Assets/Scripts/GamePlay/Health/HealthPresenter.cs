using System;
using Zenject;

public class HealthPresenter : IInitializable
{
    private readonly Health _health;
    private readonly HealthView _healthView;
    
    public Action<int> OnHealthLose;

    public HealthPresenter(Health health, HealthView healthView)
    {
        _health = health;
        _healthView = healthView;
    }
    
    public void Initialize()
    {
        _healthView.DrawHealth(_health.CurrentHealth);
    }

    public void LoseHealth()
    {
        _health.LoseHealth();
        
        _healthView.DrawHealth(_health.CurrentHealth);
        
        OnHealthLose?.Invoke(_health.CurrentHealth);
    }

    public void Reset()
    {
        _health.Reset();
        _healthView.DrawHealth(_health.CurrentHealth);
    }
}
