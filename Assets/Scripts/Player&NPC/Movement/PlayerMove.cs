using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NetworkIdentity))]
public class PlayerMove : PlayerScript{

    public Motor motor;
    NetworkIdentity netIdentity;
    public bool isRooted;
    // Use this for initialization
    public void Start () {
        Initialize();
        motor = new Motor(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        //Move with ih vector

        if (!isRooted)
        {
            Debug.Log(ih.ComputeMovement());

            if(ih.ComputeMovement() == Vector3.zero)
            {
                GetComponent<BaseAnimation>().Stay();
                Debug.Log("Je stay");
            }
            else if (ih.ComputeMovement().x < 0)
            {
                Debug.Log("Je strafe");
                GetComponent<PlayersAnimations>().StrafeLeft();
            }
            else if (ih.ComputeMovement().x > 0)
            {
                Debug.Log("Je strafe");
                GetComponent<PlayersAnimations>().StrafeRight();
            }
            else if (ih.ComputeMovement().z > 0 && ih.ComputeMovement().x == 0)
            {
                Debug.Log("J'avance");
                GetComponent<BaseAnimation>().Walk();
            }

            motor.Move(ih.ComputeMovement(), ih.ComputeRotation(), this.GetComponent<Stats>().GetSpeed());
        }



        if (ih.RightClick()) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            motor.MoveLookDirection(ih.ComputeRotation());
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
