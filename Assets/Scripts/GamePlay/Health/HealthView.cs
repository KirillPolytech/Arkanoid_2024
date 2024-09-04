using TMPro;
using Zenject;

public class HealthView
{
    private readonly TextMeshProUGUI _healthText;
    
    [Inject]
    public HealthView(TextMeshProUGUI textMeshProUGUI)
    {
        _healthText = textMeshProUGUI;
    }
    
    public void DrawHealth(int health)
    {
        _healthText.text = "" + health;
    }
}
