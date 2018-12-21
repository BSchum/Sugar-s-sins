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
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffMaxHealth(amount);
    }

    public override bool isEnded()
    {
        return false;
    }
}

