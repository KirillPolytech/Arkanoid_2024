using TMPro;
using UnityEngine;

public class BlockView
{
    private readonly TextMeshProUGUI _text;
    private readonly Renderer _renderer;

    public BlockView(Renderer renderer, TextMeshProUGUI text)
    {
        _text = text;
        _renderer = renderer;
    }

    public void UpdateText(int hits)
    {
        _text.text = hits.ToString();

        UpdateMeshColor(hits);
    }

    private void UpdateMeshColor(int hits)
    {
        float r = 1f / (hits + 1); 
        float g = 1 - 1f / (hits + 1); 
        _renderer.material.color = new Color(r, g, 1);
    }
}