using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class GelatinBuff : Buff
{
    Stats stats;
    int gelatinStacks;
    private float attackRatio;
    private float defenseRatio;

    public GelatinBuff(GameObject target) : base(target)
    {

    }
    public override void ApplyBuff()
    {
        
        stats = target.GetComponent<Stats>();
        gelatinStacks = target.GetComponent<TankAttacks>().GetGelatinStacks();
        attackRatio = ComputeRatio(Constants.MAX_GELATIN_STACK, Constants.MAX_ATTACK_MULTIPLICATOR_GELATIN, gelatinStacks);
        defenseRatio = ComputeRatio(Constants.MAX_GELATIN_STACK, Constants.MAX_DEFENSE_MULTIPLICATOR_GELATIN, gelatinStacks, true);

        stats.AddPower(stats.GetPower() * attackRatio);
        stats.AddDefense(stats.GetDefense() * defenseRatio);
        Debug.Log(stats.finalPower);
        Debug.Log(stats.finalDefense);
    }

    public override bool isEnded()
    {
        //it never ends
        return false;
    }


    /*
     * ComputeRatio(10, 0.5, 5) return 0,25;
     */
    float ComputeRatio(float maxA, float maxB, float currentValue, bool inverse = false)
    {
        float result = currentValue / maxA * maxB;
        if (inverse)
            return maxB - result;
        else
            return result;
    }
}

