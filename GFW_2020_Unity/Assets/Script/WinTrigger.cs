using UnityEditor;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<SoundManager>().PlaySfx("Pass_Mur");
            gameManager.isPassed = true;
            gameManager.UpdateGameState(Phase.Check);
            Debug.Log("pass !");
        }
    }
}