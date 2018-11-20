
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
    public abstract Vector3 ComputeRotation();
    public abstract bool SimpleAttackInput();
    public abstract bool FirstSkill();
    public abstract bool SecondSkill();
}