using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidPlayer;
    private float forceMult;
    [SerializeField] private Animator animator;

    void Awake()
    {
        this.rigidPlayer = GetComponent<Rigidbody>();
        this.forceMult = 10;
        this.animator = GetComponent<Animator>();
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
        animator.SetFloat("Blend X", moveH);
        animator.SetFloat("Blend Y", moveV);
        rigidPlayer.velocity = transform.forward * moveV * forceMult + transform.right * moveH * forceMult;        
    }
}
