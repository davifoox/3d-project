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
    private Animator animator;
    private OptionsMenu options;
    
    
    
    
    
    RigidbodyConstraints originalConstraints;
    
    [Header("Floats Human")]
    public float speed;
    
    public float jumpSpeed = 8.0f;
    [Header("Ball Floats")]
    [SerializeField] float ballSpeed = 60f;
    [SerializeField] float regularBallSpeed = 80f;
    [SerializeField] float maxBallSpeed = 120f;
    [SerializeField] float boostForce = 50f;
    [SerializeField] float ballGravity = 30f;
    
    [SerializeField] float jumpSpeedBall = 15f;
    
    [Header("General Values")]
    [SerializeField]  float gravity;
    [SerializeField]  float verticalVel;
    [SerializeField]  float turnSmoothTime = 0.1f;
    [SerializeField]  float smoothVel;

    [SerializeField]  float boostDistance = 10f;
    [SerializeField]  float boostTime;
    [SerializeField]  bool isBoosted = false;
    [SerializeField]  bool ballForm;
    public Quaternion originalRotation;
    private Vector3 originalPosition;
    public float rotSpeed;

    [Header("Audios")]

    public AudioSource audioSource;
    public AudioClip walk;

    


   
   void Awake()
   {
       originalConstraints = playerRb.constraints;
       originalRotation = transform.rotation;
       originalPosition = playerTranform.position;
       
   }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        switchForms = GetComponent<SwitchForms>();
        playerRb = GetComponent<Rigidbody>();
        ballColl = GetComponent<SphereCollider>();
        playerTranform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();


        PlayerPrefs.GetFloat("FxVol");

    }

    void Update()
    {
        MoveHuman();
        ChangeForm();
        ReleaseCursor();
        
       
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
            Cursor.visible = true;
        }
    }

   public void ChangeForm()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * rotSpeed);
            playerTranform.position = new Vector3(playerTranform.position.x, playerTranform.position.y + 2, playerTranform.position.z);
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
            
            animator = GetComponentInChildren<Animator>();
            
            bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
            bool isWalking = hasHorizontalInput || hasVerticalInput;
	    

            animator.SetBool("isWalking", isWalking);
            
          
            
            

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
    
     
     public void WalkingFxSound(float fxValue)
     {

         audioSource.volume = options.fxSlider.value;
         audioSource.PlayOneShot(walk);
         PlayerPrefs.GetFloat("FxVol", fxValue);

     }
}
