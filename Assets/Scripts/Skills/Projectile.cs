using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {

    public SkillProjectile info;

    public void Initiate ()
    {
        SpawnOnServer();
        StartCoroutine(info.DieAfterSecond(this.gameObject));
    }

    public void SpawnOnServer ()
    {
        NetworkServer.Spawn(this.gameObject);
    }
}
