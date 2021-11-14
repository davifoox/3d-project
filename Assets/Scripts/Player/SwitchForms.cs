using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchForms : MonoBehaviour
{
    public GameObject humanoid, ball;
    private PlayerMovement PlayerMovement;
    public Rigidbody playerBody;

    int whichAvatar = 1;

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        humanoid.gameObject.SetActive(true);
        ball.gameObject.SetActive(false);
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
            ResetRigidbodyMomentum();
            break;
        }

    }

    void ResetRigidbodyMomentum()
    {
        playerBody.velocity = Vector3.zero;
        playerBody.angularVelocity = Vector3.zero;
    }
}
