using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillProjectile : NetworkBehaviour {

    public float projectileSpeed;

    public float projectileDamage;

    public GameObject projectilePrefab;


    public void Initiate()
    {
        var rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * projectileSpeed * 250);
        transform.localScale *= projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitEntity = collision.transform.gameObject;
        if(hitEntity.tag == Constants.ENEMY_TAG)
        {
            //remove life
            //self destroy
        }
    }
}
