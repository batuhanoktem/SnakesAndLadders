using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private IMenuManager menuManager;

    void Awake()
    {
        menuManager = GetComponent<IMenuManager>();
    }

    public void Play()
    {
        menuManager.Play();
    }

    public void Pause()
    {
        menuManager.Pause();
    }

    public void Restart()
    {
        menuManager.Restart();
    }

    public void Quit()
    {
        menuManager.Quit();
    }
}
