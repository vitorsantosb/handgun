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
    public GameObject[] uiObj = new GameObject[1];
    public GameObject[] CanvasList = new GameObject[0];
    public Text[] UI_manager = new Text[0];
    private List<User> userList = new List<User>();
    public List<GameObject> buttonsList = new List<GameObject>();
    [Header("UI-Components")]
    public Text amountOfUsers;
    public GameObject uiRef;
    [Header("GameVars")]
    public bool isReady;
    private bool startGame;
    public Text TimeToStart_txt;
    private float timeToStart;
    public GameObject currentUser;
    public int currentUserID;

    [Header("Turn vars")]
    private float turnTimer;
    public bool startTurn;
    public int currentTurn;
    void Awake()
    {
        SetStateGame(STATE_GAME.INITIALIZING);
        this.timeToStart = 5;
        this.currentTurn = 0;
        this.turnTimer = 0;
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
        for (int i = 0; i < buttonsList.Count; i++)
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
                GameObject button = Instantiate(buttonClone, new Vector3(uiRef.transform.position.x, uiRef.transform.position.y + (i * -80), 0), new Quaternion(0, 0, 0, 0));
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
            for (int i = 0; i < buttonsList.Count; i++)
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
            for (int i = 0; i < userList.Count; i++)
            {
                userList[i].SetName(buttonsList[i].GetComponent<buttonScript>().GetUserName());
            }
            userList.Sort((a, b) => a.GetDice() < b.GetDice() ? 1 : -1);
            userList.ForEach(b => Debug.Log("Username: " + b.GetName() + " | " + "DICE RESULT: " + b.GetDice() + " | " + "ID: " + b.GetId() + " | " + b.GetUserObject()));

            SetStateGame(STATE_GAME.STARTING);

            if (GetStateGame() == STATE_GAME.STARTING)
            {
                uiObj[0].SetActive(false);
                uiObj[1].SetActive(false);
                isReady = true;
                CountToInicialize();
            }
            //ConsolerClear.ClearLog();
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
            timeToStart -= Time.deltaTime;
            TimeToStart_txt.text = timeToStart.ToString("0");
            if (timeToStart <= 0)
            {
                isReady = false;
                SetStateGame(STATE_GAME.SPAWNPLAYER);
            }
            InicializeTurn();
        }
    }
    public void InicializeTurn()
    {
        if (GetStateGame() == STATE_GAME.SPAWNPLAYER)
        {
            ConsolerClear.ClearLog();
            SetupSpawn(userList);

            UpdateUserList();
            userList.ForEach(b => Debug.Log("Username: " + b.GetName() + " | " + "DICE RESULT: " + b.GetDice() + " | " + "ID: " + b.GetId() + " | " + b.GetUserObject()));

            var actuallyPlayer = userList.FirstOrDefault<User>();
            this.currentUser = actuallyPlayer.GetUserObject();
            this.currentUserID = actuallyPlayer.GetId();

            CanvasList[0].SetActive(false);

            SetStateGame(STATE_GAME.START_TURN);

            InicializeRound(this.currentUser);
            Debug.Log("O primeiro jogador a jogar: " + actuallyPlayer.GetName() + "| Com resultado de: " + actuallyPlayer.GetDice());
        }
    }

    public void UpdateUserList()
    {
        GameObject[] playerInScene = GameObject.FindGameObjectsWithTag("Player");

        //Realizando Update na lista
        for (int index = 0; index < userList.Count; index++)
        {
            userList[index].SetUserObject(playerInScene[index]);
        }
        userList.ForEach(b => Debug.Log("PlayerName: " + b.GetName() + " Objeto atual " + b.GetUserObject()));

    }
    public void InicializeRound(GameObject _currentPlayer)
    {
        CanvasList[1].SetActive(true);
        if (GetStateGame() == STATE_GAME.START_TURN)
        {
            startTurn = true;
            this.turnTimer = 15;
            SetUserMove(_currentPlayer, true);
            TurnCount();
        }
    }
    public void TurnCount()
    {
        if (startTurn)
        {
            turnTimer -= Time.deltaTime;
            UI_manager[0].text = turnTimer.ToString("0");
            if (turnTimer <= 0)
            {
                startTurn = false;
                EndTurn();
            }
        }
    }
    public void EndTurn()
    {

        List<User> alive = userList.FindAll(u => u.GetUserObject() != null);
        if (alive.Count <= 1)
        {
            User theLastOne = alive[0];
            FinalTurn(theLastOne);
            return;
        }

        SetUserMove(this.currentUser, false);

        bool found = false;
        bool next = false;
        for (int i = 0; i < userList.Count; i++)
        {
            var p = userList[i];

            if (next && p.GetUserObject() != null)
            {
                found = true;

                currentUserID = p.GetId();
                currentUser = p.GetUserObject();

                SetStateGame(STATE_GAME.NEXT_ROUND);
                NextTurn(i);
                break;
            }

            if (p.GetId() == this.currentUserID)
            {
                next = true;
            }
        }
        if (!found)
        {
            this.currentTurn++;
            UI_manager[1].text = currentTurn.ToString("0");
            // pronto 0-0
            // e n precisa disso aqui n ?

            for (int i = 0; i < userList.Count; i++)
            {
                var p = userList[i];
                if (p.GetUserObject() != null)
                {
                    currentUserID = p.GetId();
                    currentUser = p.GetUserObject();

                    SetStateGame(STATE_GAME.NEXT_ROUND);
                    NextTurn(i);
                    break;
                }
            }
        }
    }
    public void SetUserMove(GameObject objUser, bool active)
    {
        if (objUser == null) return;
        objUser.GetComponent<Aim>().enabled = active;
        objUser.GetComponent<Movement>().enabled = active;
        //PEGAR CAMERA DO JOGADOR E HABILIDADE CAMERA
    }

    public void NextTurn(int index)
    {

        if (GetStateGame() == STATE_GAME.NEXT_ROUND)
        {
            SetUserMove(currentUser, true);
            turnTimer = 15;

            SetStateGame(STATE_GAME.START_TURN);
            InicializeRound(currentUser);
        }
    }
    public void FinalTurn(User user)
    {
        // user = winner
        // show
        //testa aqui;
        // tu ta com 2 monitor? n
        // ata k por isso não to vendo outro k

        Debug.Log("Final Turn, winner: " + user.GetId() + " | " + user.GetName());
    }
    // Update is called once per frame
    void Update()
    {
        CountToInicialize();
        TurnCount();
        AllButtonsReady();
    }
    
}
