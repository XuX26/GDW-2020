using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapis : MonoBehaviour
{
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("coucou");
        other.gameObject.transform.Translate(Vector3.back * speed); // Vector3.forward
    }

}
