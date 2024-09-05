using NaughtyAttributes;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private Transform[] blocks;
    [SerializeField] private Transform start;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private int blockInRow;

    [Button]
    public void PlaceMatrix()
    {
        int ind = 0;
        for (int i = 0; i < blockInRow; i++)
        {
            for (int j = 0; j < blockInRow && ind < blocks.Length; j++)
            {
                blocks[ind++].transform.position =
                    new Vector3(start.position.x + offsetX * j, start.position.y + offsetY * i, 0);
            }
        }
    }
}