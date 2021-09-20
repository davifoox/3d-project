using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody playerRb;
    private CharacterController controller;
    private Vector3 moveDir;
    
    
    private float horizontalInput;
    private float verticalInput;
    
    
    public float speedBall;
    public float speedNormal;
    public float rotSpeed;
    public float gravity;
    private float rot;

    
   


    public bool ballForm;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();    
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        NormalMove();
        ChangeForm();


    }

    void FixedUpdate()
    {
        MoveBall();

    }


    void MoveBall()
    {

        if(ballForm)
        {
            
            
            controller.enabled = false;


            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
    
            playerRb.AddForce(new Vector3(horizontalInput, 0f, verticalInput) * speedBall);
            
            


        }
       
    }

    void NormalMove()
    {
        if(!ballForm)
        
        {
            controller.enabled = true;
            
            if(controller.isGrounded)
            {
                if(Input.GetKey(KeyCode.W))
                {
                    moveDir = Vector3.forward * speedNormal;

                }
            
            if(Input.GetKeyUp(KeyCode.W))
            {

                moveDir = Vector3.zero;

            }
            
            }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0,rot,0);

        moveDir.y -= gravity * Time.deltaTime;
        moveDir = transform.TransformDirection(moveDir) * Time.deltaTime;

        controller.Move(moveDir);

        }

    }
    void ChangeForm(){

        if(Input.GetKeyDown(KeyCode.E)){

            ballForm = !ballForm;
        }
    }
}
