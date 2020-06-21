using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject LevelsPanel;
    public GameObject SettingsPanel;
    public GameObject audio;

    private void Start()
    {
        LevelsPanel.SetActive(true);
        SettingsPanel.SetActive(true);

        //Set scores
        GameObject[] scores = GameObject.FindGameObjectsWithTag("Score");
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].GetComponent<Text>().text = Variables.scores[i].ToString();
        }

        //Settings
        GameObject.Find("SoundButton").GetComponent<Toggle>().isOn = Variables.soundOn;
        GameObject.Find("GenerateGrassButton").GetComponent<Toggle>().isOn = Variables.generateGrass;

        //Locked levels
        for (int i = 0; i < Variables.unlockedLevels.Length - 1; i++)
        {
            if (Variables.unlockedLevels[i] == false)
            {
                GameObject.Find("Level" + (i + 1) + "PlayButton").SetActive(false);
                GameObject.Find("Level" + (i + 1) + "Score").SetActive(false);
            }
        }

        LevelsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelsPanel.SetActive(false);
            SettingsPanel.SetActive(false);
        }

        if (Variables.soundOn)
        {
            audio.SetActive(true);
        }
        else
        {
            audio.SetActive(false);
        }
    }
    public void OpenLevelsPanel()
    {
        SettingsPanel.SetActive(false);
        LevelsPanel.SetActive(true);
    }

    public void OpenSettingsPanel()
    {
        LevelsPanel.SetActive(false);
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

    public void onGenerateGrassClick()
    {
        Variables.generateGrass = GameObject.Find("GenerateGrassButton").GetComponent<Toggle>().isOn;
    }

    public void onSoundClick()
    {
        Variables.soundOn = GameObject.Find("SoundButton").GetComponent<Toggle>().isOn;
    }

}