using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour 
{
    public static float score = 0;
    public static float scoreMultiply = 5;
    public static float speedMultiply = 1;
    public static bool isGameOver = false;

    private void Start()
    {
        Time.timeScale = 1;
        scoreMultiply = 5;
        score = 0;
        speedMultiply = 1;
        isGameOver = false;
    }
}
