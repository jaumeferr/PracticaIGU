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
    private int hourCount;

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
        hourCount = 0;

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
        //Generate Planet  
        GameObject.Find("Planet").GetComponent<Planet>().GeneratePlanet();

        //Generate Vegetation
        GameObject.Find("TerrainSeed").GetComponent<GenerateVegetation>().Generate();

        //Locate NPC
        this.SpawnNPC();

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
        GameObject.Find("Planet").GetComponent<EnemySpawner>().InitializeSpawnConfig(2, 2); // waves, enemiesPerWave, waiting time

        //Esperar 10s antes de spawnear enemigos
        Invoke("DelayedSpawn", 10.0f);

        Debug.Log("Oleadas: " + GameObject.Find("Planet").GetComponent<EnemySpawner>().waves + "\n" +
                   "Enemigos por oleada: " + GameObject.Find("Planet").GetComponent<EnemySpawner>().enemiesPerWave + "\n" +
                   "Cantidad de enemigos: " + GameObject.FindGameObjectsWithTag("Enemy").Length);

    }

    public void DelayedSpawn()
    {
        GameObject.Find("Planet").GetComponent<EnemySpawner>().SpawnEnemies();
    }

    public void SpawnNPC(){
        Vector3 spawnPos = GameObject.Find("TerrainSeed").GetComponent<GenerateVegetation>().randomVertex();
        Instantiate(NPC, spawnPos + 1 * (spawnPos - Vector3.zero).normalized,Quaternion.FromToRotation(Vector3.up, (spawnPos-Vector3.zero)));
    }
}
