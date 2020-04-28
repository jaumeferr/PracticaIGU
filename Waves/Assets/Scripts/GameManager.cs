﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;

    // Empezar la partida
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); //DUMBASS NAME para Level01   
    }

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
}