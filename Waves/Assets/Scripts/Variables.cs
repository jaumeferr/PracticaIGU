using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variables
{
    public const int numLevels = 2;
    public static int currentLevel = 1;
    public static int nextLevel = 2;
    public static bool[] unlockedLevels = new bool[numLevels];
    public static int[] scores = new int[numLevels];
    public static float maxDamageDelay = 3f;

    //level 2
    public static bool playerHasPaper = false;


}
