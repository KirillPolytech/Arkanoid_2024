using System;

public class HealthPresenter
{
    public Action OnLose;

    private Health _health;
    private HealthView _healthView;

    public HealthPresenter(Health health, HealthView healthView)
    {
        _health = health;
        _healthView = healthView;
    }
    
    private void UpdateHealth(int health)
    {
        if (health > 0)
            return;

        OnLose?.Invoke();
    }
}
