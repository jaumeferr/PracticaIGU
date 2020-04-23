using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public int vidas;
    GameObject texto_vidas;

    //public GameObject player;
    Rigidbody player;
    Rigidbody rb;
    Transform tf;
    float speed = 0.1f;
    Vector3 planetCenter = new Vector3(0.0f, 0.0f, 0.0f);
    GameObject texto;

    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = this.GetComponent<Rigidbody>(); 
        tf = this.GetComponent<Transform>();
        texto = GameObject.Find("Fin");
        texto_vidas = GameObject.Find("Vidas");
        setVidasTexto();
        texto.GetComponent<Text>().text = "";
    }

    void FixedUpdate()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();

        //ENEMY MOVEMENT
            //A --> Vector de C al enemigo, B --> Vector de C al jugador
        Vector3 v = Vector3.Cross(rb.position - planetCenter, player.position - planetCenter).normalized;
        Vector3 dir = Vector3.Cross(v, rb.position - planetCenter).normalized;

        //Mirar hacia el objetivo
        /*RaycastHit hit;
        if(Physics.Raycast(planetCenter, rb.position, out hit)){
            Vector3 normal = hit.normal;
        }*/

        Quaternion rotation = Quaternion.LookRotation((tf.position + dir * speed) - tf.position, Vector3.up);
        tf.rotation = rotation;

        //Ir hacia el objetivo
        tf.position = tf.position + (dir * speed);
    }

    // Ataque enemigo
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !player.GetComponent<PlayerController>().attacking)
        {
            vidas = vidas - 1;
            setVidasTexto();
            if (vidas < 1)
            {
                other.gameObject.SetActive(false);
                texto.GetComponent<Text>().text = "YOU DIED";
            }
        }
    }

    private void setVidasTexto()
    {
        texto_vidas.GetComponent<Text>().text = "Vidas: " + vidas.ToString();
    }
}
