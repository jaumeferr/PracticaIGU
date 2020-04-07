using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{
    GravityAttractor planet;

    void Awake(){
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate(){
        planet.Attract(this.GetComponent<Transform>());
    }
}
