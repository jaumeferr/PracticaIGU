using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody player;
    Rigidbody rb;

    Vector3 moveAmount;
    Vector3 smoothMoveSpeed;

    //Movement variables
    Vector3 planetCenter = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 directionToTarget;
    Vector3 directionToTargetNormalized;
    float distanceToTarget;
    Vector3 auxPoint;

    // Start is called before the first frame update
    void Start()
    {
       rb = this.GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Direction between enemy and player
        directionToTarget = player.position - rb.position;
        distanceToTarget = directionToTarget.magnitude; 
        directionToTargetNormalized = Vector3.Normalize(directionToTarget);

        Ray rayToTarget = new Ray(rb.position, player.position);
        RaycastHit rayHitToTarget;
        if(Physics.Raycast (rayToTarget, out rayHitToTarget)){
            Debug.DrawLine (rayToTarget.origin, rayHitToTarget.point, Color.green);
        }

        //Point some small distance along the line
        auxPoint = rb.position + directionToTargetNormalized;

        //Ray through that point to the center of the earth
        Ray rayToGround = new Ray(planetCenter, auxPoint);
        
        //Intersection over the ray and the ground
        RaycastHit hitGroundInfo;
        if(Physics.Raycast (rayToGround, out hitGroundInfo)){
            if(hitGroundInfo.rigidbody != null){
                Debug.DrawLine (rayToGround.origin, hitGroundInfo.point, Color.yellow);
                //Move to the waypoint
                rb.position = hitGroundInfo.point;
            }
        }
    }
}
