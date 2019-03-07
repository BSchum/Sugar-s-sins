using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;

/// <summary>
/// This is the 4th spell of the the tank, berserker
/// - Buff Damage, Defense, MaxHealth
/// - Buff Damage, Defense, Maxhealth of his current totem
/// - Double the number of gelatin stacks generation
/// Buff player 
/// </summary>
public class BerserkSkill : Skill
{
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);
        TankAttacks player = this.gameObject.GetComponent<TankAttacks>();
        player.AddBuff(new PowerBuff(this.gameObject, 10));
        player.AddBuff(new DefenseBuff(this.gameObject, 10));
        player.AddBuff(new MaxHealthBuff(this.gameObject, 20));
        player.AddBuff(new GelatinGenerationBuff(this.gameObject, 2));
        CmdBuffTotem();
        yield return null;
    }

    [Command]
    public void CmdBuffTotem()
    {
        RpcBuffTotem();
    }
    [ClientRpc]
    private void RpcBuffTotem()
    {
        if(this.gameObject.GetComponent<TankAttacks>().lastActiveTotem != null)
        {
            TotemProjectile totem = this.gameObject.GetComponent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>();
            totem.AddBuff(new DamageBuff(totem.gameObject, 50));
            totem.AddBuff(new DefenseBuff(totem.gameObject, 50));
            totem.AddBuff(new MaxHealthBuff(totem.gameObject, 50));
        }
    }

    public override bool HasRessource()
    {
        if (gameObject.GetComponent<TankAttacks>().GetGelatinStacks() >= this.cost)
        {
            return true;
        }
        return false;
    }
}
