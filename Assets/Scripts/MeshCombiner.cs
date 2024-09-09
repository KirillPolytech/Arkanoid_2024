using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    private CombineInstance[] _combine;
    private int ind;

    void Start()
    {
        _combine = new CombineInstance[100];

        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        _combine[ind++].mesh = meshFilter.sharedMesh;
        _combine[ind++].transform = meshFilter.transform.localToWorldMatrix;

    }
}
