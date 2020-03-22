using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text playerText;

    [SerializeField]
    private Board board;

    [SerializeField]
    private float playerSpeed = 80f;

    [SerializeField]
    private Vector2 playerPadding = new Vector2(18f, 18f);

    [SerializeField]
    private RollManager rollManager;

    [SerializeField]
    private GameObject playerList;

    public event System.Action<GameState, Player> gameStateChanged;

    public GameState GameState { get; set; } = GameState.NotStarted;

    private Player[] players;

    private int activePlayerIndex;
    private Player activePlayer;

    private bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        rollManager.OnRollFinished = () =>
        {
            Move();
        };

        players = playerList.GetComponentsInChildren<Player>();
        ChooseStartPlayer();
    }

    private void ChoosePlayer()
    {
        activePlayer = players[activePlayerIndex];
        activePlayer.IsActive = true;

        playerText.text = $"{activePlayer.playerName} Turn";

        if (activePlayer.isBot)
        {
            rollManager.Disable();
            StartCoroutine(BotPlay());
        }
        else
        {
            rollManager.Enable();
        }
    }

    private void ChooseStartPlayer()
    {
        activePlayerIndex = Random.Range(0, players.Length);
        ChoosePlayer();
    }

    private void ChangePlayer()
    {
        activePlayer.IsActive = false;

        if (++activePlayerIndex == players.Length)
            activePlayerIndex = 0;

        ChoosePlayer();
    }

    public void Roll()
    {
        rollManager.Disable();
        rollManager.Roll();
    }

    private void Move()
    {
        int nextPiece = activePlayer.ActivePiece + rollManager.activeNumber;

        if (nextPiece > board.Pieces.Length)
            nextPiece = board.Pieces.Length;

        var pieces = board.Pieces.Where(p => p.number > activePlayer.ActivePiece && p.number <= nextPiece).ToList();
        activePlayer.ActivePiece = nextPiece;
        StartCoroutine(MovePlayer(pieces));
    }

    private void Move(Piece piece)
    {
        var activePiece = activePlayer.GetComponent<RectTransform>().anchoredPosition;
        var nextPiece = piece.GetComponent<RectTransform>().anchoredPosition + playerPadding;
        if (activePiece != nextPiece)
        {
            activePlayer.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(activePiece, nextPiece, playerSpeed * Time.deltaTime);
        }
        else
        {
            isMove = false;
        }
    }

    private IEnumerator BotPlay()
    {
        yield return new WaitForSeconds(activePlayer.botSpeed);
        Roll();
    }

    private IEnumerator MovePlayer(List<Piece> pieces)
    {
        foreach (Piece piece in pieces)
        {
            isMove = true;
            while (isMove)
            {
                Move(piece);
                yield return null;
            }
            yield return new WaitUntil(() => !isMove);
        }

        var lastPiece = pieces.Last();
        if (lastPiece.nextPiece != -1)
        {
            activePlayer.ActivePiece = lastPiece.nextPiece;

            var nextPiece = board.Pieces.Single(p => p.number == lastPiece.nextPiece);
            isMove = true;
            while (isMove)
            {
                Move(nextPiece);
                yield return null;
            }
            yield return new WaitUntil(() => !isMove);
        }

        if (lastPiece.number != 36)
        {
            if (rollManager.activeNumber != 6)
                ChangePlayer();
            else
            {
                if (activePlayer.isBot)
                {
                    yield return new WaitUntil(() => GameState != GameState.Paused);
                    StartCoroutine(BotPlay());
                }
                else
                    rollManager.Enable();
            }
        }
        else
            gameStateChanged(GameState.Finished, activePlayer);
    }
}
