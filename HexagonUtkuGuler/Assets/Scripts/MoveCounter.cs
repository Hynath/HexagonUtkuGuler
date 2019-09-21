using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounter : MonoBehaviour
{
    public static int MoveCount = 0;
    Text moves;

    // Start is called before the first frame update
    void Start()
    {
        moves = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        moves.text = "Moves: " + MoveCount;
    }
}
