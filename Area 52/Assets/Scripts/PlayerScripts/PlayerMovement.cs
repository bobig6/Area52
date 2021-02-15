using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles input and calls CharacterController2D class
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerInput.isInventoryActive)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                start_jumping();
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }

            if (horizontalMove == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }
        }
    }


    public void stop_jumping()
    {
        anim.SetBool("isJumping", false);
    }
    public void start_jumping()
    {
        anim.SetBool("isJumping", true);
    }

    public void on_crouch(bool is_crouching)
    {
        anim.SetBool("isCrouching", is_crouching);
    }

    /*
     Update runs once per frame.
     FixedUpdate can run once, zero, or several times per frame,
     depending on how many physics frames per second are set in
     the time settings, and how fast/slow the framerate is.
     */

    void FixedUpdate()
    {
        //Move the character 
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}
