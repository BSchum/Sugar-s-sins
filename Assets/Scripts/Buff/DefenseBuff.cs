using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class DefenseBuff : Buff
{
    public DefenseBuff(GameObject target) : base(target)
    {
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffDefense(10);
    }

    public override bool isEnded()
    {
        return false;
    }
}

