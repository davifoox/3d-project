using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Rigidbody playerRB;
    private Vector3 inputVector;

    public  float speed = 10;
    public  float regularSpeed = 10;
    public  float maxSpeed = 15;

    public float jumpSpeed = 15; 
    public float rotSpeed;

    public bool isGround = true;

    private float boostForce = 100f;
    private bool ballForm = true;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        NormalMove();
    }

    void FixedUpdate()
    {
        //playerRB.velocity = inputVector;
        TerrainPush();
    }

    void NormalMove()
    {
        if(ballForm)
        {
            //inputVector = new Vector3(Input.GetAxis("Horizontal") * speed, playerRB.velocity.y, Input.GetAxis("Vertical") * speed);
            //transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
    
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            float cameraRot = Camera.main.transform.rotation.eulerAngles.y;
            playerRB.position += Quaternion.Euler(0, cameraRot, 0) * input * speed * Time.deltaTime;

            if(Input.GetButtonDown("Jump") && isGround)
            {
                playerRB.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                isGround = false;
            }
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
        speed = maxSpeed;

        while (speed > 12)
        {
            yield return new WaitForSeconds(0.1f);
            speed--;
        }

        Debug.Log("BoostTimerEnded!");
        speed = regularSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void UnlockCursor()
    {
        if(Input.GetKey("Escape")){
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void TerrainPush()
    {
        int naturalYDistance = 1;
        float playerPositionCalculatedY = this.transform.position.y - Terrain.activeTerrain.SampleHeight(this.transform.position);
     
        if (playerPositionCalculatedY < naturalYDistance && isGround)
        {
            float pushHeight = 0.5f - playerPositionCalculatedY;
            transform.position += new Vector3(0, pushHeight, 0);
        }
    }
}
