using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancePose : MonoBehaviour
{
    public GameObject[] prefab;
    public GameObject actualChar;
    private int randomPrefabInt;


    public void InstanciateCharacter()
    {
        randomPrefabInt = Random.Range(0, prefab.Length);
        //actualChar = Instantiate(prefab[randomPrefabInt],this.transform, Vector3.zero);
        actualChar = Instantiate(prefab[randomPrefabInt], transform.position, Quaternion.identity, transform);
    }
}
