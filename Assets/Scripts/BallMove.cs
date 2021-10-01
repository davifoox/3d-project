using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Rigidbody playerRB;
    private Vector3 inputVector;
    public Transform cam;

    float speed = 10;
    float jumpSpeed = 15;

    public bool isGround = true;

    private float boostForce = 200f;
    private bool ballForm = true;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

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

    public void Boost(Vector3 direction)
    {
        if (ballForm)
        {
            playerRB.AddForce(direction * boostForce, ForceMode.Impulse);
            StartCoroutine("BoostTimer");
        }
    }

    IEnumerator BoostTimer()
    {
        Debug.Log("BoostTimer!");
        speed = 50;

        while (speed > 10)
        {
            yield return new WaitForSeconds(0.1f);
            speed--;
        }

        Debug.Log("BoostTimerEnded!");
        speed = 10;
    }

    void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;

        }
        
    }

}
