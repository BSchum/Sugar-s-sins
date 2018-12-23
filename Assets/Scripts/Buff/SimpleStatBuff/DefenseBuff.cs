using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class DefenseBuff : Buff
{
    private float amount;

    public DefenseBuff(GameObject target, float amount) : base(target)
    {
        this.amount = amount;
        this.artwork = Resources.Load<Sprite>("Icons/Buffs/DefenseBuff");
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffDefense(amount);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

