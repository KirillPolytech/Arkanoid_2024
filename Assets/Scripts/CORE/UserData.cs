using System;

public class UserData
{
    private LevelData[] LevelsData = new LevelData[6];
    public ControlType ControlType = ControlType.mouse;

    public void ChangeLevelData(int ind, LevelData levelData)
    {
        if (LevelsData[ind].IsCompleted)
            return;
        
        if (ind < 0 || ind > LevelsData.Length)
            throw new IndexOutOfRangeException();

        LevelsData[ind] = levelData;
    }
}

public struct LevelData
{
    public float Time;
    public bool IsCompleted;
}

public enum ControlType{keyboard, mouse}