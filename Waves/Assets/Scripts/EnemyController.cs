using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody player;
    Rigidbody rb;

    Vector3 moveAmount;
    Vector3 smoothMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
       rb = this.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(
            player.position.x - rb.position.x,
            player.position.y - rb.position.y,
            player.position.z - rb.position.z).normalized;
        
        
    }
}
