using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireWallSkill : Skill {

    public override bool HasRessource()
    {
        return true;
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
    void CmdSpawnProjectile(Vector3 pos)
    {
        GameObject newFireWall = Instantiate(skillProjectile.gameObject, pos, Quaternion.identity);
        Vector3 fireWallPos = transform.position;
        fireWallPos.y = newFireWall.transform.position.y;
        newFireWall.transform.LookAt(fireWallPos);
       
        FireWallProjectile fireWallProjectile = newFireWall.GetComponent<FireWallProjectile>();
        fireWallProjectile.source = this.gameObject;

        MageAttack mage = GetComponent<MageAttack>();
        if (mage.isOnUlt)
        {
            fireWallProjectile.upgraded = true;
        }

        fireWallProjectile.Initiate();

        ProcessCoolDown();
    }


}
