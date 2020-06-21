using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScoreController : MonoBehaviour
{
    public Text text;
    void Start()
    {
        if(Variables.currentLevel == 2){
            GameObject.Find("NextLevelButton").SetActive(false);
        }
        text.text = Variables.lastScore.ToString();
    }
}
