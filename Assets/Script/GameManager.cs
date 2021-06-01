using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;
using UnityEditor;
public class GameManager : SpawnPlayer
{
    [Header("Players")]
    public GameObject playerClone;
    public GameObject buttonClone;
    public int playerCount;

    [Header("Arrays and Lists")]
    public InputField[] usernameInput = new InputField[4];
    public GameObject[] dontDestroyObjects = new GameObject[2];
    public Text[] username_txt = new Text[4];
    private List<User> users = new List<User>();
    private List<GameObject> buttonsList = new List<GameObject>();
    [Header("UI-Components")]
    public Text playerCount_Txt;
    public GameObject uiRef;

    void Awake()
    {

    }


    public int GetPlayerCount()
    {
        return int.Parse(playerCount_Txt.text);
    }
    public void ButtonEvent(InputField param)
    {
        this.playerCount = int.Parse(param.text);
        CreatNewUser(playerCount);
    }
    public void CreatNewUser(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject button = Instantiate(buttonClone, new Vector3(uiRef.transform.position.x, uiRef.transform.position.y + (i * -40), 0), new Quaternion(0, 0, 0, 0));
            button.transform.SetParent(uiRef.transform);
            buttonsList.Add(button);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
