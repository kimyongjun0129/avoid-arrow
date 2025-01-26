using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]  // Á÷·ÄÈ­
public class GameData
{
    public int NowLevel_Game = 0;
    public int UnLockLevel_Game = 0;

    public int Level_1_Score = 0;
    public int Level_2_Score = 0;
    public int Level_3_Score = 0;
    public int Level_4_Score = 0;
    public int Level_5_Score = 0;

    public int Level_Score_Sum = 0;

    public List<int> Level_Score_UnLock = new List<int>() { 0, 100, 150, 150, 150, 0 };
}

