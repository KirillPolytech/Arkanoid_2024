using TMPro;
using UnityEngine.Localization;
using Zenject;

public class HealthView
{
    private readonly TextMeshProUGUI _healthText;
    private readonly LocalizedString _localizedString;
    private int _health;

    [Inject]
    public HealthView(TextMeshProUGUI textMeshProUGUI, LocalizedString localizedString)
    {
        _healthText = textMeshProUGUI;
        _localizedString = localizedString;

        _localizedString.Arguments = new object[] { _health };
        _localizedString.StringChanged += DrawHealth;
    }

    private void DrawHealth(string str)
    {
        _healthText.text = str;
    }

    public void DrawHealth(int health)
    {
        _health = health;
        _localizedString.Arguments[0] = _health;
        _localizedString.RefreshString();
    }
}