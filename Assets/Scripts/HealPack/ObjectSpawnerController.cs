using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectSpawnerController : NetworkBehaviour {

    public void SpawnObject(ObjectToSpawn obj)
    {
        CmdSpawnObject(obj);
    }

    [Command]
    public void CmdSpawnObject(ObjectToSpawn obj)
    {
        //cast sur le serveur
        obj.RpcChangeState();
    }
}
