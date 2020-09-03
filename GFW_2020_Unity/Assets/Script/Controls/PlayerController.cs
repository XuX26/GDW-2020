using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool instantDestroyMode;
    public float maxRangeRayCast;
    public WallCreator wallCreator;

    void Start()
    {
        wallCreator = GetComponent<WallCreator>();
    }

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

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        else if (Input.GetKeyDown(KeyCode.W))
        {
            wallCreator.ResetWall();
            Debug.Log("Wall Reset");
        }
    }

    void BreakWallOnRaycast(RaycastHit hit)
    {
        hit.transform.gameObject.SetActive(false);
        wallCreator.AddDestroyedCube(hit.transform.gameObject);
    }
}