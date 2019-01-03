using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoProjectile : SkillProjectile {

    public float rotateSpeed = 20f;
    public float attractForce;

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
            Vector3 dir = (transform.position - attractedObject.transform.position) * attractForce * Time.deltaTime;

            attractedObject.transform.position += dir;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SkillProjectile projectile = other.GetComponent<SkillProjectile>();
        if (other.gameObject.tag == "Enemy" || projectile != null)
        {
            if (!attractedObjects.Contains(other.gameObject))
            {
                attractedObjects.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SkillProjectile projectile = other.GetComponent<SkillProjectile>();
        if (other.gameObject.tag == "Enemy" || projectile != null)
        {
            if (!attractedObjects.Contains(other.gameObject))
            {
                attractedObjects.Add(other.gameObject);
            }
        }
    }
}
