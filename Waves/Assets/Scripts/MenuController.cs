using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject LevelsPanel;
    public GameObject SettingsPanel;

    private void Start() {
        
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            LevelsPanel.SetActive(false);
            SettingsPanel.SetActive(false);
        }
    }
    public void OpenLevelsPanel(){
        LevelsPanel.SetActive(true);
    }

    public void OpenSettingsPanel(){
        SettingsPanel.SetActive(true);
    }

    public void SetScore(int level, int score){
        GameObject[] scores = GameObject.FindGameObjectsWithTag("Scores");
        scores[level-1].GetComponent<Text>().text = score.ToString();
    }

}