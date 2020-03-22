using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public bool isBot;
    public bool IsActive { get; set; }
    public int ActivePiece { get; set; } = 1;
    public float botSpeed = 2f;
}
