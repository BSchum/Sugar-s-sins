using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MageUltimateSkill : Skill {

    public float lifeSteal;
    public float duration;

    public override bool HasRessource()
    {
        MageAttack mage = GetComponent<MageAttack>();
        return mage.burstPassif >= cost;
    }

    public override IEnumerator Cast()
    {
        StartCoroutine(ProcessCoolDown());

        GetComponent<MageAttack>().burstPassif -= cost;

        CmdBuff();

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
        MageUltimateBuff ultimateBuff = new MageUltimateBuff(this.gameObject, duration, lifeSteal);
        GetComponent<PlayerAttack>().AddBuff(ultimateBuff);
    }

}
