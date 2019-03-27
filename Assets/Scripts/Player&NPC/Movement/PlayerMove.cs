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
        if(!isRooted)
            motor.Move(ih.ComputeMovement(), ih.ComputeRotation(), this.GetComponent<Stats>().GetSpeed());

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
