using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Joystick joystick;
    public CharacterController2D controller;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        // get inputs from joystick or keyboard
        float horizontalInput = (System.Math.Abs(joystick.Horizontal) > 0.2) ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");
        float verticalInput = (joystick.Vertical >= 0.8) ? joystick.Vertical : Input.GetAxisRaw("Vertical");
        horizontalMove = horizontalInput * runSpeed;
        jump = verticalInput > 0 ? true : false;
        // crouch = verticalInput < 0 ? true : false;
    }

    void FixedUpdate()
    {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
