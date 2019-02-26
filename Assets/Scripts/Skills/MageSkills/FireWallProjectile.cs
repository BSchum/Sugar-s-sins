using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireWallProjectile : SkillProjectile {

    public float reducingSpeed;
    public float increasingSpeed;

    [HideInInspector]
    public bool upgraded = false;

    public override void Initiate()
    {
        base.Initiate();
        DieAfterLifeTime();

        if(upgraded)
        {
            increasingSpeed *= 2f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyProjectile")
        {
            if(upgraded)
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.GetComponent<Rigidbody>().velocity *= reducingSpeed;
            }
        }
        else if (other.gameObject.tag == "AlliedProjectile")
        {
            other.GetComponent<Rigidbody>().velocity *= increasingSpeed;
        }

        var projectile = other.GetComponent<SkillProjectile>();
        if (projectile != null && projectile.isMoltable)
        {
            Test();
        }
    }

    private void Test()
    {
        if(!isServer)
            Debug.Log("ON SERVER");
    }

    [ClientRpc]
    private void RpcTest()
    {
        Debug.Log("ON CLIENT");
    }
}
