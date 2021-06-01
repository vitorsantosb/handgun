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
    public GameObject[] dontDestroyObjects = new GameObject[1];
    public GameObject[] uiObj = new GameObject[1];
    private List<User> userList = new List<User>();
    public List<GameObject> buttonsList = new List<GameObject>();
    [Header("UI-Components")]
    public Text amountOfUsers;
    public GameObject uiRef;

    void Awake()
    {
        SetStateGame(STATE_GAME.INITIALIZING);
    }


    public int GetPlayerCount()
    {
        return int.Parse(amountOfUsers.text);
    }
    public void ReSizeList()
    {
        uiObj[1].SetActive(false);
        uiObj[0].SetActive(true);
        for (int i = 0; i < buttonsList.ToArray().Length; i++)
        {
            Destroy(buttonsList[i]);
        }
        buttonsList.Clear();
        userList.Clear();
    }
    public void ButtonEvent(InputField param)
    {
        this.playerCount = int.Parse(param.text);
        uiObj[0].SetActive(false);
        uiObj[1].SetActive(true);
        CreatNewUser(playerCount);
    }
    public void CreatNewUser(int playerCount)
    {
        if (GetStateGame() == STATE_GAME.INITIALIZING)
        {
            for (int i = 0; i < playerCount; i++)
            {
                GameObject button = Instantiate(buttonClone, new Vector3(uiRef.transform.position.x, uiRef.transform.position.y + (i * -65), 0), new Quaternion(0, 0, 0, 0));
                button.transform.SetParent(uiRef.transform);
                buttonsList.Add(button);
                userList.Add(new User("", 0, i + 1, playerClone));
            }
        }
    }
    public void AllButtonsReady()
    {
        for (int i = 0; i < buttonsList.ToArray().Length; i++)
        {
            if (buttonsList[i].GetComponent<buttonScript>().isReady)
            {
                uiObj[2].SetActive(true);
            }
            else if (buttonsList[i].GetComponent<buttonScript>().isReady == false)
            {
                uiObj[2].SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        AllButtonsReady();
    }
}
