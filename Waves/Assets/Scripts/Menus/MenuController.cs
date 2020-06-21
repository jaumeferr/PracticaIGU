using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject LevelsPanel;
    public GameObject SettingsPanel;

    private void Start() {
        GameObject[] scores = GameObject.FindGameObjectsWithTag("Score");
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].GetComponent<Text>().text = Variables.scores[i].ToString();
        }

        GameObject.Find("LevelsPanel").SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelsPanel.SetActive(false);
            SettingsPanel.SetActive(false);
        }
    }
    public void OpenLevelsPanel()
    {
        LevelsPanel.SetActive(true);
        for (int i = 0; i < Variables.scores.Length; i++)
        {
            SetScore(i, Variables.scores[i]);
        }
    }

    public void OpenSettingsPanel()
    {
        SettingsPanel.SetActive(true);
    }

    public void SetScore(int level, int score)
    {
        GameObject[] scores = GameObject.FindGameObjectsWithTag("Score");
        if (scores != null)
        {
            scores[level].GetComponent<Text>().text = score.ToString();
        }
    }

}