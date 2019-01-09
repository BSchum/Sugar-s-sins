using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class MaxHealthBuff : Buff
{
    private float amount;

    public MaxHealthBuff(GameObject target, float amount) : base(target)
    {
        this.amount = amount;
        this.artwork = Resources.Load<Sprite>("Icons/Buffs/Buff_MaxHealth");
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffMaxHealth(amount);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

