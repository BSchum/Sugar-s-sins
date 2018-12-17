using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PowerBuff : Buff
{
    int amount;
    float lastApply;
    float duration = 10f;
    public PowerBuff(GameObject target, int amount) : base(target)
    {
        this.amount = amount;
        lastApply = Time.time;
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffPower(amount);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

