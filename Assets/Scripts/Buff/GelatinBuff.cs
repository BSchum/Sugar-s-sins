using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class GelatinBuff : Buff
{
    Stats stats;
    int gelatinStacks;
    private float attackRatio;
    private float defenseRatio;

    public GelatinBuff(GameObject target) : base(target)
    {
        duration = 0;
    }
    public override void ApplyBuff()
    {
        stats = target.GetComponent<Stats>();
        gelatinStacks = target.GetComponent<TankAttacks>().GetGelatinStacks();
        attackRatio = Helpers.ComputeRatio(Constants.MAX_GELATIN_STACK, Constants.MAX_ATTACK_MULTIPLICATOR_GELATIN, gelatinStacks);
        defenseRatio = Helpers.ComputeRatio(Constants.MAX_GELATIN_STACK, Constants.MAX_DEFENSE_MULTIPLICATOR_GELATIN, gelatinStacks, true);
        stats.BuffPower(stats.GetCurrentPower() * attackRatio);
        stats.BuffDefense(stats.GetCurrentDefense() * defenseRatio);
    }

    public override bool isEnded()
    {
        //it never ends
        return false;
    }



}

