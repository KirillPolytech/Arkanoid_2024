using TMPro;

public class LevelTimerView
{
    private readonly TextMeshProUGUI _timerText;

    public LevelTimerView(TextMeshProUGUI text)
    {
        _timerText = text;
    }

    public void UpdateText(float milSec, float sec, float min)
    {
        _timerText.text = $"{min}:{sec}:{milSec}";
    }
}