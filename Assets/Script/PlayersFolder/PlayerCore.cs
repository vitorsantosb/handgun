using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public int life = 10;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetLife() => this.life;
    public void SetLife(int _health) => this.life = _health;
    
}
