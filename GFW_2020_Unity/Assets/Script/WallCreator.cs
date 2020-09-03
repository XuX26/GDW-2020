using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public float gap;
    public GameObject CubeModel;
    public Transform firstCubePos; //top left corner
    public Transform lastCubePos; //bot right corner

    private float cubeSize;
    private int nbrCubeInLane;
    private int nbrLane;
    private List<GameObject> destroyedCube = new List<GameObject>();

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        cubeSize = CubeModel.GetComponent<BoxCollider>().size.x; //only x because it's a square
        CreateWall();
    }

    void CreateWall()
    {
        SetNbrOfCubes();
        InstanciateCubes();
    }

    void SetNbrOfCubes()
    {
        float tmpNbrCubesInLane = GetWallLenght() / (gap * cubeSize);
        nbrCubeInLane = (int)tmpNbrCubesInLane;

        float tmpNbrLane = GetWallWidth() / (gap * cubeSize);
        nbrLane = (int)tmpNbrLane;
    }

    void InstanciateCubes()
    {
        Debug.Log(nbrCubeInLane);
        Debug.Log(nbrLane);

        Vector3 NextInstancePose = firstCubePos.localPosition;
        for (int lane = 1; lane > nbrLane; lane--)
        {
            for (int cube = 0; cube < nbrCubeInLane; cube++)
            {
                Instantiate(CubeModel, NextInstancePose, Quaternion.identity,this.transform);
                NextInstancePose.x += gap;
            }

            NextInstancePose.x = firstCubePos.localPosition.x;
            NextInstancePose.y -= gap;
        }
        Debug.Log("Wallfinished");
    }

    float GetWallLenght()
    {
       Debug.Log(lastCubePos.localPosition.x - firstCubePos.localPosition.x);
        return (lastCubePos.localPosition.x - firstCubePos.localPosition.x);
    }

    float GetWallWidth()
    {
        return (lastCubePos.localPosition.y - firstCubePos.localPosition.y);
    }

    public void ResetWall()
    {
        foreach (GameObject cube in destroyedCube)
        {
            ReactiveCube(cube);
        }
        CleanCubeList();
        anim.Play("WallPop");
    }

    public void AddDestroyedCube(GameObject cube)
    {
        destroyedCube.Add(cube);
    }

    void ReactiveCube(GameObject cube)
    {
        cube.SetActive(true);
    }

    void CleanCubeList()
    {
        //destroyedCube = new List<GameObject>();
        destroyedCube.Clear();
    }
}