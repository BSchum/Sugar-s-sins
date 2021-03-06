﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TornadoProjectile : SkillProjectile {

    public bool drawAttractSphere;
    public Color attractSphereColor;
    public float rotateSpeed = 20f;
    public float attractForce;
    public float attractRange;

    public bool drawSpeedSphere;
    public Color speedSphereColor;
    public float bonusSpeed;
    public float bonusRange;

    public override void Initiate ()
    {
        StartCoroutine(BehaveMove());
    }

    IEnumerator BehaveMove ()
    {
        bool movingForward = true;

        float spawnTime = lifeTime;

        while (spawnTime > 0)
        {

            spawnTime -= Time.deltaTime;
            Attracts();

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    void Attracts ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attractRange);
        foreach (Collider c in colliders)
        {
            if (c.tag == Constants.ENEMY_TAG || c.tag == "EnemyProjectile" )
            {
                Vector3 dir = (transform.position - c.transform.position) * attractForce * Time.deltaTime;
                c.transform.position += dir;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Debug.Log("Hello");
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody>().velocity += Vector3.one * bonusSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody>().velocity -= Vector3.one * bonusSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawAttractSphere)
        {
            Gizmos.color = attractSphereColor;
            Gizmos.DrawWireSphere(transform.position, attractRange);
        }
        if(drawSpeedSphere)
        {
            Gizmos.color = speedSphereColor;
            Gizmos.DrawWireSphere(transform.position, bonusRange);
        }
        
    }
}
