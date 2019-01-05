using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : SkillProjectile {

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public override void Throw ()
    {
        transform.SetParent(null);
        GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);

        DieAfterLifeTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Calcul des dégats
        if(collision.gameObject.tag == Constants.ENEMY_TAG)
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            float velSpeed = vel.x + vel.y + vel.z;

            damage += velSpeed;
            source.GetComponent<MageAttack>().AddBurstPassif(damage);
            source.GetComponent<Stats>().AddCurrentHealth(damage * source.GetComponent<Stats>().GetLifeSteal());
        }
    }
}
