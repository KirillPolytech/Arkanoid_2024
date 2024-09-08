using TMPro;
using UnityEngine;
using Zenject;

public class FPSView: ITickable
{
    private readonly TextMeshProUGUI _counter;
    
    public FPSView(TextMeshProUGUI counter)
    {
        _counter = counter;
    }
    
    public void Tick()
    {
        _counter.text = $"FPS: {(int)(1 / Time.deltaTime)}";
    }
}
