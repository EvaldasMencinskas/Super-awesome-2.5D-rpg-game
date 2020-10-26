using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller; //access to diferent script
    public Animator animator; // access to animator
    public float RunSpeed = 40f; // run speed 
    float HorizontalMove = 0f; // move left or right
    float VerticalMove = 0f;
    bool Jump = false; // jump 
    bool croutch = false; // crouch
    bool stopmoving = false;
    

     private void Start()
    {
        float number = PlayerPrefs.GetFloat("DistanceX", 0);
        transform.position = Vector3.right * number + Vector3.up * PlayerPrefs.GetFloat("DistanceY", 0);
        //Debug.Log("wtf is this shit: " + number);
    }
    // Update is called once per frame
    void Update()
    {
        // horizontal speed is 1 or -1 * runspeed(40)
        if (!stopmoving)
        {
            HorizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;
            VerticalMove = Input.GetAxisRaw("Vertical");
        }
        if(VerticalMove == -1 || HorizontalMove !=0 && VerticalMove == -1)
        {
            croutch = true;
            animator.SetBool("CrouchBool", croutch);
            if(HorizontalMove != 0)
            {
                animator.SetBool("CrouchSlideBool", croutch);
            }
            
        }
        else // when crouch is finished stop all crouch animations
        {
            croutch = false;
            animator.SetBool("CrouchBool", croutch);
            animator.SetBool("CrouchSlideBool", croutch);
        }

        // if space is pressed and player is on ground jump becomes true 
        if (Input.GetButtonDown("Jump") && controller.m_Grounded && !croutch)
        {
            Jump = true; 
            animator.SetFloat("Run Float", Mathf.Abs(0.0F));
            animator.SetTrigger("Jump Trigger");
        }
        else
        {               // FOR runing i used HorizontalMove speed variable
            animator.SetFloat("Run Float", Mathf.Abs(HorizontalMove));
        }

        //Attack animations if on ground
        if (!croutch) // if crouch you cant jump or do attack animations
        {
            if (controller.m_Grounded)

                if (Input.GetButtonDown("Fire1"))
                {
                    animator.Play("AttackOne");
                }

                else if (Input.GetButtonDown("Fire2"))
                {
                    animator.Play("AttackTwo");                 
                }


                else if (Input.GetKeyUp("1"))
                {
                    animator.Play("RedSwordAttack");
                }
        }
        
            // if not on a ground and mouse pressed do jumpattack animation
            if (!controller.m_Grounded && Input.GetButtonDown("Fire1"))
        {
            animator.Play("JumpAttack");
        }
            
        
    }

    void FixedUpdate ()
    {
        //Move character
        
        controller.Move(HorizontalMove * Time.fixedDeltaTime, croutch, Jump);
        Jump = false;
        
        
    }
}
