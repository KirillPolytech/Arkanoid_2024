using System;
using UnityEngine;
using Zenject;

public class PlatformModel
{
    public Action<int> OnHealthLose;

    public int Health { get; private set; } = 3;

    private Rigidbody _rb;

    [Inject]
    public PlatformModel(Rigidbody rigidbody)
    {
        _rb = rigidbody;
    }

    public void AddForce(float force)
    {
        _rb.AddForce(new Vector3(force, 0, 0));
    }

    public void LoseHealth()
    {
        if (Health < 0)
            throw new Exception("Health below zero");

        Health--;

        OnHealthLose?.Invoke(Health);
    }
}