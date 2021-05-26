using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : SpawnPlayer
{
    [Header("Players")]
    public GameObject[] players = new GameObject[4];

    [Header("UI-Component")]
    public Button[] button = new Button[4];
    public bool btIsReady_1, btIsReady_2, btIsReady_3, btIsReady_4;
    public Text uiButtonTxt_1, uiButtonTxt_2, uiButtonTxt_3, uiButtonTxt_4;
    public InputField playerCount_Txt;
    public int playerCount;
    void Awake()
    {
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
    }
    public int GetPlayerCount()
    {
        return Int32.Parse(playerCount_Txt.text);
    }
    public void SetPlayerCount()
    {
        this.playerCount = Int32.Parse(playerCount_Txt.text);
        try
        {
            switch (playerCount)
            {
                case 2:
                    button[2].gameObject.SetActive(false);
                    button[3].gameObject.SetActive(false);
                    break;
                case 3:
                    button[2].gameObject.SetActive(true);
                    break;
                case 4:
                    button[3].gameObject.SetActive(true);
                    break;
            }
        }
        catch (System.Exception)
        {

        }
    }
    public void SetupDices()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
