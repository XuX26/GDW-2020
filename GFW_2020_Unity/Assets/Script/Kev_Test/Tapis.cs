using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapis : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public Vector3 move;

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("coucou");
        if(other.gameObject.CompareTag("Player"))
        {
            //  other.gameObject.transform.Translate(gameObject.transform.forward * speed); // Vector3.back
            movement(move);
        }  
    }

    void movement(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

}
