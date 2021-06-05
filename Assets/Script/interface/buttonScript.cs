using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonScript : MonoBehaviour
{
    public bool isReady;
    public Text buttonText;
    public InputField userNameInput;
    private static string userName;
    public Text userText;
    public int diceResult;
    public Text diceResult_txt;
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
    public void RollingDice()
    {
        diceResult = Random.Range(0, 21);
        diceResult_txt.text = diceResult.ToString();
    }
    private void Update()
    {
        
    }
}
