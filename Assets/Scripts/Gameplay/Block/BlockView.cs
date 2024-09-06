using TMPro;

public class BlockView
{
    private TextMeshProUGUI _text;

    public BlockView(TextMeshProUGUI text)
    {
        _text = text;
    }

    public void UpdateText(int hits) => _text.text = hits.ToString();
}