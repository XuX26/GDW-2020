using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerFx : MonoBehaviour
{
    // Start is called before the first frame update

    public int Time;

    public List<Sprite> sprite;
    public GameObject Panel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect()
    {
        gameObject.GetComponent<Image>().sprite = sprite[Time];
        Time += 1;
        if(Time==5)
        {
            Destroy(Panel);
            Destroy(gameObject);
        }
    }
}
