using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 8f;
    public bool paperFound = false;
    public int score = 0;

    Vector3 moveAmount;
    Vector3 smoothMoveSpeed;

    enum PlayerState
    {
        Walk, Attack, Jump
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * playerSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveSpeed, .15f);
    }

    void addPoints(int points)
    {
        this.score += points;
    }

    void FixedUpdate(){
        this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + this.GetComponent<Transform>().TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void Jump(){
        
    }
}
