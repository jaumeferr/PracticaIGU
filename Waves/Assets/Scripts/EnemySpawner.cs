using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int count;

    public Transform prefabEnemy;
    private GameObject planet;
    private GameObject player;

    private Vector3 planetCenter;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Planet");
        player = GameObject.Find("Player");

        radius = 40.0f;
        count = 15;
        SpawnEnemies(count);
    }

    // Update is called once per frame9
    void Update()
    {
        
    }

    void SpawnEnemies(int count){
        Rigidbody planetRb = planet.GetComponent<Rigidbody>();
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        int i = count;

        //Spawnear enemigos
        while(i > 0){
            //Generar angulo alfa/beta aleatorio
            float beta = Random.Range(0.0f, 2 * Mathf.PI);
            float alfa = Random.Range(0.0f, 2 * Mathf.PI);

            //Determinar x,y,z 
            Vector3 newPos = new Vector3(planetRb.position.x + radius*Mathf.Cos(beta), planetRb.position.y + radius*Mathf.Sin(beta), planetRb.position.z + radius*Mathf.Cos(alfa));

            //Comprobar distancia segura respecto al jugador
            Vector3 enemyToPlayer = newPos - playerRb.position;

            Debug.Log(
                enemyToPlayer.magnitude
            );
            Debug.Log(
                newPos
            );

            if(enemyToPlayer.magnitude > 5){
                //Generar enemigo
                Instantiate(prefabEnemy, newPos, Quaternion.identity);
                //Decrementar contador
                i --;
            }
        }
    }
    
}