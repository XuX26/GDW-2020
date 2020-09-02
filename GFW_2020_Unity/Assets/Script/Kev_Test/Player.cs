using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float temps = 2;

    public void OnTriggerEnter(Collider collision)
    {
        if (gameObject.transform.CompareTag("Player"))
        {
            Debug.Log("c'est ok");
            temps -= Time.deltaTime;
        }

    }

    public void Update()
    {
        if (temps <= 0)
        {
            Debug.Log("c'est good");
            rb.isKinematic = true;
        }
    }
}
