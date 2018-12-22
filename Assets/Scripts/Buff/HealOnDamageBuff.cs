﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class HealOnDamageBuff : Buff
{
    public HealOnDamageBuff(GameObject target) : base(target)
    {
        DamageReceived damageReceived = HealFromDamagePercentage;
        target.GetComponent<Stats>().Subscribe(damageReceived);
    }

    public override void ApplyBuff()
    {
    }

    public void HealFromDamagePercentage(float amount)
    {
        Debug.Log("Je recupere 10% de "+amount);
        target.GetComponent<Stats>().TakeDamage(-amount * 10 / 100);
    }
    public override bool isEnded()
    {
        return false;
    }
}

