using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        // store movement inputs
        if (Input.touchCount > 0)
        {
            // touch screen inputs


        } else
        {
            // keyboard inputs
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            jump = Input.GetAxisRaw("Vertical") > 0 ? true : false;
            crouch = Input.GetAxisRaw("Vertical") < 0 ? true : false;
        }
    }

    void FixedUpdate()
    {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
