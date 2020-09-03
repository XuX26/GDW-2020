using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float temps;



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("WallPas"))
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
           //score ++
        }
        else if(PoseManager.passe == false) // rajouter un couldown après avoir toucher le mur
        {
            temps -= Time.deltaTime;

            //

            if(temps <= 0 && PoseManager.passe == false)
            {
                PoseManager.end = true;
            }
            else if(temps >= 0 && PoseManager.passe == true)
            {

            }
            //FindObjectOfType<GameManager>().reloadScene();
        }
    }

}
