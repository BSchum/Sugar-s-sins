using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotorBase
{
    protected Camera cam;
    protected GameObject entity;
    protected bool hasCamera;
    public float speed = 10.0f;

    public abstract void Move(Vector3 movement, Vector3 rotation);

    protected void MoveEntity(Vector3 movement)
    {
        entity.transform.Translate(new Vector3(movement.x * Time.deltaTime * speed, 0, movement.z * Time.deltaTime * speed));
    }
}