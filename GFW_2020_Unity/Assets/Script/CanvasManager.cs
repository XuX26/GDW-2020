using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] lifeX;

    public void DisplayX(int life)
    {
        lifeX[life-1].SetActive(true);
    }
}
