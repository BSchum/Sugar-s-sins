using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealPack : ObjectToSpawn
{
    public GameObject healSphere;
    public float disableTime = 3f;
    
    public void DisableHealSphere(ObjectSpawnerController osc)
    {
        SetState(Destroy);
        RpcChangeState();
        StartCoroutine(Wait());
        osc.CmdSpawnObject(gameObject);
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(disableTime);
        SetState(Spawn);
        RpcChangeState();
    }
    
    protected override void Spawn()
    {
        healSphere.SetActive(true);
    }

    protected override void Destroy()
    {
        healSphere.SetActive(false);
    }
}
