using UnityEngine;

public class Buff : MonoBehaviour
{
    private void Update()
    {
        transform.position -= Vector3.up * Time.deltaTime;
    }
}
