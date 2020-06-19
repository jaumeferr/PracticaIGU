using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    //public GameObject player;
    Rigidbody player;
    Rigidbody rb;
    Transform tf;
    float speed = 0.1f;
    Vector3 planetCenter = new Vector3(0.0f, 0.0f, 0.0f);
    public bool paper = false;
    public Transform paperT;

    //Level 2
    public Rigidbody friend;

    // Start is called before the first frame update
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Rigidbody>();

        //Level 2
        GameObject friendObj = GameObject.FindGameObjectWithTag("NPC");
        if(friendObj != null){
            friend = friendObj.GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        if(friend != null){
            //Level 2
            float FriendDist = Vector3.Magnitude(rb.position - friend.position);
            float playerDist = Vector3.Magnitude(rb.position - player.position);

            if(playerDist <= FriendDist){
                Follow(player);
            }
            else{
                Follow(friend);
            }
        } 
        else{
            Follow(player);
        }
    }

    public void DropPaper()
    {
        //Crear instancia del papel sobre el enemigo
        Debug.Log("Dropping mf paper");

        Transform paper_dropped = Instantiate(paperT, tf.position, tf.rotation);

        //Aplicar fuerza para hacerlo volar en una dirección aleatoria.
        Vector2 force_dir = (tf.position - planetCenter).normalized;
        float force_val = 3;
        paper_dropped.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, tf.localEulerAngles.y,0) * force_val, ForceMode.Acceleration);

    }

    public void Follow(Rigidbody followed){
        //ENEMY MOVEMENT
        //A --> Vector de C al enemigo, B --> Vector de C al jugador
        Vector3 v = Vector3.Cross(rb.position - planetCenter, followed.position - planetCenter).normalized;
        Vector3 dir = Vector3.Cross(v, rb.position - planetCenter).normalized;

        //Mirar hacia el objetivo
        Quaternion rotation = Quaternion.LookRotation((tf.position + dir * speed) - tf.position, Vector3.up);
        tf.rotation = rotation;

        //Ir hacia el objetivo
        tf.position = tf.position + (dir * speed);
    }
}
