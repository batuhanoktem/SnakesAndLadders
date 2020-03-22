using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, IMenuManager
{
    [SerializeField]
    private GameObject mainMenuCanvas;

    void Start()
    {
#if UNITY_ANDROID || UNITY_IOS
        mainMenuCanvas.transform.Find("QuitButton").gameObject.SetActive(false);
        mainMenuCanvas.transform.Find("StartButton").GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
#endif
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Restart()
    {
        throw new System.NotImplementedException();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
