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

        radius = 41.0f;
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
            float b = Random.Range(0.0f, 2 * Mathf.PI);
            float a = Random.Range((-1)*Mathf.PI / 2, Mathf.PI/2);

            float x = radius * Mathf.Cos(b) * Mathf.Cos(a);
            float y = radius * Mathf.Cos(b) * Mathf.Sin(a);
            float z = radius * Mathf.Sin(b);
            //Determinar x,y,z 
            Vector3 newPos = new Vector3(x,y,z);

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