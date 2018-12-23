using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Buff
{
    protected float lastApply;
    protected float duration = 10f;
    public GameObject target;
    public Sprite artwork;
    public Buff(GameObject target)
    {
        lastApply = Time.time;
        this.target = target;
    }

    public abstract void ApplyBuff();

    public abstract bool isEnded();

    protected bool isApplied = false;

    public override string ToString()
    {
        return base.ToString();
    }
    public BuffForUI GetBuffAsUIObject()
    {
        return new BuffForUI(lastApply, artwork, duration);
    }
}



