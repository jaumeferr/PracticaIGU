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
        SceneManager.LoadScene("Menu");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victoria");
    }

    public void NextLvl()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_01");
    }

    public void NextLv2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_02");
    }
}
