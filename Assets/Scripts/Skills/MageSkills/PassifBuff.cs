using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassifBuff : Buff {

    public float energyPerDamage;
    [HideInInspector]
    public float currentEnergy;

    public void AddEnergy(float damage)
    {
        currentEnergy += damage * energyPerDamage;
        if (currentEnergy > 100)
        {
            currentEnergy = 100;
        }
    }

    public PassifBuff(GameObject target) : base(target)
    {
    }

    public override void ApplyBuff()
    {

    }

    public override bool isEnded()
    {
        return false;
    }

}
