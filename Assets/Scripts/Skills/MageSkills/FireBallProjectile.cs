using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBallProjectile : SkillProjectile {

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public override void Throw ()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);

        DieAfterLifeTime();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Calcul des dégats
        if(collision.gameObject.tag == Constants.ENEMY_TAG)
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            float velSpeed = vel.x + vel.y + vel.z;
            Debug.Log("Fireball damage : " + damage + " : " + velSpeed);
            damage += velSpeed;
            source.GetComponent<MageAttack>().AddBurstPassif(damage);
            source.GetComponent<Stats>().AddCurrentHealth(damage * source.GetComponent<Stats>().GetLifeSteal());
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    
}
