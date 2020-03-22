using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour, IMenuManager
{
    [SerializeField]
    private GameObject pauseCanvas;

    private Text title;
    private RectTransform resumeButton;
    private RectTransform restartButton;
    private RectTransform quitButton;

    private GameManager gameManager;

    void Start()
    {
        title = pauseCanvas.transform.Find("Title").GetComponent<Text>();
        resumeButton = pauseCanvas.transform.Find("ResumeButton").GetComponent<RectTransform>();
        restartButton = pauseCanvas.transform.Find("RestartButton").GetComponent<RectTransform>();
        quitButton = pauseCanvas.transform.Find("QuitButton").GetComponent<RectTransform>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.gameStateChanged += GameManager_gameStateChanged;
    }

    private void GameManager_gameStateChanged(GameState gameState, Player player)
    {
        switch (gameState)
        {
            case GameState.NotStarted:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Finished:
                if (player.isBot)
                {
                    title.text = "YOU LOSE!";
                }
                else
                {
                    title.text = "YOU WIN!";
                }
                resumeButton.gameObject.SetActive(false);
                restartButton.anchoredPosition = new Vector2(restartButton.anchoredPosition.x, restartButton.anchoredPosition.y + resumeButton.anchoredPosition.y);
                quitButton.anchoredPosition = new Vector2(quitButton.anchoredPosition.x, quitButton.anchoredPosition.y + resumeButton.anchoredPosition.y);
                pauseCanvas.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Play()
    {
        pauseCanvas.SetActive(false);
        BackgroundMusic.Instance.Play();
    }

    public void Pause()
    {
        gameManager.GameState = GameState.Paused;
        BackgroundMusic.Instance.Pause();
        title.text = "PAUSED";
        pauseCanvas.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        BackgroundMusic.Instance.Play();
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        BackgroundMusic.Instance.Play();
    }
}
