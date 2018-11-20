using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillProjectile : NetworkBehaviour {

    public float speed, damage, lifeTime;

    public GameObject projectilePrefab;

    private void Start()
    {
        Initiate();
    }

    public IEnumerator DieAfterSecond (GameObject gameobject)
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameobject);
    }

    public virtual void Initiate()
    {
        SpawnOnServer();
        Debug.Log("Papa");
    }

    public virtual void SpawnOnServer()
    {
        NetworkServer.Spawn(this.gameObject);
    }

    public virtual void ProjectileBehav ()
    {

    }
}
