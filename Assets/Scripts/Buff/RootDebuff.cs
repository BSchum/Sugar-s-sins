using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RootDebuff : Buff
{
    
    public RootDebuff(GameObject target) : base(target)
    {
        this.duration = 4f;
    }

    public override void ApplyBuff()
    {
        target.GetComponent<PlayerMove>().isRooted = true;
    }

    public override bool isEnded()
    {
        if (Time.time > lastApply + duration)
        {
            target.GetComponent<PlayerMove>().isRooted = false;
        }
        return Time.time > lastApply + duration;
    }
}

