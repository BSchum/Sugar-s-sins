using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealPack : ObjectToSpawn
{
    public GameObject healSphere;
    public float disableTime = 3f;
    [SyncVar]
    public bool healTaken = false;
    
    public void DisableHealSphere()
    {
        SetState(Destroy);
        
        if (isServer)
            healTaken = true;
            //RpcChangeState();
            StartCoroutine(Wait());
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(disableTime);
        SetState(Spawn);
        //RpcChangeState();
        healTaken = false;
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
