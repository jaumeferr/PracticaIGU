using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 8f;
    public float sensibility = 1;
    public bool paperFound = false;
    public int score = 0;

    Vector3 moveAmount;
    public float turn;
    Vector3 smoothMoveSpeed;
    
    Rigidbody rb;
    Transform tf;

    enum PlayerState
    {
        Walk, Attack, Jump
    }


    public bool attacking;
    public SkillBarController skillBar;

    public HealthBar healthBar;
    public int vidas;
    public int muertos;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        attacking = false;
        skillBar.SetAttackBar();
        healthBar.SetMaxHealth(vidas);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento adelante y atrás
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(0, 0, moveVertical).normalized;

        Vector3 targetMoveAmount = moveDir * playerSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveSpeed, .15f);

        //Move and face movement direction
        rb.MovePosition(rb.position + tf.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        tf.Rotate(0,moveHorizontal,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(!attacking)
            {
                vidas = vidas - 1;
                healthBar.SetHealthBar(vidas);
                if (vidas < 1)
                {
                    FindObjectOfType<GameManager>().GameOver();
                }
            } else 
            {
                muertos = muertos - 1;
                if (muertos == 0)
                {
                    FindObjectOfType<GameManager>().Victory();
                }
            }
        }
    }

    void addPoints(int points)
    {
        this.score += points;
    }


}
