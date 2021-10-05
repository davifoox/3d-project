using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public Transform target;
    public Transform pivot;
    public Vector3 offset;
    public bool useOffsetValues;

    public float rotSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues)
        {

            offset = target.position - transform.position;
        }
        
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.parent;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get the X pos of mouse & rotate the target

        float horizontal = Input.GetAxis("Mouse X") * rotSpeed;
        target.Rotate(0, horizontal, 0);

        //Get the Y pos of mouse & rotate the target/pivot
        //float vertical = Input.GetAxis("Mouse Y") * rotSpeed;
        //pivot.Rotate(vertical , 0 , 0);
        
        //Move the camera based on the current rot of the target & the original offset
        float desiredY = target.eulerAngles.y;
        float desidedX = pivot.eulerAngles.x;


        Quaternion rot = Quaternion.Euler(desidedX, desiredY, 0);
        transform.position = target.position - (rot * offset);
        
        
        //transform.position = target.position - offset;
        
        transform.LookAt(target);
    }
}
