using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBallProjectile : SkillProjectile, IThreatable {
    bool canTrigger;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public override void Throw ()
    {
        canTrigger = true;
        transform.SetParent(null);
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        DieAfterLifeTime();
    }

    private void OnTriggerEnter(Collider collision)
    {

        //Calcul des dégats
        if(canTrigger && (collision.gameObject.tag == Constants.ENEMY_TAG || collision.gameObject.tag == Constants.BOSS_TAG))
        {
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            Debug.Log("Speed = " + speed);
            float velSpeed = (speed / 10);

            damage += velSpeed;
            Debug.Log("Fireball damage : " + damage + " : " + velSpeed);


            if (source != null)
            {
                source.GetComponent<MageAttack>().AddBurstPassif(damage);
                if(source.GetComponent<Stats>().GetLifeSteal() != 0)
                    source.GetComponent<Health>().TakeDamage(-damage * source.GetComponent<Stats>().GetLifeSteal());
            }
            GenerateThreat(collision.GetComponent<EnemyController>());
            Debug.Log(damage);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void GenerateThreat(EnemyController enemy)
    {
        enemy.AddThreatFor(source, 10);
    }
}
