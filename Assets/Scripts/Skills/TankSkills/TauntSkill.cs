using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class TauntSkill : Skill, IThreatable {
    public int range = 5;
    public override IEnumerator Cast()
    {
        
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
        IEnumerable<Collider> sortedColls = colliders.Where(c => c != null)
                                                     .Where(c => c.tag == Constants.ENEMY_TAG)
                                                     .OrderBy(c => Vector3.Distance(this.transform.position, c.transform.position));

        foreach(Collider coll in sortedColls)
        {
            CmdGenerateThreat(coll.gameObject);
        }

        GetComponent<PlayerAttack>().AddBuff(new HealOnDamageBuff(this.gameObject));
        yield return true;
    }
    [Command]
    public void CmdGenerateThreat(GameObject go)
    {
        go.GetComponent<EnemyController>().AddThreatFor(this.gameObject, threat);
    }
    public void GenerateThreat(EnemyController enemy)
    {
        enemy.AddThreatFor(this.gameObject, threat);
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
}
