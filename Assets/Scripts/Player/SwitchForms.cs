using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchForms : MonoBehaviour
{
    
    
    public GameObject humanoid, ball;
    private PlayerMovement PlayerMovement;

    int whichAvatar = 1;


    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        humanoid.gameObject.SetActive(true);
        ball.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchAvatar()
    {

        switch (whichAvatar)
        {
            case 1:
            whichAvatar = 2;
            humanoid.gameObject.SetActive(false);
            ball.gameObject.SetActive(true);
            break;


            case 2:
            whichAvatar = 1;
            humanoid.gameObject.SetActive(true);
            ball.gameObject.SetActive(false);
            break;


        }

    }
}
