using System.Linq;
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
        startGame = false;
    }


    public int GetPlayerCount()
    {
        return int.Parse(amountOfUsers.text);
    }
    public void ReSizeList()
    {
        SetStateGame(STATE_GAME.INITIALIZING);
        uiObj[0].SetActive(true);
        uiObj[1].SetActive(false);
        uiObj[2].SetActive(false);
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
            SetStateGame(STATE_GAME.READY_TO_GO);
        }
    }
    public void AllButtonsReady()
    {
        if (GetStateGame() == STATE_GAME.READY_TO_GO)
        {
            bool ReadyState = true;

            for (int i = 0; i < buttonsList.ToArray().Length; i++)
            {
                if (buttonsList[i].GetComponent<buttonScript>().isReady == false)
                {
                    ReadyState = false;
                    break;
                }
            }

            uiObj[2].SetActive(ReadyState);


        }
    }
    public bool startGame;
    public void IsReadyToStart()
    {
        Debug.Log("start game is: " + startGame);
        startGame = true;
        CallingDices();
        SetStateGame(STATE_GAME.ROLLING_DICES);
        if (startGame == false)
        {
            SetStateGame(STATE_GAME.READY_TO_GO);

        }
    }
    public void CallingDices()
    {
        if (GetStateGame() == STATE_GAME.ROLLING_DICES)
        {

            for (int i = 0; i < buttonsList.ToArray().Length; i++)
            {
                buttonsList[i].GetComponent<buttonScript>().RollingDice();
                CheckDiceResult();
            }

        }
    }
    public void CheckDiceResult()
    {
        if (GetStateGame() == STATE_GAME.ROLLING_DICES)
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        AllButtonsReady();
    }
}
