using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject.tag == "EnemyProjectile")
        {
            Debug.Log("Enemy Projectile");
            if(upgraded)
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.GetComponent<SkillProjectile>().speed *= reducingSpeed;
            }
        }
        else if (other.gameObject.tag == "AlliedProjectile")
        {
            Debug.Log("Allied Projectile");

            other.GetComponent<SkillProjectile>().speed *= increasingSpeed;
        }
    }
}
