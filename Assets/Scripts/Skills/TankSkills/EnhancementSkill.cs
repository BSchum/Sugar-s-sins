using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class EnhancementSkill : Skill
{
    public override IEnumerator Cast()
    {
        this.gameObject.GetComponent<PlayerAttack>().AddBuff(new EnhancementBuff(this.gameObject));
        CmdBuffAllies();
        yield return null;
    }

    [Command]
    public void CmdBuffAllies()
    {

        RpcBuffAllies();
    }

    [ClientRpc]
    public void RpcBuffAllies()
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, 100f);
        IEnumerable<Collider> sortedColls = colls.Where(c => c != null && c.tag == Constants.PLAYER_TAG);

        foreach (Collider coll in sortedColls)
        {
            coll.GetComponent<PlayerAttack>().AddBuff(new DamageReduceBuff(coll.gameObject));
        }
    }
    public override bool HasRessource()
    {
        return true;
    }
}
