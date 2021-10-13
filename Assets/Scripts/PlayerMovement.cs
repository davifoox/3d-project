using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public CharacterController controller; 
   public Transform cam;
    
    
    [Header("Floats Human")]
    public float speed;
    
    public float jumpSpeed = 8.0f;
    [Header("Ball Floats")]
    public float ballSpeed;
    
    public float jumpSpeedBall = 15f;
    
    [Header("General Values")]
    public float gravity;
    public float verticalVel;
    public float turnSmoothTime = 0.1f;
    public float smoothVel;
    
    
    [Header("Bools")]
    public bool ballForm;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHuman();
        ChangeForm();
        ReleaseCursor();
        BallMove();
       

    }

    void ReleaseCursor()
    {

        
        if(Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
            
        }
        
    }

    void ChangeForm()
    {
        if(Input.GetKeyDown(KeyCode.E)){

            ballForm = !ballForm;
        }

    }

    private void MoveHuman()
    {
        
        if(!ballForm)
        {
            
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            
        

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 gravityVector = new Vector3(0, verticalVel, 0);
        

        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        
        {
            verticalVel = jumpSpeed;

        }

        if(!controller.isGrounded)
        {
            verticalVel -= gravity * Time.deltaTime;
            
        }

        if(direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // adiciona a camera no ang do target
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // move referente a dir do target ( mouse X )
            
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            

        }

            controller.Move(gravityVector * Time.deltaTime);



        }
      
        
    }

    public void BallMove()
    {
       if(ballForm)
       
       {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            

        

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 gravityVector = new Vector3(0, verticalVel, 0);
        
        
        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        
        {
            verticalVel = jumpSpeedBall;

        }

        if(!controller.isGrounded)
        {
            verticalVel -= gravity * Time.deltaTime;
            
        }
        
        
        
        if(direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // adiciona a camera no ang do target
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // move referente a dir do target ( mouse X )
            controller.Move(moveDir.normalized * ballSpeed * Time.deltaTime);
            

        }

    controller.Move(gravityVector * Time.deltaTime);


           
       }
        
        


    }

}
