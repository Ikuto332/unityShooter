using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    gunHndgun hndgun;



    Animator ani;

    public float walkSpeed = 4.0F;
    public float runSpeed = 5.0F;
    public float jumpSpeed = 8.0F;

    public int ammo_In_mag = 50;

   // public int ammo_inventory = 300;
    

    public float gravity = 20.0F;
     public bool allowRun = false; //ຫ້າມບໍ່ແລ່ນຕອນຍ່າງຊ້າຍຂວາ

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    public gunHndgun Hanngun;
    void Start()
    {
        ani = GetComponent<Animator>();
       
        controller = GetComponent<CharacterController>();
      
    }

    void Update()
    {
  
       

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= walkSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //---------------------
        float move = Input.GetAxis("Vertical");
        ani.SetFloat("speed", move);

        float move2 = Input.GetAxis("Horizontal");
        ani.SetFloat("speed2", move2);



       // if (Input.GetButton("Reload"))
       //{
       //     if(ammo_In_mag < 50)
       //     {
       //         ani.SetTrigger("reload");         
       //         ammo_In_mag = +50;                            
       //     }
           
       // }



       // if (Input.GetButton("Fire1"))
       // {
       //     if(ammo_In_mag >= 1)
       //     {
       //         ani.SetBool("allowshoot", true);
       //         ammo_In_mag =- 1;    
                
       //         if(ammo_In_mag <= 0)
       //         {
       //             ani.SetBool("allowshoot", false);
       //         }

       //     }
        
       // }














        if (Input.GetKey(KeyCode.LeftShift))
        {
        

                 walkSpeed = runSpeed;
            
            if (walkSpeed > 4  )
            {              
                ani.SetBool("runing", true);       
            }       
        }
        else
        {
           

            walkSpeed = 4;
            if (walkSpeed == 4)
            {
                
                ani.SetBool("runing", false);
            }
        }






        //-------------------------------------

    



        if (move == 0)
        {
            ani.SetBool("notmove", true);
           
        }
        else
        {
            ani.SetBool("notmove", false);
         
        }

        if (move2 == 0)
        {
            ani.SetBool("notmove2", true);

        }
        else
        {
            ani.SetBool("notmove2", false);

        }

        //--------------------






        //----------------------------


        //------------------------
    }
}