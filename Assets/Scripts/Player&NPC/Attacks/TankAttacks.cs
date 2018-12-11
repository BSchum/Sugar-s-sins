using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Stats))]
public class TankAttacks : PlayerAttack {
    [SerializeField]
    int gelatinStack = Constants.MAX_GELATIN_STACK;
    int maxGelatinStack = Constants.MAX_GELATIN_STACK;
    float attackRatio;
    float defenseRatio;
    public GameObject lastActiveTotem;

    public int GetGelatinStacks()
    {
        return gelatinStack;
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
        base.Start();
        stats = GetComponent<Stats>();
        buffs.Add(new GelatinBuff(this.gameObject));
        ApplyBuffs();
    }

    public void Update()
    {
        if (isLocalPlayer)
        {
            CmdInitializeSkills();
            base.Update();
            if (ih.FirstSkill() && skills[0].CanCast() && !skills[0].isOnCooldown)
            {
                StartCoroutine(skills[0].Cast());
                StartCoroutine(skills[0].ProcessCoolDown());
            }
            else if(ih.SecondSkill() && skills[1].CanCast() && !skills[1].isOnCooldown)
            {
                StartCoroutine(skills[1].Cast());
                StartCoroutine(skills[1].ProcessCoolDown());
            }
            else if(ih.ThirdSkill() && skills[2].CanCast() && !skills[2].isOnCooldown)
            {
                StartCoroutine(skills[2].Cast());
                StartCoroutine(skills[2].ProcessCoolDown());
            }
            else if(ih.Ultimate() && skills[3].CanCast() && !skills[3].isOnCooldown)
            {
                StartCoroutine(skills[3].Cast());
                StartCoroutine(skills[3].ProcessCoolDown());
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
    }
}
