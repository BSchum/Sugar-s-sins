using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class GelatinGenerationBuff : Buff
{
    int amount;
    public GelatinGenerationBuff(GameObject target, int amount) : base(target)
    {
        this.amount = amount;
        this.artwork = Resources.Load<Sprite>("Icons/Buffs/GelatinGenerationBuff");
    }

    public override void ApplyBuff()
    {
        target.GetComponent<TankAttacks>().SetGelatinStackRatio(amount);
    }

    public override bool isEnded()
    {
        if (Time.time > lastApply + duration) { 
            target.GetComponent<TankAttacks>().SetGelatinStackRatio(1);
        }
        return Time.time > lastApply + duration;
    }
}

