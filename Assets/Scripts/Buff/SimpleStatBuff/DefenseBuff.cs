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
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffDefense(amount);
    }

    public override bool isEnded()
    {
        return false;
    }
}

