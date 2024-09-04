using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    private void Update()
    {
        transform.position -= Vector3.up * Time.deltaTime;
    }

    public abstract void Execute();
}
