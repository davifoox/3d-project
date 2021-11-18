using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    private bool isRespawning;
    private Vector3 respawnPoint;
    public PlayerMovement thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    
        respawnPoint = thePlayer.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            Respawn();

        }

    }
    void OnControllerCollider(ControllerColliderHit hit)
    {
        Respawn();

    }
    public void Respawn()
    {
        thePlayer.transform.position = respawnPoint;

    }
}
