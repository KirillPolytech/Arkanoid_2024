public class LayerStorage
{
    public static LayerData BlockLayer = new LayerData(8, "Block");
}

public struct LayerData
{
    public readonly int Layer;
    public readonly string Name;

    public LayerData(int layer, string name)
    {
        Layer = layer;
        Name = name;
    }
}
