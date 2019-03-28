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
        Debug.Log("Hello");
        GameObject rain = Instantiate(rainPrefab, currentTarget.transform.position, Quaternion.identity);
    }
    public override bool HasRessource()
    {
        return true;
    }
}
