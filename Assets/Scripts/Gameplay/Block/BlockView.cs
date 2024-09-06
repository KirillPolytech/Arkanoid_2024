using TMPro;
using UnityEngine;

public class BlockView
{
    private readonly TextMeshProUGUI _text;

    public BlockView(TextMeshProUGUI text)
    {
        _text = text;
    }

    public void UpdateText(int hits)
    {
        float value = 1f / (hits + 1) + 0.2f;
        _text.color = new Color(value, value, 1);
        _text.text = hits.ToString();
    }
}