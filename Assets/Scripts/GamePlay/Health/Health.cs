using System;

public class Health
{
    public Action<int> OnHealthLose;

    public int CurrentHealth { get; private set; } = 3;
    
    public void LoseHealth()
    {
        if (CurrentHealth < 0)
            throw new Exception("Health below zero");

        CurrentHealth--;

        OnHealthLose?.Invoke(CurrentHealth);
    }

}
