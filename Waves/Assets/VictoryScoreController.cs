using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScoreController : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text.text = Variables.lastScore.ToString();
    }
}
