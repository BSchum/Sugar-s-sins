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
            GameObject newFireWall = Instantiate(skillProjectile.gameObject, hit.point, transform.rotation);
            NetworkServer.Spawn(newFireWall);
            SkillProjectile fireWallProjectile = newFireWall.GetComponent<SkillProjectile>();

            fireWallProjectile.Initiate();

            ProcessCoolDown();
        }

        yield return null;
    }
}
