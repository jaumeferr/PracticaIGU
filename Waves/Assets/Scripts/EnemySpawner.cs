using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int count;

    public Transform prefabEnemy;
    public Rigidbody planet;
    public Rigidbody player;

    private Vector3 planetCenter;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        radius = 40.0f;
        count = 15;
        SpawnEnemies(count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies(int count){
        int i = count;

        //Spawnear enemigos
        while(i > 0){
            //Generar angulo alfa/beta aleatorio
            float beta = Random.Range(0.0f, 2 * Mathf.PI);
            float alfa = Random.Range(0.0f, 2 * Mathf.PI);

            //Determinar x,y,z 
            Vector3 newPos = new Vector3(planet.x + radius*Math.cos(beta), planet.y + radius*Math.sin(beta), planet.z + radius*Math.cos(alfa));

            //Comprobar distancia segura respecto al jugador
            Vector3 enemyToPlayer = newPos - player.position;

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
