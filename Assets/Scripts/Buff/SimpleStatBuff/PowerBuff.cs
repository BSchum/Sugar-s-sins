using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PowerBuff : Buff
{
    int amount;
    public PowerBuff(GameObject target, int amount) : base(target)
    {
        this.amount = amount;
        artwork = Resources.Load<Sprite>("Icons/Buffs/PowerBuff");
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

