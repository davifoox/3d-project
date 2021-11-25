using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCameras : MonoBehaviour
{
    
    public GameObject player;
    public Transform tFollow;
    private CinemachineVirtualCamera vcam1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        vcam1 = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("CamFollow");
            if(player!= null)
            {
                tFollow = player.transform;
                vcam1.Follow = tFollow;

            }

        }
    }
}
