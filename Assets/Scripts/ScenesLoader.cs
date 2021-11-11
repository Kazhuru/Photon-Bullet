using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public void LoadGame()
    {
        GameSession gameStatus = FindObjectOfType<GameSession>();
        gameStatus.RestartScore();
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadStartScene()
    {
        GameSession gameStatus = FindObjectOfType<GameSession>();
        gameStatus.RestartScore();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameOver()
    {

        StartCoroutine(WaitAndLoadGameOver(2f));
    }

    IEnumerator WaitAndLoadGameOver(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over Scene");
    }
}
