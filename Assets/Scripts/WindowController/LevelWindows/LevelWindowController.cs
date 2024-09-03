using System;

public class LevelWindowController : WindowController
{
    public override void Open<T>()
    {
        Window window = null;
        
        foreach (var win in windows)
        {
            if (win.GetType() != typeof(T))
            {
                win.Close();
                continue;
            }
            
            window = win;
        }
        
        if (!window)
            throw new Exception("Unknown window");

        if (CurrentWindow)
            CurrentWindow.Close();

        CurrentWindow = window;

        CurrentWindow.Open();
    }
}