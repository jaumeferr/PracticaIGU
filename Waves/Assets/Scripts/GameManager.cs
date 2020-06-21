using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;

    // Pantalla al morir
    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            SceneManager.LoadScene("GameOver");
        }
    }

    // Al salir de la partida nos llevará al menu principal
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Victory()
    {
        //Calcular score
        GameObject.Find("GameManager").GetComponent<ChallengeController>().CalculateScore();

        //Unlock next level if its locked
        if(Variables.unlockedLevels[Variables.currentLevel] == false){
            //Unlock level
        }
        
        SceneManager.LoadScene("Victoria");
        
    }

    public void NextLvl()
    {
        Time.timeScale = 1f;
        Variables.currentLevel = 1;
        Variables.nextLevel = 2;
        SceneManager.LoadScene("Level_01");
    }

    public void NextLv2()
    {
        Time.timeScale = 1f;
        Variables.currentLevel = 2;
        Variables.nextLevel = 3;
        SceneManager.LoadScene("Level_02");
    }

    public void GoToNextLv(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_0" + Variables.nextLevel);
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_0" + Variables.currentLevel);
    }
}
