using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public PlayerRespawn playerRespawn;

    // Start is called before the first frame update
    void Start()
    {
        playerRespawn = FindObjectOfType<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals ("Player"))
        {

            playerRespawn.SetRespawnPoint(transform.position);
            Debug.Log("Checkpoint");

        }

    }
    private void OnControllerCollider(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        
        {
            
            playerRespawn.SetRespawnPoint(transform.position);
            Debug.Log("Checkpoint");


        }

    }
}
