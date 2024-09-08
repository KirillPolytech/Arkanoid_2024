using NaughtyAttributes;
using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private Transform[] blocks;
    [SerializeField] private Transform start;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private int blockInRow;

    [SerializeField] private int rombRow = 9;

    [Button]
    public void PlaceMatrix()
    {
        int ind = 0;
        for (int i = 0; i < blockInRow; i++)
        {
            if (ind > blocks.Length)
                Debug.LogWarning("Ind out of range");
            
            for (int j = 0; j < blockInRow ; j++)
            {
                blocks[ind++].transform.position =
                    new Vector3(start.position.x + offsetX * j, start.position.y + offsetY * i, 0);
            }
        }
    }
    
    [Button]
    public void PlaceRhomb()
    {
        foreach (var block in blocks)
        {
            block.transform.position = Vector3.zero;
        }
        
        int ind = 0;
        int halfY = rombRow;
        int z = 0;
        for (int i = 0; i < halfY * 2; i++)
        {
            if (i == halfY)
            {
                z = halfY;
            }

            if (i > halfY)
            {
                z -= 1;
            }
            else if (i != halfY)
            {
                z += 1;
            }
            
            for (int j = Mathf.Clamp(halfY - z, 0, halfY); 
                 j < Mathf.Clamp(blockInRow - halfY + z, 0, blockInRow) 
                 && ind < blocks.Length; j++)   
            {
                blocks[ind++].transform.position =
                    new Vector3(start.position.x + offsetX * j, start.position.y + offsetY * i, 0);
            }
        }

        for (int i = ind; i < blocks.Length; i++)
        {
            blocks[i].gameObject.SetActive(false);
        }
    }
}