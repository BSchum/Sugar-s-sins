using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class GelatinGenerationBuff : Buff
{
    int amount;
    float lastApply;
    float duration = 10f;
    public GelatinGenerationBuff(GameObject target, int amount) : base(target)
    {
        this.amount = amount;
        lastApply = Time.time;
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

