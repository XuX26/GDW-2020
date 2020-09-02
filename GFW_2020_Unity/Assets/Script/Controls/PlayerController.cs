using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool instantDestroyMode;
    public float maxRangeRayCast;

    void Update()
    {
        ReadInput();
    }

    void ReadInput()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!instantDestroyMode)
            {
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Wall")
                {
                    BreakWallOnRaycast(hit);
                }
            }

            else
            {
                int layerMask = 1 << 8;
                RaycastHit[] hits = Physics.RaycastAll(ray, maxRangeRayCast, layerMask);
                for (int i = 1; i < hits.Length; i++)
                {
                    BreakWallOnRaycast(hits[i]);
                }

            }
        }
    }

    void BreakWallOnRaycast(RaycastHit hit)
    {
        hit.transform.gameObject.SetActive(false);
    }
}
