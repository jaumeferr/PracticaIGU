using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class ChallengeController : MonoBehaviour
{
    public UnityEngine.UI.Text timerText;
    private float secondsCount;
    private int minuteCount;
    public Transform NPC;

    public void Start()
    {
        string level_name = SceneManager.GetActiveScene().name;

        switch (level_name)
        {
            case "Level_01":
                Initialize_Level_01();
                break;

            case "Level_02":
                Initialize_Level_02();
                break;


        }

        secondsCount = 0;
        minuteCount = 0;
    }

    void Update()
    {
        UpdateTimerUI();
    }

    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount + "' " + (float)(Math.Truncate((double)secondsCount * 100.0) / 100.0) + "''";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }

    public void Initialize_Level_01()
    {
        Variables.currentLevel = 1;
        Variables.nextLevel = Variables.currentLevel + 1;
        //Generate Planet  
        GameObject.Find("Planet").GetComponent<Planet>().GeneratePlanet();

        //Generate Vegetation
        GameObject.Find("TerrainSeed").GetComponent<GenerateVegetation>().Generate();

        //Clear Scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        //Generate Enemies
        GameObject.Find("Planet").GetComponent<EnemySpawner>().InitializeSpawnConfig(1, 40);

        //Spawn enemies
        GameObject.Find("Planet").GetComponent<EnemySpawner>().SpawnEnemies();
    }

    public void Initialize_Level_02()
    {
        Variables.currentLevel = 2;
        Variables.nextLevel = Variables.currentLevel + 1;

        //Generate Planet  
        GameObject.Find("Planet").GetComponent<Planet>().GeneratePlanet();

        //Generate Vegetation
        GameObject.Find("TerrainSeed").GetComponent<GenerateVegetation>().Generate();

        //Locate NPC
        //this.SpawnNPC();

        //Clear Scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        //Initialize Spawner
        GameObject.Find("Planet").GetComponent<EnemySpawner>().InitializeSpawnConfig(5, 5); // waves, enemiesPerWave, waiting time

        //Esperar 10s antes de spawnear enemigos
        Invoke("DelayedSpawn", 20.0f);

        Debug.Log("Oleadas: " + GameObject.Find("Planet").GetComponent<EnemySpawner>().waves + "\n" +
                   "Enemigos por oleada: " + GameObject.Find("Planet").GetComponent<EnemySpawner>().enemiesPerWave + "\n" +
                   "Cantidad de enemigos: " + GameObject.FindGameObjectsWithTag("Enemy").Length);

    }

    public void DelayedSpawn()
    {
        GameObject.Find("Planet").GetComponent<EnemySpawner>().SpawnEnemies();
    }

    public void SpawnNPC()
    {
        Vector3 spawnPos = GameObject.Find("TerrainSeed").GetComponent<GenerateVegetation>().randomVertex();
        Instantiate(NPC, spawnPos + 1 * (spawnPos - Vector3.zero).normalized, Quaternion.FromToRotation(Vector3.up, (spawnPos - Vector3.zero)));


    }

    public void CalculateScore()
    {
        float score = 0;

        //Scores level 2
        if (SceneManager.GetActiveScene().name == "Level_02")
        {
            float timer = this.secondsCount + 60 * this.minuteCount;

            if (timer < 40)
            {
                score = 100;
            }

            if (timer >= 40 && timer <= 140)
            {
                //F(x)= -1'4x + 199 --> X1=70,Y1=100 / X2=140,Y2=1
                score = -1.4f * timer + 199; 
            }

            if (timer > 140)
            {
                score = 1;
            }

            if(score > Variables.scores[Variables.currentLevel -1]){
                Variables.scores[Variables.currentLevel - 1] = (int)Math.Round(score);
            }
        }

        Variables.lastScore = (int)Math.Round(score);
    }
}
