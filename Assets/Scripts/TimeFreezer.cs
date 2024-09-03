using UnityEngine;

public class TimeFreezer
{
    public void Freeze()
    {
        Time.timeScale = 0;
    }
    
    public void UnFreeze()
    {
        Time.timeScale = 1;
    }
}
