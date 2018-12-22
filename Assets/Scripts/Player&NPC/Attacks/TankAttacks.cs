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

    /*
     * ComputeRatio(10, 0.5, 5) return 0,25;
     */
    #region Unity's method
    public void Start()
    {
        base.Start();
        stats = GetComponent<Stats>();
        //TODO Ne plus utiliser le buff de gelatin comme un buff, mais du code pr�cis pour le tank
        //TODO Transformer le systeme de stats afin de les recalculer seulement si on les buff ou non.
        //TODO Afin de palier au systeme de latence sur un calcul en temps reel
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
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Je prend des degats");
                this.GetComponent<Health>().TakeDamage(20);
            }
        }
    }
    #endregion
    #region Gelatin behaviour
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

    public int GetGelatinStacks()
    {
        return gelatinStack;
    }

    public void SetGelatinStackRatio(int amount)
    {
        this.gelatinStackRatio = amount;
    }
    #endregion
    #region Helpers
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

        //BUFFS
        Text BuffUI = GameObject.Find("BuffDebug").GetComponent<Text>();
        string bufftext = "";
        int i = 0;
        foreach (Buff buff in buffs)
        {
            bufftext += "\n"+gameObject.name+" -- Buff n" + i + " -- Nom : " + buff.GetType();
            i++;
        }

        BuffUI.text = bufftext;


    }
    float ComputeRatio(float maxA, float maxB, float currentValue)
    {
        return currentValue / maxA * maxB;
    }
    #endregion

}
