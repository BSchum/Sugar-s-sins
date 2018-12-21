using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MotorBase
{
    public Motor(GameObject entity)
    {
        this.entity = entity;
        if(entity.GetComponentInChildren<Camera>() != null)
        {
            this.cam = entity.GetComponentInChildren<Camera>();
            hasCamera = true;
        }
    }

    public override void Move(Vector3 movement, Vector3 rotation)
    {
        MoveLookDirection(rotation);
        MoveEntity(movement);
    }

    void MoveLookDirection(Vector3 rotation)
    {
        if (hasCamera)
        {
            if ( rotation.x > 0)
            {
                Debug.Log("Up");
            } else
            {
                Debug.Log("Down");
            }
            Debug.Log(cam.transform.parent.localRotation.x);
            if(cam.transform.parent.localRotation.x > -0.3)
            {
                if (cam.transform.parent.localRotation.x < 0.3)
                {
                    cam.transform.parent.Rotate(new Vector3(-rotation.x, 0, 0));
                } else if(rotation.x > 0)
                {
                    cam.transform.parent.Rotate(new Vector3(-rotation.x, 0, 0));
                }
            }
            else if (rotation.x < 0)
            {
                cam.transform.parent.Rotate(new Vector3(-rotation.x, 0, 0));
            }


            /*if (cam.transform.parent.rotation..x - rotation.x > 30)
            {
                rotation.x = 30 - cam.transform.parent.rotation.eulerAngles.x;
            } else if (cam.transform.parent.rotation.localEulerAngles.x - rotation.x < -30)
            {
                rotation.x = -30 - cam.transform.parent.rotation.eulerAngles.x;
            }*/
            //rotation.x = Mathf.Clamp(rotation.x, -30f, 30f);
        }
        entity.transform.Rotate(new Vector3(0, rotation.y, 0));
    }
}
