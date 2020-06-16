using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    public Transform prefabEnemy;
    private GameObject planet;
    private GameObject player;

    private Vector3 planetCenter;
    private float radius;
    public int waves;
    public int currentWave;
    public int enemiesPerWave;
    public GameObject[] currentWaveEnemies;

    public void InitializeSpawnConfig(int waves, int enemiesPerWave)
    {
        this.planet = GameObject.Find("Planet");
        this.player = GameObject.Find("Player");

        this.planetCenter = planet.GetComponent<Transform>().position;
        this.radius = 41.0f;

        this.waves = waves;
        this.enemiesPerWave = enemiesPerWave;
        this.currentWaveEnemies = new GameObject[enemiesPerWave];
        this.currentWave = 0;
    }

    public void SpawnEnemies()
    {
        Rigidbody planetRb = planet.GetComponent<Rigidbody>();
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        int i = 0;

        //Spawnear enemigos
        while (i < enemiesPerWave)
        {
            //Generar angulo alfa/beta aleatorio
            float b = Random.Range(0.0f, 2 * Mathf.PI);
            float a = Random.Range((-1) * Mathf.PI / 2, Mathf.PI / 2);

            float x = Mathf.Cos(b) * Mathf.Cos(a);
            float y = Mathf.Cos(b) * Mathf.Sin(a);
            float z = Mathf.Sin(b);
            //Determinar x,y,z 
            Vector3 dir = new Vector3(x, y, z);

            Vector3 spawnPoint = dir.normalized * 50;

            if ((spawnPoint - playerRb.position).magnitude > 2)
            {
                currentWaveEnemies[i] = Instantiate(prefabEnemy, spawnPoint, Quaternion.identity).gameObject;
                i++;
            }
        }

        currentWave++;
    }
}