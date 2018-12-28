using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotemSkill : Skill
{
    public Camera cam;
    public override IEnumerator Cast()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

        gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);

        Vector3 lookingRotation = gameObject.GetComponentInChildren<Camera>().gameObject.transform.forward;
        //Ray ray = new Ray(gameObject.transform.position, lookingRotation);
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit rHit;
        if (Physics.Raycast(ray, out rHit))
        {
            StartCoroutine(ProcessCoolDown());
            CmdSpawnProjectile(rHit.point);
        }

       
        yield return false;
    }

    public override bool HasRessource()
    {
        return gameObject.GetComponent<TankAttacks>().GetGelatinStacks() >= this.cost;
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
