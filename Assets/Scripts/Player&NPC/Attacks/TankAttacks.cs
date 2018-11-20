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
     * ComputeRatio(10, 0.5, 5) return 0,25;
     */
    float ComputeRatio(float maxA, float maxB, float currentValue)
    {
        return currentValue / maxA * maxB;
    }
    
    public void Start()
    {
        stats = GetComponent<Stats>();
        ApplyGelatinBehaviour();
        base.Start();

    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            base.Update();
            if (ih.FirstSkill() && skills[0].CanCast() && !skills[0].isOnCooldown)
            {
                skills[0].source = this.gameObject;
                StartCoroutine(skills[0].Cast());
                StartCoroutine(skills[0].ProcessCoolDown());
            }
        }
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
