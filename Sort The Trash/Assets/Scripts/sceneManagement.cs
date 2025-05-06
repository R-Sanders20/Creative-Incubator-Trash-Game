using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class sceneManagement : MonoBehaviour
{
    string scoreText;
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadSceneAsync(0);
        scoreText = "Score: ";
    }
}
