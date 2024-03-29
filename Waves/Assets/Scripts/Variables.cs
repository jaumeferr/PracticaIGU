﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variables
{
    public const int numLevels = 2;
    public static int currentLevel = 1;
    public static int nextLevel = 2;
    public static bool[] unlockedLevels = new bool[] {true, false, false};
    public static int[] scores = new int[] {0, 0};
    public static int lastScore = 0;
    public static float maxDamageDelay = 3f;
    public static int maxPlayerLifes = 5;
    public static int maxNPCLifes = 10;
    public static bool generateGrass = false;
    public static bool soundOn = true;

    //level 2
    public static bool playerHasPaper = false;


}
