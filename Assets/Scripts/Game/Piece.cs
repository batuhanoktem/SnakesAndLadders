using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private int row;
    private int column;

    public int number;

    [SerializeField]
    private bool isSnake;

    [SerializeField]
    private bool isLadder;

    [SerializeField]
    public int nextPiece = -1;

    void Awake()
    {
        row = (number - 1) / 6;
        column = (number - 1) % 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
