using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Croix;
    public int life;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            //DisplayX();
        }
    }

    public void DisplayX(int life)
    {
        Croix[life].SetActive(true);
    }
}
