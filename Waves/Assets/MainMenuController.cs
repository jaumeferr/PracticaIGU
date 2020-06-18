using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject LevelsPanel;
    public GameObject SettingsPanel;

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

}
