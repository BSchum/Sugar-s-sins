﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputHandler
{
    public enum InputType
    {
        Keyboard,
        Controller
    }
    public InputType inputState;

    public abstract Vector3 ComputeMovement();
    public abstract Vector3 ComputeMovementFromMouse();
    public abstract Vector3 ComputeRotation();
    public abstract bool SimpleAttackInput();
    public abstract bool RightClick();

    public abstract bool FirstSkill();
    public abstract bool FirstSkillUp();
    public abstract bool SecondSkill();
    public abstract bool ThirdSkill();
    public abstract bool Ultimate();
}