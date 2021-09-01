using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody playerRb;
    private float horizontalInput;
    private float verticalInput;
    private float speed = 10.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        playerRb.AddForce(new Vector3(horizontalInput, 0f, verticalInput) * speed);
    }
}
