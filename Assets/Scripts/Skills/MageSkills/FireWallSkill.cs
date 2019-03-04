using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireWallSkill : Skill {
    public Camera cam;
    public override bool HasRessource()
    {
        return true;
    }

    public override IEnumerator Cast()
    {
        StartCoroutine(ProcessCoolDown());

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            source = transform.gameObject;

            CmdSpawnProjectile(hit.point);
        }

        yield return null;
    }

    [Command]
    void CmdSpawnProjectile(Vector3 pos)
    {
        GameObject newFireWall = Instantiate(skillProjectile.gameObject, pos, source.transform.rotation);
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

        StartCoroutine(ProcessCoolDown());
    }
}
