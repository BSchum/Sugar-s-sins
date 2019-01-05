using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TornadoSkill : Skill {

    public override bool CanCast()
    {
        return base.CanCast();
    }

    public override IEnumerator Cast()
    {
        Ray ray = new Ray(transform.position, transform.GetChild(0).transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            CmdSpawnProjectile(hit.point);
        }
        
        yield return null;
    }

    [Command]
    void CmdSpawnProjectile (Vector3 pos)
    {
        GameObject newTornado = Instantiate(skillProjectile.gameObject, pos, Quaternion.Euler(transform.forward));
        NetworkServer.Spawn(newTornado);
        TornadoProjectile tornadoProjectile = newTornado.GetComponent<TornadoProjectile>();
        tornadoProjectile.source = this.gameObject;

        MageAttack mage = GetComponent<MageAttack>();
        if (mage.isOnUlt)
        {
            tornadoProjectile.speedBonus *= 1.5f;
            tornadoProjectile.attractForce *= 2f;
        }

        tornadoProjectile.Initiate();

        ProcessCoolDown();
    }

    public override bool HasRessource()
    {
        return true;
    }
}
