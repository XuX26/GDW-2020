using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PoseManager PoseManager;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.CompareTag("Wall"))
        {
            PoseManager.touch = true;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.transform.CompareTag("WallPas"))
        {
            PoseManager.passe = true;
        }
    }

    public void Update()
    {
        verif();
    }

    public void verif()
    {
        if(PoseManager.passe)
        {
            PoseManager.instancePose = true;
            PoseManager.touch = false;
            if(PoseManager.touch == false)
            {
                //score ++
            }
            else if(PoseManager.touch)
            {
                //score --
            }
            else
            {
                //Score Normal
            }

        }
        else if(PoseManager.passe == false && PoseManager.touch) // rajouter un couldown après avoir toucher le mur
        {
            PoseManager.defaite = true;
            //FindObjectOfType<GameManager>().reloadScene();
        }
    }

}
