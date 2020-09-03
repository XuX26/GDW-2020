using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool megaDestroyMode;
    public bool spamMode;
    public float maxRangeRayCast;
    public float radiusSphereCast;
    public WallCreator wallCreator;
    public SoundManager SoundManager;

    private GameManager gameManager;
    int layerMask = 1 << 8;


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
        if (gameManager.state == Phase.PlayMode)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (spamMode && Input.GetMouseButtonDown(0))
            {

                if (!megaDestroyMode)
                {
                    if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Wall")
                    {
                        BreakWallOnRaycast(hit);
                        
                    }
                }

                else
                {
                    RaycastHit[] hits = Physics.SphereCastAll(ray, radiusSphereCast, maxRangeRayCast, layerMask);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        BreakWallOnRaycast(hits[i]);
                        
                    }
                }

            }
            else if (!spamMode && Input.GetMouseButton(0))
            {
                if (!megaDestroyMode)
                {
                    if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Wall")
                    {
                        BreakWallOnRaycast(hit);

                    }
                }

                else
                {
                    RaycastHit[] hits = Physics.SphereCastAll(ray, radiusSphereCast, maxRangeRayCast, layerMask);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        BreakWallOnRaycast(hits[i]);
                        FindObjectOfType<SoundManager>().PlaySfx("Destruction_Wall");

                    }
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