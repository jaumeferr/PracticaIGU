using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f;

    public void Attract (Transform tf, Rigidbody rb){
        Vector3 targetDir = (tf.position - transform.position).normalized;
        Vector3 bodyUp = tf.up;
        float mass = rb.mass;

        tf.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * tf.rotation;
        tf.GetComponent<Rigidbody>().AddForce(targetDir * gravity * mass, ForceMode.Acceleration);
    }
}
