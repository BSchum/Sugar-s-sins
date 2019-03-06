using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TornadoSkill : Skill {
    public Camera cam;

    public override bool CanCast()
    {
        return base.CanCast();
    }

    public override bool HasRessource()
    {
        return true;
    }

    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            CmdSpawnProjectile(hit.point);
        }

        yield return null;
    }

    [Command]
    void CmdSpawnProjectile (Vector3 pos)
    {
        RpcSpawnProjectile(pos);
    }

    [ClientRpc]
    void RpcSpawnProjectile(Vector3 pos)
    {
        GameObject newTornado = Instantiate(skillProjectile.gameObject, pos, Quaternion.Euler(transform.forward));

        TornadoProjectile tornadoProjectile = newTornado.GetComponent<TornadoProjectile>();
        tornadoProjectile.source = this.gameObject;

        MageAttack mage = GetComponent<MageAttack>();
        if (mage.isOnUlt)
        {
            tornadoProjectile.bonusSpeed *= 1.5f;
            tornadoProjectile.attractForce *= 2f;
        }

        tornadoProjectile.Initiate();

        StartCoroutine(ProcessCoolDown());
    }
}
