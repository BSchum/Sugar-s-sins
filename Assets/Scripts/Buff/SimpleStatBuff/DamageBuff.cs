using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DamageBuff : Buff
{
    float amount;
    public DamageBuff(GameObject target, float amount) : base(target)
    {
        this.amount = amount;
    }

    public override void ApplyBuff()
    {
        Debug.Log("Apply buff");
        target.GetComponent<Stats>().BuffDamage(amount);
    }

    public override bool isEnded()
    {
        //It ends when the totem is destroyed
        return false;
    }
}

