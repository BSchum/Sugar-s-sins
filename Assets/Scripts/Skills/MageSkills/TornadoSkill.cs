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

    public override IEnumerator Cast(GameObject target = null)
    {
        StartCoroutine(ProcessCoolDown());

        Ray ray = new Ray(transform.position, cam.transform.forward);
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

    public override bool HasRessource()
    {
        return true;
    }
}
