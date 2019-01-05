﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallProjectile : SkillProjectile {

    public float reducingSpeed;
    public float increasingSpeed;

    [HideInInspector]
    public bool upgraded = false;

    public override void Initiate()
    {
        base.Initiate();
        DieAfterLifeTime();

        if(upgraded)
        {
            increasingSpeed *= 2f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyProjectile")
        {
            if(upgraded)
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.GetComponent<Rigidbody>().velocity *= reducingSpeed;
            }
        }
        else if (other.gameObject.tag == "AlliedProjectile")
        {
            other.GetComponent<Rigidbody>().velocity *= increasingSpeed;
        }
    }
}