using TMPro;

public class FPSView
{
    private readonly TextMeshProUGUI _counter;
    
    public FPSView(TextMeshProUGUI counter)
    {
        _counter = counter;
    }

    public void UpdateFPSValue(float value)
    {
        _counter.text = $"FPS: {value}";
    }
}
