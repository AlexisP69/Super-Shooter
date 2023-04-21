using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public string nextSceneName = "Login";

    public void Play()
    { 
        SceneManager.LoadScene(nextSceneName);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application Quittée");
    }

}