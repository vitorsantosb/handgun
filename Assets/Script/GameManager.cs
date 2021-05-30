using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;
public class GameManager : SpawnPlayer
{
    [Header("Players")]
    public GameObject playerClone;

    [Header("Arrays and Lists")]
    public Button[] button = new Button[4];
    public InputField[] usernameInput = new InputField[4];
    public GameObject[] dontDestroyObjects = new GameObject[2];
    public Text[] username_txt = new Text[4];
    [SerializeField] private List<GameObject> users = new List<GameObject>();

    [Header("UI-Components")]
    public Text uiButtonTxt_1;
    public Text uiButtonTxt_2;
    public Text uiButtonTxt_3;
    public Text uiButtonTxt_4;
    public InputField playerCount_Txt;
    public int playerCount;
    public Text diceResult_1, diceResult_2, diceResult_3, diceResult_4;
    private int dice_1, dice_2, dice_3, dice_4;
    private bool btIsReady_1, btIsReady_2, btIsReady_3, btIsReady_4;
    [Header("GameComponents")]
    private bool startCount;
    private float timeToStart;
    public Text timeToStart_txt;
    [Header("SceneManager")]
    public string sceneTarget;
    private GameObject managerObj;
    public Scene targetScene;

    void Awake()
    {
        startCount = false;
        timeToStart = 5;
        SetStateGame(STATE_GAME.INITIALIZING);
        button[0].onClick = new Button.ButtonClickedEvent();
        button[0].onClick.AddListener(() =>
        {
            btIsReady_1 = !btIsReady_1;
            uiButtonTxt_1.text = btIsReady_1 ? "READY" : "UNREADY";
        });

        button[1].onClick = new Button.ButtonClickedEvent();
        button[1].onClick.AddListener(() =>
        {
            btIsReady_2 = !btIsReady_2;
            uiButtonTxt_2.text = btIsReady_2 ? "READY" : "UNREADY";
        });

        button[2].onClick = new Button.ButtonClickedEvent();
        button[2].onClick.AddListener(() =>
        {
            btIsReady_3 = !btIsReady_3;
            uiButtonTxt_3.text = btIsReady_3 ? "READY" : "UNREADY";
        });

        button[3].onClick = new Button.ButtonClickedEvent();
        button[3].onClick.AddListener(() =>
        {
            btIsReady_4 = !btIsReady_4;
            uiButtonTxt_4.text = btIsReady_4 ? "READY" : "UNREADY";
        });

        button[2].gameObject.SetActive(false);
        button[3].gameObject.SetActive(false);
        usernameInput[2].gameObject.SetActive(false);
        usernameInput[3].gameObject.SetActive(false);
    }
    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
    public int GetPlayerCount()
    {
        return int.Parse(playerCount_Txt.text);
    }
    public void ButtonEvent(InputField param)
    {
        param = playerCount_Txt;
        if (playerCount == 2)
        {
            username_txt[0].text = usernameInput[0].text;
            username_txt[1].text = usernameInput[1].text;
            SetPlayerCount(param);
        }
        else
        {
            username_txt[0].text = usernameInput[0].text;
            username_txt[1].text = usernameInput[1].text;
            username_txt[2].text = usernameInput[2].text;
            username_txt[3].text = usernameInput[3].text;
            SetPlayerCount(param);
        }
    }
    public void SetPlayerCount(InputField playersNumber)
    {
        this.playerCount = int.Parse(playersNumber.text);
        try
        {
            switch (this.playerCount)
            {
                case 2:
                    button[2].gameObject.SetActive(false);
                    button[3].gameObject.SetActive(false);

                    if (btIsReady_1 == true && btIsReady_2 == true)
                    {
                        SetStateGame(STATE_GAME.ROLLING_DICES);
                        SetupDices(playerCount);
                    }
                    break;
                case 4:
                    button[2].gameObject.SetActive(true);
                    button[3].gameObject.SetActive(true);

                    usernameInput[2].gameObject.SetActive(true);
                    usernameInput[3].gameObject.SetActive(true);

                    if (btIsReady_1 == true && btIsReady_2 == true && btIsReady_3 == true && btIsReady_4 == true)
                    {
                        SetStateGame(STATE_GAME.ROLLING_DICES);
                        SetupDices(playerCount);
                    }
                    break;
            }
        }
        catch (System.Exception)
        {

        }
    }
    public void SetupDices(int playerCount)
    {
        Debug.Log("Rolling Dices... - MENSAGE LINE 92");
        if (GetStateGame() == STATE_GAME.ROLLING_DICES)
        {
            SetStateGame(STATE_GAME.STARTING);
            switch (this.playerCount)
            {
                case 2:
                    dice_1 = Random.Range(0, 21);
                    dice_2 = Random.Range(0, 21);
                    diceResult_1.text = dice_1.ToString();
                    diceResult_2.text = dice_2.ToString();

                    startCount = true;
                    break;
                case 4:
                    dice_1 = Random.Range(0, 21);
                    dice_2 = Random.Range(0, 21);
                    dice_3 = Random.Range(0, 21);
                    dice_4 = Random.Range(0, 21);

                    diceResult_1.text = dice_1.ToString();
                    diceResult_2.text = dice_2.ToString();
                    diceResult_3.text = dice_3.ToString();
                    diceResult_4.text = dice_4.ToString();

                    startCount = true;
                    break;
            }
        }
    }
    public void CountToStart()
    {
        if (GetStateGame() == STATE_GAME.STARTING)
        {
            if (startCount)
            {
                timeToStart -= Time.deltaTime;
                timeToStart_txt.text = timeToStart.ToString("0");
                if (timeToStart <= 0)
                {
                    startCount = false;
                    for (int i = 0; i < dontDestroyObjects.Length; i++)
                    {
                        DontDestroyOnLoad(dontDestroyObjects[i]);
                        SceneController.SceneToGo(sceneTarget);
                        SetStateGame(STATE_GAME.SPAWNPLAYER);
                        LoadingUsers(playerCount);
                        //ClearLog();
                    }
                }
            }
        }
    }
    public void CheckDiceResult(int playerCount)
    {
        switch (playerCount)
        {
            case 2:
                if (dice_1 == dice_2)
                {
                    
                }
                break;
        }
    }
    public void AddNewUser(int userCount)
    {
        Debug.Log("Insert players in list -- MENSAGE LINE 213" + " | " + userCount);
        for (int i = 0; i <= 4; i++)
        {
            if (i <= userCount)
            {
                users.Add(playerClone);
            }
        }
    }
    public void LoadingUsers(int userCount)
    {
        if (GetStateGame() == STATE_GAME.SPAWNPLAYER)
        {
            dontDestroyObjects[0].transform.GetChild(0).gameObject.SetActive(false);
            // -> Desabilitando interface antiga.
            AddNewUser(userCount);
            SceneManager.MoveGameObjectToScene(managerObj, targetScene);
            SetupSpawn(users);
        }

    }
    // Update is called once per frame
    void Update()
    {
        CountToStart();
    }
}
