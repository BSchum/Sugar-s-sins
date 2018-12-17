using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotemSkill : Skill
{

    public override IEnumerator Cast()
    {
        Vector3 lookingRotation = gameObject.GetComponentInChildren<Camera>().gameObject.transform.forward;
        Ray ray = new Ray(gameObject.transform.position, lookingRotation);
        RaycastHit rHit;
        if (Physics.Raycast(ray, out rHit))
        {
            CmdSpawnProjectile(rHit.point);
        } 
       
        yield return false;
    }

    public override bool HasRessource()
    {
        if (gameObject.GetComponent<TankAttacks>().GetGelatinStacks() >= this.cost)
        {
            gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);
            return true;
        }
        return false;    
    }

    [Command]
    void CmdSpawnProjectile(Vector3 pos)
    {
        RpcSpawnProjectile(pos);
    }

    [ClientRpc]
    void RpcSpawnProjectile(Vector3 pos)
    {
        GameObject totem = Instantiate(skillProjectile.gameObject, pos, Quaternion.identity);
        totem.GetComponent<TotemProjectile>().source = gameObject;
        gameObject.GetComponent<TankAttacks>().lastActiveTotem = totem;
    }
}
