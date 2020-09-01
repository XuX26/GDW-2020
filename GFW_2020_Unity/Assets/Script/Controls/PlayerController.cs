using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    void ReadInput()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("mouseDown");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                BreakWallOnRaycast(hit);
            }
        }

    }

    void BreakWallOnRaycast(RaycastHit hit)
    {
        Debug.Log(hit.collider.tag);
        hit.transform.gameObject.SetActive(false);
    }
}
