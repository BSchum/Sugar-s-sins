using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpeedBuff : Buff
{
    float speedAmount;
    public SpeedBuff(GameObject target, float amount) : base(target)
    {
        speedAmount = amount;
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffSpeed(speedAmount);
    }

    public override bool isEnded()
    {
        return Time.time > lastApply + duration;
    }
}

