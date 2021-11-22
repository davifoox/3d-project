using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    
    public PlayerRespawn playerRespawn;
    public Renderer checkRend;

    public Material checkOn;
    public Material checkOff;

    

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
            CheckpointOn();
            Debug.Log("Checkpoint");

        }

    }
    private void OnControllerCollider(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        
        {
            
            playerRespawn.SetRespawnPoint(transform.position);
            CheckpointOn();
            Debug.Log("Checkpoint");


        }

    }

    public void CheckpointOn()
    {
        
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        foreach (Checkpoint checkpoint in  checkpoints)
        {
            checkpoint.CheckpointOff();

        }

        checkRend.material = checkOn;

    }

    public void CheckpointOff()
    {
        checkRend.material = checkOff;

    }
}
