using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonScript : MonoBehaviour
{
    public static bool isReady;
    public Text buttonText;
    public InputField userNameInput;
    private static string userName;
    public Text userText;
    public void IsReady()
    {
        isReady = !isReady;
        buttonText.text = isReady ? "READY" : "UNREADY";
        Debug.Log(isReady);
        CreateUserName();
    }
    public void CreateUserName()
    {
        userText.text = userNameInput.text;
        userName = userNameInput.text;
    }
    public string GetUserName() => userName;
}
