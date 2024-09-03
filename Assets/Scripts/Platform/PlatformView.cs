using TMPro;
using Zenject;

public class PlatformView
{
    private TextMeshProUGUI _healthText;
    
    [Inject]
    public PlatformView(TextMeshProUGUI textMeshProUGUI)
    {
        _healthText = textMeshProUGUI;
    }
    
    public void DrawHealth(int health)
    {
        _healthText.text = "" + health;
    }
}
