using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 8f;
    float torqueSpeed = 1000f;
    public bool paperFound = false;
    public int score = 0;

    Vector3 moveAmount;
    public float turn;
    Vector3 smoothMoveSpeed;
    
    Rigidbody rb;

    enum PlayerState
    {
        Walk, Attack, Jump
    }

    //Color al atacar
    public Color myColor;
    public Material material;
    public KeyCode changeCol;
    private bool attacking;
    public float cooldown;
    private float nextFireTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * playerSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveSpeed, .15f);
        rb.MovePosition(rb.position + this.GetComponent<Transform>().TransformDirection(moveAmount) * Time.fixedDeltaTime);

        if(Time.time > nextFireTime){
            if (Input.GetKeyDown(changeCol))
            {
                print("GO STUPID!");
                nextFireTime = Time.time + cooldown;
                material.color = myColor;
            }
        }
    }

    void addPoints(int points)
    {
        this.score += points;
    }


}
