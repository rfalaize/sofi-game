using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // store movement inputs
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //jump = Input.GetButtonDown("Jump") ? true : false;
        //crouch = Input.GetButtonDown("Crouch") ? true : false;
        jump = Input.GetAxisRaw("Vertical") > 0 ? true : false;
        crouch = Input.GetAxisRaw("Vertical") < 0 ? true : false;
    }

    void FixedUpdate()
    {
        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
