using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; 
    public Transform playerTranform;
    public SphereCollider ballColl;
    public Rigidbody playerRb;
    public Transform cam;
    public GameObject mainCamera;
    private SwitchForms switchForms;
    RigidbodyConstraints originalConstraints;
    
    [Header("Floats Human")]
    public float speed;
    
    public float jumpSpeed = 8.0f;
    [Header("Ball Floats")]
    [SerializeField] float ballSpeed = 60f;
    [SerializeField] float regularBallSpeed = 80f;
    [SerializeField] float maxBallSpeed = 120f;
    [SerializeField] float boostForce = 150f;
    [SerializeField] float ballGravity = 30f;
    
    public float jumpSpeedBall = 15f;
    
    [Header("General Values")]
    [SerializeField]  float gravity;
    [SerializeField]  float verticalVel;
    [SerializeField]  float turnSmoothTime = 0.1f;
    [SerializeField]  float smoothVel;

    [SerializeField]  float boostDistance = 10f;
    [SerializeField]  float boostTime;
    [SerializeField]  bool isBoosted = false;
    
    [Header("Bools")]
    public bool ballForm;
   
   void Awake()
   {
       originalConstraints = playerRb.constraints;
   }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        switchForms = GetComponent<SwitchForms>();
        playerRb = GetComponent<Rigidbody>();
        ballColl = GetComponent<SphereCollider>();
        playerTranform = GetComponent<Transform>();
    }

    void Update()
    {
        MoveHuman();
        ChangeForm();
        ReleaseCursor();
        
       //isso aqui tá sendo feito na corrotina já:
        /*if(isBoosted){

            boostTime+= Time.deltaTime;
            if(boostTime>=3.5){

                ballSpeed = 25f;
                jumpSpeedBall = 15f;
                gravity = 35f;
                boostTime = 0;
                isBoosted = false;
            }
        }*/
    }

    void FixedUpdate()
    {
        //apply gravity
        
        playerRb.AddForce(Vector3.down * ballGravity * playerRb.mass);

        BallMove();
    }

    void ReleaseCursor()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

   public void ChangeForm()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            switchForms.SwitchAvatar();
            ballForm = !ballForm;
        }
    }

    private void MoveHuman()
    {
        if(!ballForm)
        {
            // condições
            controller.enabled = true;
            playerRb.detectCollisions = false;
            ballColl.enabled = false;
            playerRb.constraints = RigidbodyConstraints.FreezeRotation;
            playerTranform.rotation = Quaternion.Euler(0,playerTranform.localRotation.eulerAngles.y,0);

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
            //condições
            controller.enabled = false;
            playerRb.detectCollisions = true;
            ballColl.enabled =true;
            playerRb.constraints = originalConstraints;
            
            //camera
            Vector3 camToMe = transform.position - mainCamera.transform.position;
            camToMe.y = 0;
            camToMe.Normalize();

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
        
            //Vectors
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            Vector3 gravityVector = new Vector3(0, verticalVel, 0);
        
            Vector3 movement = (camToMe * vertical + mainCamera.transform.right * horizontal).normalized;

            playerRb.AddForce(movement * ballSpeed);        

            if(Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                verticalVel = jumpSpeedBall;
            }
       }
    
        
    }

    public void Boost(Vector3 direction)
    {
        if (ballForm)
        {
            playerRb.AddForce(direction * boostForce, ForceMode.Impulse);
            StartCoroutine("BoostTimer");
        }
    }

    IEnumerator BoostTimer()
    {
        Debug.Log("BoostTimer!");
        ballSpeed = maxBallSpeed;

        while (ballSpeed > regularBallSpeed)
        {
            yield return new WaitForSeconds(0.1f);
            ballSpeed -= 2f;
        }

        Debug.Log("BoostTimerEnded!");
        ballSpeed = regularBallSpeed;
    }
    
     void OnTriggerEnter(Collider collision)
     {
      
/*        if(collision.gameObject.tag == "Boost" && ballForm)
        {
            isBoosted = true;  
            ballSpeed = 50f;
            jumpSpeedBall = 35f;
            gravity = 30f;
        }
    }
    */
     }
}
