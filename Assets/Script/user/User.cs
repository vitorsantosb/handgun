using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class User : MonoBehaviour
{
    private string username;
    private int dice;
    private int id;
    private GameObject obj;
    public User(string username, int dice, int id, GameObject userObject)
    {
        this.username = username;
        this.dice = dice;
        this.id = id;
        this.obj = userObject;
    }
    public string GetName() => this.username;
    public void SetName(string name) => this.username = name;

    public int GetDice() => this.dice;
    public void SetDice(int dice) => this.dice = dice;
    public int GetId() => this.id;
    public GameObject GetUserObject() => this.obj;
    public void SetUserObject(GameObject userObj) => this.obj = userObj;
}