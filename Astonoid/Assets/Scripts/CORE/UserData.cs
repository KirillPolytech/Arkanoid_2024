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
        if (ind < 0 || ind > LevelsData.Length)
            throw new IndexOutOfRangeException();

        if (LevelsData[ind].Equals(default(LevelData)))
        {
            LevelsData[ind] = levelData;
            return;
        }
        
        if (LevelsData[ind].TimeData.time < levelData.TimeData.time)
            return;
        
        LevelsData[ind] = levelData;
    }
}

public struct LevelData
{
    public TimeData TimeData;

    public LevelData(TimeData data)
    {
        TimeData = data;
    }
}

public struct TimeData
{
    public float Min, Sec, MilSec;
    public float time;

    public TimeData(float min, float sec, float milSec)
    {
        Min = min;
        Sec = sec;
        MilSec = milSec;

        time = MilSec + (Sec + Min * 60) * 1000;
    }
}