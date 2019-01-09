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

    public override bool RightClick()
    {
        return Input.GetButton("Fire2");
    }

    public override bool FirstSkill()
    {
        return Input.GetButton("FirstSkill");
    }

    public override bool FirstSkillUp()
    {
        return Input.GetButtonUp("FirstSkill");
    }

    public override bool SecondSkill()
    {
        return Input.GetButtonDown("SecondSkill");
    }

    public override bool ThirdSkill()
    {
        return Input.GetButtonDown("ThirdSkill");
    }

    public override bool Ultimate()
    {
        return Input.GetButtonDown("Ultimate");
    }
    public override Vector3 ComputeMovementFromMouse()
    {
        if(Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            return Vector3.forward;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public override Vector3 ComputeMovement()
    {
        this.inputState = InputType.Keyboard;
        //TODO compute vector from ZQSD or Arrow
        float z = 0;
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            z = 1;
        }
        float x = Input.GetAxis("Horizontal");
        if(z == 0)
            z = Input.GetAxis("Vertical");
        return new Vector3(x, 0, z);
    }

    public override Vector3 ComputeRotation()
    {
        float xRot = Input.GetAxis("Mouse Y");
        float yRot = Input.GetAxis("Mouse X");
        return new Vector3(xRot, yRot, 0);
    }

}