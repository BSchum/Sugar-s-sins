using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoProjectile : SkillProjectile {

    public float rotateSpeed = 20f;
    public float attractForce;
    public float speedBonus;

    List<GameObject> attractedObjects = new List<GameObject>();

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
            transform.position += transform.forward * speed;
            transform.Rotate(Vector3.up * (rotateSpeed /** (movingForward ? 1 : -1)*/ / lifeTime));

            spawnTime -= Time.deltaTime;

            movingForward = spawnTime < lifeTime / 2 ? false : true;

            Attracts();

            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }

    void Attracts ()
    {
        foreach (GameObject attractedObject in attractedObjects)
        {
            if(attractedObject.tag == "Enemy" || attractedObject.tag == "EnemyProjectile")
            {
                Vector3 dir = (transform.position - attractedObject.transform.position) * attractForce * Time.deltaTime;

                attractedObject.transform.position += dir;
            }
            else if (attractedObject.tag == "Player")
            {
                attractedObject.GetComponent<Rigidbody>().velocity *= speedBonus;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyProjectile" || other.gameObject.tag == "Player")
        {
            if (!attractedObjects.Contains(other.gameObject))
            {
                attractedObjects.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyProjectile" || other.gameObject.tag == "Player")
        {
            if (!attractedObjects.Contains(other.gameObject))
            {
                attractedObjects.Add(other.gameObject);
            }
        }
    }
}
