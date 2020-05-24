using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // store movement inputs
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        jump = Input.GetAxisRaw("Vertical") > 0 ? true : false;
    }

    void FixedUpdate()
    {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
