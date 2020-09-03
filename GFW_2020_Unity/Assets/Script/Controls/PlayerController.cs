using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool instantDestroyMode;
    public float maxRangeRayCast;
    public WallCreator wallCreator;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        ReadInput();
    }

    void ReadInput()
    {
        if (Input.GetMouseButton(0) && gameManager.state == Phase.PlayMode)
        {
            int layerMask = 1 << 8;
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

        else if (Input.GetKeyDown(KeyCode.N))
        {
            gameManager.UpdateGameState(Phase.NewRound);
        }
    }

    void BreakWallOnRaycast(RaycastHit hit)
    {
        wallCreator.AddDestroyedCube(hit.transform.gameObject);
        hit.transform.gameObject.SetActive(false);
    }
}