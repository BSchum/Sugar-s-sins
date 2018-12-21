using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NetworkIdentity))]
public class PlayerMove : PlayerScript{

    public MotorBase motor;
    NetworkIdentity netIdentity;

    // Use this for initialization
    public void Start () {
        Initialize();
        motor = new Motor(this.gameObject);
    }

    // Update is called once per frame
    void Update () {

        //Move with ih vector
        motor.Move(ih.ComputeMovement(), ih.ComputeRotation(), this.GetComponent<Stats>().GetSpeed());
    }
}
