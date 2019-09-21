using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    public static int BombScoreValue = 0;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveCounter.MoveCount == 0)
        {
            scoreValue = 0;
            BombScoreValue = 0;
        }
        score.text = "Score: " + scoreValue;
    }
}
