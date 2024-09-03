using UnityEngine;

public abstract class WindowController : MonoBehaviour
{
    [SerializeField] protected Window[] windows;
    
    public Window CurrentWindow { get; protected set; }
    
    public abstract void Open<T>() where T : Window;
}
