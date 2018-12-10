using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DamageBuff : Buff
{
    public DamageBuff(GameObject target) : base(target)
    {
    }

    public override void ApplyBuff()
    {
        target.GetComponent<Stats>().BuffDamage(10);
    }

    public override bool isEnded()
    {
        //It ends when the totem is destroyed
        return false;
    }
}

