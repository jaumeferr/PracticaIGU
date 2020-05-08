using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ScriptableObject
{
    public int count;

    public Transform prefabEnemy;
    private GameObject planet;
    private GameObject player;

    private Vector3 planetCenter;
    private float radius;

    // Start is called before the first frame update
    public EnemySpawner(GameObject planet, GameObject player)
    {
        this.planet = planet;
        this.player = player;

        planetCenter = planet.GetComponent<Transform>().position;
        radius = 41.0f;
        count = 15;
    }

    public void SpawnEnemies()
    {
        Rigidbody planetRb = planet.GetComponent<Rigidbody>();
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        int i = count;

        //Spawnear enemigos
        while (i > 0)
        {
            //Generar angulo alfa/beta aleatorio
            float b = Random.Range(0.0f, 2 * Mathf.PI);
            float a = Random.Range((-1) * Mathf.PI / 2, Mathf.PI / 2);

            float x = Mathf.Cos(b) * Mathf.Cos(a);
            float y = Mathf.Cos(b) * Mathf.Sin(a);
            float z = Mathf.Sin(b);
            //Determinar x,y,z 
            Vector3 dir = new Vector3(x, y, z);

            //Raycast a algún punto de la superficie del planeta
            RaycastHit hit;

            if (Physics.Raycast(planetCenter, dir, out hit))
            {
                //Comprobar distancia segura respecto al jugador
                Vector3 enemyToPlayer = hit.point - playerRb.position;

                if (enemyToPlayer.magnitude > 5)
                {
                    //Generar enemigo
                    Instantiate(prefabEnemy, hit.point + hit.point.normalized, Quaternion.identity);
                    //Decrementar contador
                    //i--;
                }
            }

            i--;
        }
    }
}