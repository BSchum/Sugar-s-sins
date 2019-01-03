using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallProjectile : SkillProjectile {

    public float reducingSpeed;
    public float increasingSpeed;

    public override void Initiate()
    {
        base.Initiate();
        DieAfterLifeTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyProjectile")
        {
            other.GetComponent<Rigidbody>().velocity *= reducingSpeed;
        }
        else if (other.gameObject.tag == "AlliedProjectile")
        {
            other.GetComponent<Rigidbody>().velocity *= increasingSpeed;
        }
    }
}
