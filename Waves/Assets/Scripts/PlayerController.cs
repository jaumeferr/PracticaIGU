using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Color al atacar
    public Color initialColor;
    public Color AttackColor;
    public Material material;
    public KeyCode changeCol;
    public bool attacking;
    public float cooldown;
    private float nextFireTime;
    private float attackTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tf = this.GetComponent<Transform>();
        attacking = false;
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

        if(Time.time > nextFireTime){
            if (Input.GetKeyDown(changeCol))
            {
                nextFireTime = Time.time + cooldown;
                attackTime = Time.time + (cooldown / 2);
                //material.color = AttackColor;

                this.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
                this.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", AttackColor);

                attacking = true;
            }
        }
        if(attacking){
            if(Time.time > (attackTime)){
                //material.color = initialColor;
                this.GetComponent<Renderer>().sharedMaterial.EnableKeyword("_EMISSION");
                this.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", initialColor);
                attacking = false;
                attackTime = 0;
                nextFireTime = 0;
            }
        }
    }

    void addPoints(int points)
    {
        this.score += points;
    }


}
