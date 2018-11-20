using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillProjectile : NetworkBehaviour {

    public float speed, damage, lifeTime;

    private void Start()
    {
        Initiate();
    }

    public void DieAfterLifeTime()
    {
        Destroy(this.gameObject, lifeTime);
    }

    public virtual void Initiate()
    {
        SpawnOnServer();
    }

    public virtual void SpawnOnServer()
    {
        NetworkServer.Spawn(this.gameObject);
    }

    public virtual void ProjectileBehav ()
    {

    }
}
