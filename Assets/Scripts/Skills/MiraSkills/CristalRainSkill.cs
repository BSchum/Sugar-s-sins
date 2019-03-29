using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CristalRainSkill : Skill
{
    public GameObject rainPrefab;
    public GameObject rainSpawn;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        CmdSpawnRain(currentTarget);
        yield return null;
    }

    [Command]
    void CmdSpawnRain(GameObject currentTarget)
    {
        RpcSpawnRain(currentTarget);
    }

    [ClientRpc]
    void RpcSpawnRain(GameObject currentTarget)
    {
        GameObject rain = Instantiate(rainPrefab, rainSpawn.transform.position, Quaternion.identity);
    }
    public override bool HasRessource()
    {
        return true;
    }
}
