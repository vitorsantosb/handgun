using System.Security.Cryptography;
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
    [Header("GameVars")]
    public bool isReady;
    private bool startGame;
    public Text TimeToStart_txt;
    private float timeToStart = 5;
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
        SetStateGame(STATE_GAME.INITIALIZING);
        ConsolerClear.ClearLog();
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
    public void CallingDices()
    {
        if (GetStateGame() == STATE_GAME.ROLLING_DICES)
        {
            for (int i = 0; i < buttonsList.ToArray().Length; i++)
            {
                buttonsList[i].GetComponent<buttonScript>().RollingDice();
                userList[i].SetDice(buttonsList[i].GetComponent<buttonScript>().diceResult);
            }
            int duplicated_dices = userList
                .GroupBy(d => d.GetDice())
                .Where(x => x.Count() > 1)
                .Sum(x => x.Count());
            Debug.Log("Dados Duplicados: " + duplicated_dices);
            if (duplicated_dices > 1)
            {
                CallingDices();
            }
            SetStateGame(STATE_GAME.CHANGE_USER_INFO);
            ChangeUserList();
        }
    }
    public void ChangeUserList()
    {
        if (GetStateGame() == STATE_GAME.CHANGE_USER_INFO)
        {
            for (int i = 0; i < userList.ToArray().Length; i++)
            {
                userList[i].SetName(buttonsList[i].GetComponent<buttonScript>().GetUserName());
            }
            userList.Sort((a, b) => a.GetDice() < b.GetDice() ? 1 : -1);
            userList.ForEach(b => Debug.Log("Username: " + b.GetName() + " | " + "DICE RESULT: " + b.GetDice() + " | " + "ID: " + b.GetId()));
            SetStateGame(STATE_GAME.STARTING);
            if (GetStateGame() == STATE_GAME.STARTING)
            {
                uiObj[0].SetActive(false);
                uiObj[1].SetActive(false);
                isReady = true;
                CountToInicialize();
            }

        }
    }
    public void IsReadyToStart()
    {
        startGame = true;
        Debug.Log("startGame value: " + startGame);
        CallingDices();
        SetStateGame(STATE_GAME.ROLLING_DICES);

        if (startGame == false)
        {
            SetStateGame(STATE_GAME.READY_TO_GO);
        }
    }
    public void CountToInicialize()
    {
        if (isReady)
        {
            Debug.Log("Função ON - MENSAGE LINE - 152");
            timeToStart -= Time.deltaTime;
            TimeToStart_txt.text = timeToStart.ToString("0");
            if (timeToStart <= 0)
            {
                isReady = false;
                DontDestroyOnLoad(dontDestroyObjects[0]);
                SceneController.SceneToGo("CenaTeste");
            }
        }
    }
    public void InicializeTurn()
    {
        for (int i = 0; i < userList.ToArray().Length; i++)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        CountToInicialize();
        AllButtonsReady();
    }
}
