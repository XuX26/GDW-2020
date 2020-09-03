using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancePose : MonoBehaviour
{
    public GameObject[] prefab;
    public PoseManager PoseManager;

    private int randomPrefabInt;
    public Transform SpawnPos; 

    //void Start()
   // {

     //   for (int i = 0; i < prefab.Length; i++)
      //  {
      //      prefab[i] = Resources.Load("Prefabs/Prefab" + i) as GameObject;
      //  }
      //  Instantiate(prefab[Random.Range(0, prefab.Length)]);

    //}

    void Update()
    {
        instance(); 
    }

    void instance()
    {
        if(PoseManager.passe && PoseManager.instancePose) // Player after wall + rajouter condition pour un couldown // PoseManager.timerOk
        {
            randomPrefabInt = Random.Range(0, prefab.Length);
            Instantiate(prefab[randomPrefabInt], SpawnPos.position, SpawnPos.rotation);
        }
        PoseManager.passe = false;
        PoseManager.instancePose = false;
    }
}
