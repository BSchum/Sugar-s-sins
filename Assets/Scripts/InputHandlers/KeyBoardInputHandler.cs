using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
class KeyBoardInputHandler : InputHandler
{
    public override bool SimpleAttackInput()
    {
        return Input.GetButtonDown("Fire1");
    }

    public override Vector3 ComputeMovement()
    {
        this.inputState = InputType.Keyboard;
        //TODO compute vector from ZQSD or Arrow
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        return new Vector3(x, 0, z);
    }

    public override Vector3 ComputeRotation()
    {
        float xRot = Input.GetAxis("Mouse Y");
        float yRot = Input.GetAxis("Mouse X");
        return new Vector3(xRot, yRot);
    }

    
}