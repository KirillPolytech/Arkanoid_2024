using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class UserData
{
    private readonly LevelData[] LevelsData = new LevelData[6];
    public ControlType ControlType = ControlType.mouse;

    public IList<LevelData> GetLevelsData => LevelsData.AsReadOnlyList();

    private UserData()
    {
        //debug
        /*
        LevelsData[0].IsCompleted = true;
        LevelsData[0].Min = 59;
        LevelsData[0].Sec = 50;
        LevelsData[0].MilSec = 999;
        */
        //
    }

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
    public float Min, Sec, MilSec;
    public bool IsCompleted;

    public LevelData(float min, float sec, float milSec, bool isCompleted)
    {
        Min = min;
        Sec = sec;
        MilSec = milSec;
        IsCompleted = isCompleted;
    }
}