using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    public GameObject QuitButton;
    public void Menu()
    {
        //SceneManager.LoadScene("HomeMenu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuNovo");
    }
    public void Options()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuOptions");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void IngameScene()
    {
        //SceneManager.LoadScene("MapaBlocagem");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
    }

}
