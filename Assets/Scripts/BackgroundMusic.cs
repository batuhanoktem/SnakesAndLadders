using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance { get; set; }

    private AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
            Destroy(gameObject);
    }

    public void Play()
    {
        if (audioSource.isPlaying) return;
            audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }
}