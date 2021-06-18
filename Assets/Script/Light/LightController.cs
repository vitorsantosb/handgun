using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject sun;

    public GameObject[] lamps = new GameObject[30];
    public float intensity;

    public void Start()
    {
        lamps = GameObject.FindGameObjectsWithTag("lamps");
    }
    // Update is called once per frame
    void Update()
    {

        for (int index = 0; index >= lamps.Length; index++)
        {
            if (sun.GetComponent<LightManager>().GetStateLamp() == true)
            {
                lamps[index].GetComponent<Light>().enabled = true;
            }
            else
            {
                lamps[index].GetComponent<Light>().enabled = false;
            }
        }
    }
}