using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChallengeController : MonoBehaviour
{
    public void Start()
    {
        string level_name = SceneManager.GetActiveScene().name;

        switch (level_name)
        {
            case "Level_01":
                break;

            case "Level_02":
                Initialize_Level_02();
                break;


        }
    }

    public void Initialize_Level_01()
    {
        //Generate Planet  
        GameObject.Find("Planet").GetComponent<Planet>().GeneratePlanet();

        //Generate Vegetation
        GameObject.Find("TerrainSeed").GetComponent<GenerateVegetation>().Generate();

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

        //Generate Enemies
        GameObject.Find("Planet").GetComponent<EnemySpawner>().InitializeSpawnConfig(3, 15);

        //Spawn enemies
        GameObject.Find("Planet").GetComponent<EnemySpawner>().SpawnEnemies();

    }

}
