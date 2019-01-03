using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MageUltimateSkill : Skill {

    public float lifeSteal;
    public float duration;

    public override bool HasRessource()
    {
        return true;
    }

    public override IEnumerator Cast()
    {
        yield return null;
    }

    [Command]
    public void CmdBuff()
    {
        RpcBuff();
    }

    [ClientRpc]
    public void RpcBuff()
    {
         GetComponent<PlayerAttack>().AddBuff(new DamageReduceBuff(gameObject));
    }

}
