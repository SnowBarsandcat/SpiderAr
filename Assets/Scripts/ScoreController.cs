using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    void Update()
    {
        scoreText.text = score.ToString();
    }
}
