using UnityEditor;
using UnityEngine;

public class SetGameData : MonoBehaviour
{
    public float characterSpeed;

    void Start()
    {
        GameData.characterSpeed = characterSpeed;
    }
}