using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawnerController : NetworkBehaviour
{
    [Command]
    public void CmdSpawnObject(GameObject obj)
    {
        ObjectToSpawn ots = obj.GetComponent<ObjectToSpawn>();
        ots.RpcChangeState();
    }
}
