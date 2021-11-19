using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    private bool isRespawning;
    public float respawnLenght;
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
        if(hit.gameObject.tag == "Water")
        
        {

            Respawn();
        
        }
        

    }
    public void Respawn()
    {
        
        if(!isRespawning)
        {

            StartCoroutine("RespawnCo");

        }
    }
    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        

        
        yield return new WaitForSeconds(respawnLenght);
        isRespawning = false;


        
        thePlayer.transform.position = respawnPoint;

    }
    public void SetRespawnPoint(Vector3 newRespPos)
    
    {

        respawnPoint = newRespPos;

    }
}
