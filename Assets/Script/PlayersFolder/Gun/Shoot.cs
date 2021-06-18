using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float BlastPower = 5;

    public GameObject Ammunition;
    public Transform ShotPoint;
    private float timeToDestroy = 7;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject CreatedAmmunition = Instantiate(Ammunition, ShotPoint.position, ShotPoint.rotation);
            CreatedAmmunition.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;
            DestroyShootOnTime(CreatedAmmunition);
        }
    }
    public void DestroyShootOnTime(GameObject obj) => Destroy(obj, timeToDestroy);
}
