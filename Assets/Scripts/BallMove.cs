using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    
    public Rigidbody playerRB;
    private Vector3 inputVector;
    public Transform cam;


    public float speed;
    public float jumpSpeed;


    public bool isGround = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        NormalMove();
    }

    void FixedUpdate(){

        playerRB.velocity = inputVector;
    }
    void NormalMove(){

        inputVector = new Vector3(Input.GetAxis("Horizontal") * speed, playerRB.velocity.y, Input.GetAxis("Vertical") * speed);
        

        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));

        if(Input.GetButtonDown("Jump") && isGround)
        {
            playerRB.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGround = false;
            

        }
    }
    void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

        }
        
    }

}
