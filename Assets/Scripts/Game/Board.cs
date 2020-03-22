using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Piece[] Pieces { get; set; }
    void Awake()
    {
        Pieces = GetComponentsInChildren<Piece>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
