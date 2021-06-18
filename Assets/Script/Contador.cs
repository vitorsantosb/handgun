using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Contador : MonoBehaviour
{
    public void TurnCount(bool isReady, float timer, Text textTimer)
    {
        if (isReady)
        {
            timer -= Time.deltaTime;
            textTimer.text = timer.ToString("0");
            if (timer <= 0)
            {
                isReady = false;
            }
        }
    }
}
