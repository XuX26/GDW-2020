using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float temps;
    public PoseManager PoseManager;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("WallPas"))
        {
            PoseManager.passe = true;
        }
    }

    public void Update()
    {
        temps -= Time.deltaTime;
        verif();
    }

    public void verif()
    {
        if(PoseManager.passe && temps > 0)
        {
            PoseManager.instancePose = true;
            temps = 10;
           //score ++
        }
        else if(PoseManager.passe == false && temps <= 0) // rajouter un couldown après avoir toucher le mur
        {

            PoseManager.end = true;
            //FindObjectOfType<GameManager>().reloadScene();
        }
    }

}
