using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Stats))]
public class TankAttacks : PlayerAttack {
    [SerializeField]
    int gelatinStack = 10;
    int maxGelatinStack = Constants.MAX_GELATIN_STACK;
    float attackRatio;
    float defenseRatio;

    void ApplyGelatinBehaviour()
    {
        attackRatio = ComputeRatio(Constants.MAX_GELATIN_STACK, Constants.MAX_ATTACK_MULTIPLICATOR_GELATIN, gelatinStack);
        defenseRatio = ComputeRatio(Constants.MAX_GELATIN_STACK, Constants.MAX_DEFENSE_MULTIPLICATOR_GELATIN, gelatinStack);
        stats.AddPower(stats.GetPower() * attackRatio);
        stats.AddDefense(stats.GetDefense() * defenseRatio);
    }
    
    /*
     * ComputeRatio(10, 0, 0.5, 0, 5) return 0,25;
     */
    float ComputeRatio(float maxA, float maxB, float currentValue)
    {
        return currentValue / maxA * maxB;
    }
    
    public void Start()
    {
        stats = GetComponent<Stats>();
        ApplyGelatinBehaviour();
    }

    public void AddGelatinStack(int gelatinStackAmount)
    {
        if (gelatinStack + gelatinStackAmount >= maxGelatinStack)
        {
            gelatinStack = maxGelatinStack;
        }
        else if(gelatinStack + gelatinStackAmount <= 0){
            gelatinStack = 0;
        }
        else
        {
            gelatinStack += gelatinStackAmount;
        }
        ApplyGelatinBehaviour();
    }
}
