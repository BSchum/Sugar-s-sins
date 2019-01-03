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
            GameObject newTornado = Instantiate(skillProjectile.gameObject, hit.point, Quaternion.Euler(transform.forward));
            NetworkServer.Spawn(newTornado);
            SkillProjectile tornadoProjectile = newTornado.GetComponent<SkillProjectile>();

            tornadoProjectile.Initiate();

            ProcessCoolDown();
        }
        
        yield return null;
    }

    public override bool HasRessource()
    {
        return true;
    }
}
