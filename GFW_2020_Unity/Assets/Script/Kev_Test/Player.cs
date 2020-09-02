using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public void OnCollisionEnter(Collision collision)
    {
        if (gameObject.transform.CompareTag("Player"))
        {
            rb.isKinematic = true;
        }
    }
}
