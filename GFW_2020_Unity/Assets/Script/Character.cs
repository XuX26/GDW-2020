using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rb;
    //public SoundManager SoundManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.back * GameData.characterSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Wall")
        {
            SoundManager.instance.PlaySfx("Collision2");
            rb.freezeRotation = false;
        }
    }
}

