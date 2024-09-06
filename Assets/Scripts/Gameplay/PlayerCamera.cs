using UnityEngine;

public class PlayerCamera
{
    public Camera Cam { get; private set; }
    
    public PlayerCamera(Camera camera)
    {
        Cam = camera;
    }
}
