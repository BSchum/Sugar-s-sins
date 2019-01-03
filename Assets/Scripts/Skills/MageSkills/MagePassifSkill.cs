using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MagePassifSkill : Skill {

    public float energyPerDamage;
    [HideInInspector]
    public float currentEnergy;

    public override bool HasRessource()
    {
        return energyPerDamage == 100;
    }

    public override IEnumerator Cast()
    {
        currentEnergy = 0;
        yield return null;
    }

    public void AddEnergy (float damage)
    {
        currentEnergy += damage * energyPerDamage;
        if(currentEnergy > 100)
        {
            currentEnergy = 100;
        }
    }
}
