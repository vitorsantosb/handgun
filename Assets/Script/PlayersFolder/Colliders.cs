using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colliders : MonoBehaviour
{
    public int playerHealth;
    public int maxHealth;
    public Image LifeBar;

    void Start()
    {
        this.playerHealth = 100;
        this.maxHealth = 120;

    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bullet")
        {
            RemoveHealth(10);
            Debug.Log("HIT COLISION");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            RemoveHealth(50);
            Debug.Log("HIT !!!!");
        }
    }
    public int GetLife() => this.playerHealth;
    public void SetLife(int increment)
    {
        if (increment >= this.playerHealth)
        {
            this.playerHealth =  this.maxHealth;
        }
        else
        {
            this.playerHealth = increment;
        }
        if (this.playerHealth <= 0)
        {
            DeathController();
        }
    }
    public void AddHealth(int lifeIncrement) => this.SetLife(this.playerHealth + lifeIncrement);
    public void RemoveHealth(int lifeReduced) => this.SetLife(this.playerHealth - lifeReduced);
    //public void UpdateLifeBar() => this.LifeBar.fillAmount = ((1.6f / this.maxHealth) * this.playerHealth);
    public void DeathController() => Destroy(gameObject);
}
