using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private AudioSource audioSource;
    private SceneSettings settings;
    private int score;

    private void Awake()
    {
        int gameManagerCounter = FindObjectsOfType<GameSession>().Length;
        if (gameManagerCounter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        settings = FindObjectOfType<SceneSettings>();
        audioSource.clip = settings.GetSceneAudioClip();
        audioSource.Play();
    }

    void Update()
    {
        settings = FindObjectOfType<SceneSettings>();
        if (settings != null)
        {
            if (audioSource.clip.name != settings.GetSceneAudioClip().name)
            {
                audioSource.clip = settings.GetSceneAudioClip();
                audioSource.Play();
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void RestartScore()
    {
        score = 0;
    }
}
