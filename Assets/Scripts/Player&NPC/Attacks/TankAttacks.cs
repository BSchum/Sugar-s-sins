using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Stats))]
public class TankAttacks : PlayerAttack {
    [SerializeField]
    int gelatinStack = Constants.MAX_GELATIN_STACK;
    int maxGelatinStack = Constants.MAX_GELATIN_STACK;
    float attackRatio;
    float defenseRatio;
    public GameObject lastActiveTotem;

    int gelatinStackRatio;

    public int GetGelatinStacks()
    {
        return gelatinStack;
    }

    public void SetGelatinStackRatio(int amount)
    {
        this.gelatinStackRatio = amount;
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
            UIForDebug();
            CmdInitializeSkills();
            base.Update();
            if (ih.FirstSkill() && skills[0].CanCast() && !skills[0].isOnCooldown && skills[0].HasRessource())
            {
                StartCoroutine(skills[0].Cast());
                StartCoroutine(skills[0].ProcessCoolDown());
            }
            else if(ih.SecondSkill() && skills[1].CanCast() && !skills[1].isOnCooldown && skills[1].HasRessource())
            {
                StartCoroutine(skills[1].Cast());
                StartCoroutine(skills[1].ProcessCoolDown());
            }
            else if(ih.ThirdSkill() && skills[2].CanCast() && !skills[2].isOnCooldown && skills[2].HasRessource())
            {
                StartCoroutine(skills[2].Cast());
                StartCoroutine(skills[2].ProcessCoolDown());
            }
            else if(ih.Ultimate() && skills[3].CanCast() && !skills[3].isOnCooldown && skills[3].HasRessource())
            {
                StartCoroutine(skills[3].Cast());
                StartCoroutine(skills[3].ProcessCoolDown());
            }else if(ih.Ultimate() && skills[3].isOnCooldown && !skills[4].isOnCooldown && skills[4].CanCast() && skills[4].HasRessource())
            {
                StartCoroutine(skills[4].Cast());
                StartCoroutine(skills[4].ProcessCoolDown());
            }
        }
    }

    public void AddGelatinStack(int gelatinStackAmount)
    {
        gelatinStackAmount *= gelatinStackRatio;
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

        if(gelatinStackAmount < 0)
        {
            foreach(Skill skill in skills)
            {
                if(skill.isOnCooldown)
                    skill.internalCD += 0.2f * gelatinStackAmount;
            }
        }
    }

    public void UIForDebug()
    {
        //STATS
        string text = this.stats.ToString();
        text += "\n Gelatin Stack : " + GetGelatinStacks();

        if(lastActiveTotem != null)
            text += "\n\n Totem :" + this.lastActiveTotem.GetComponent<Stats>() + "\nLighning :" + this.lastActiveTotem.GetComponent<TotemProjectile>().lighting;
            
        Text stats = GameObject.Find("DebugUI").GetComponent<Text>();

        stats.text = text;

        //SKILLS
        Text skillUI = GameObject.Find("SkillUIDebug").GetComponent<Text>();

        string skilltext = "";

        foreach(Skill skill in skills)
        {
            skilltext += skill.ToString();
        }
        skillUI.text = skilltext;

    }
}
