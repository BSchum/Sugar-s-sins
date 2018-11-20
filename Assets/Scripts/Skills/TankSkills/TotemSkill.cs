using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotemSkill : Skill
{

    public override IEnumerator Cast()
    {
        Vector3 lookingRotation = source.GetComponentInChildren<Camera>().gameObject.transform.forward;
        Ray ray = new Ray(source.transform.position, lookingRotation);
        RaycastHit rHit;
        if (Physics.Raycast(ray, out rHit))
        {
            Debug.Log("Spawn that thing");
            CmdSpawnProjectile(rHit.point);
        }

        yield return false;
    }
    [Command]
    void CmdSpawnProjectile(Vector3 pos)
    {
        RpcSpawnProjectile(pos);
    }

    [ClientRpc]
    void RpcSpawnProjectile(Vector3 pos)
    {
        Debug.Log("Spawn that thing on all client");

        GameObject totem = Instantiate(skillProjectile.gameObject, pos, Quaternion.identity);
        totem.GetComponent<TotemProjectile>().source = source;
        source.GetComponent<TankAttacks>().lastActiveTotem = totem;
    }
}
