using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public float intensity;
    private float inclination;
    public bool DayOrNight;


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(intensity * 355 / 60 * Time.deltaTime, 0, 0);
        inclination = gameObject.transform.eulerAngles.x;
        if (inclination >= 180)
        {
            SetStateLamp(true);
        }
        else
        {
            SetStateLamp(false);
        }
    }
    public bool GetStateLamp()
    {
        return this.DayOrNight;
    }
    public void SetStateLamp(bool state) => this.DayOrNight = state;
}
