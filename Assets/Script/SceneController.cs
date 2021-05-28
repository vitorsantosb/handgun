using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneController
{
    public static void SceneToGo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

