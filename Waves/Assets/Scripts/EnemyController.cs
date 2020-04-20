using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int vidas;
    public UnityEngine.UI.Text texto_vidas;

    //public GameObject player;
    public Rigidbody player;
    Rigidbody rb;
    Transform tf;
    float speed = 0.1f;
    Vector3 planetCenter = new Vector3(0.0f, 0.0f, 0.0f);
    public UnityEngine.UI.Text texto;

    
    // Start is called before the first frame update
    void Start()
    {
        this.texto.text = "";
        rb = this.GetComponent<Rigidbody>(); 
        tf = this.GetComponent<Transform>();
        setVidasTexto();
        //player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        //ENEMY MOVEMENT
            //A --> Vector de C al enemigo, B --> Vector de C al jugador
        Vector3 v = Vector3.Cross(rb.position - planetCenter, player.position - planetCenter).normalized;
        Vector3 dir = Vector3.Cross(v, rb.position - planetCenter).normalized;

        //Mirar hacia el objetivo
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
                this.texto.text = "YOU DIED";
            }
        }
    }

    private void setVidasTexto()
    {
        this.texto_vidas.text = "Vidas: " + vidas.ToString();
    }
}
