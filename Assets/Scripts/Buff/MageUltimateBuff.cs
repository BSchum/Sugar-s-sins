using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageUltimateBuff : Buff
{
    public float duration;
    public float lifeSteal;

    public MageUltimateBuff (GameObject target, float duration, float lifeSteal) : base(target)
    {
        this.duration = duration;
        this.lifeSteal = lifeSteal;
    }

    public override void ApplyBuff()
    {
        duration -= Time.deltaTime;

        target.GetComponent<MageAttack>().isOnUlt = true;
        target.GetComponent<Stats>().BuffLifeSteal(lifeSteal);
    }

    public override bool isEnded()
    {
        if(duration <= 0)
        {
            target.GetComponent<MageAttack>().isOnUlt = false;
        }
        return duration <= 0;
    }
}
