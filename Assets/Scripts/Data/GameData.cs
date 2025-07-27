using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector2 playerPosition;
    public GameData()
    {
        this.playerPosition = Vector2.zero;
    }
}
