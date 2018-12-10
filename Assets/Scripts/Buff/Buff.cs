using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Buff
{
    public Buff(GameObject target)
    {
        this.target = target;
    }
    public GameObject target;
    public abstract void ApplyBuff();

    public abstract bool isEnded();

    protected bool isApplied = false;

}

