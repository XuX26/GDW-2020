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
            fail();
        }
    }

    public void fail()
    {
        if(life==3)
        {
            //Ecran de Score
            Debug.Log("Score");
        }
        else
        {
            life += 1;
            Croix[life - 1].SetActive(true);
        }
        
    }
}
