using TMPro;
using UnityEngine;
using Zenject;

public class FPS : ITickable
{
    private readonly TextMeshProUGUI _counter;
    
    public FPS(TextMeshProUGUI counter)
    {
        _counter = counter;
    }
    
    public void Tick()
    {
        _counter.text = $"FPS: {(int)(1 / Time.deltaTime)}";
    }
}
