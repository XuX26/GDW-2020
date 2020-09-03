using UnityEditor;

public class SetGameData
{
    public float characterSpeed;

    void Start()
    {
        GameData.characterSpeed = characterSpeed;
    }
}