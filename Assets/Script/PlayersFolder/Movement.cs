using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidPlayer;
    private float forceMult;

    void Awake()
    {
        this.rigidPlayer = GetComponent<Rigidbody>();
        this.forceMult = 30;
    }

    void Start()
    {

    }


    void Update()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        move(moveH, moveV);
    }

    public void move(float moveH, float moveV)
    {
        if (moveV == 0 && moveH == 0)
        {
            return;
        }
        rigidPlayer.velocity = transform.forward * moveV * forceMult + transform.right * moveH * forceMult;
    }
}
