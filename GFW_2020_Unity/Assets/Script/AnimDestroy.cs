using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroy : MonoBehaviour
{
    // Start is called before the first frame update

    public float TimeAnim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeAnim -= Time.deltaTime;

        if(TimeAnim<=0)
        {
            Destroy(gameObject);
        }
    }
}
