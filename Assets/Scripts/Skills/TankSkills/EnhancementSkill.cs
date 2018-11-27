using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class EnhancementSkill : Skill
{
    public override IEnumerator Cast()
    {
        Debug.Log("Je cast l'amélio", this);
        source.GetComponent<PlayerAttack>().AddBuff(new EnhancementBuff(source));
        Debug.Log("Je buff mes alliés local");
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
        Debug.Log("Je buff mes alliés");
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
