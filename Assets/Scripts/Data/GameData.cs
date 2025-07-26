using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    public Vector2 playerPosition;
    public GameData()
    {
        this.score = 0;
        this.playerPosition = Vector2.zero;
    }
}
