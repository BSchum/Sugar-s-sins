using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BossController : EnemyController, IRessourcesManipulator {

    public enum Skills
    {
        EnergyRay = 0,
        MirrorBeam = 1,
        CristalCone = 2,
        CristalRain = 3,
        CristalHorde = 4,
        Duplication = 5
    }
    private int currentSpell = 0;
    public List<Skills> skillOrder;
    public float attackSpeed = 5f;
    float internalCD = 0;


    private Skill selectedSkill;
    public float resource = 100;

    float timeBetweenSpells = 5;
    float lastSpell;
    private bool hasCastCristalHorde = false;

    public float CurrentRessourceValue
    {
        get
        {
            return resource;
        }
    }

    public float MaxRessourceValue {
        get {
            return 100;
        }
    }
    #region Unity's method
    // Use this for initialization
    void Start () {
        base.Start();
        UIManager.instance.BossHPSubscribe(this.stats);
        foreach(Skill skill in skills)
        {
            skill.source = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update () {
        base.Update();
        if (isCasting || !canMove)
        {
            GetComponent<BaseAnimation>().Stay();
        }


        if (FakeMiraController.aliveFakeMira == 0)
        {
            canMove = true;
            isCasting = false;
            FakeMiraController.aliveFakeMira = -1;
        }

        if(timeBetweenSpells + lastSpell < Time.time)
        {
            lastSpell = Time.time;
            //Duplication - When she is at zero ressources
            if (CanCastSkill(Skills.Duplication) && this.resource < 10)
            {
                StartCoroutine(GetSkill(Skills.Duplication).Cast(currentTarget));
            }
            //Cristal Horde - Quand elle est a 50%
            if(CanCastSkill(Skills.CristalHorde) && this.stats.GetHealth() <= this.stats.GetMaxHealth() / 2 && !hasCastCristalHorde)
            {
                hasCastCristalHorde = true;
                StartCoroutine(GetSkill(Skills.CristalHorde).Cast(sources.LastOrDefault().Key));
            }
            //Energy ray -When she is above 0 ressources
            else if (CanCastSkill(Skills.EnergyRay))
            {
                StartCoroutine(GetSkill(Skills.EnergyRay).Cast(currentTarget));
            }
            //Mirror Beam - When she is above 0 ressources
            else if (CanCastSkill(Skills.MirrorBeam))
            {
                StartCoroutine(GetSkill(Skills.MirrorBeam).Cast());
            }
            //Cone de Cristal suivi de Pluie de cristaux - When she is above 0 ressources
            else if (CanCastSkill(Skills.CristalCone))
            {
                StartCoroutine(GetSkill(Skills.CristalCone).Cast(currentTarget));
                StartCoroutine(GetSkill(Skills.CristalRain).Cast(currentTarget));
            }

        }
    }
    #endregion
    private bool CanCastSkill(Skills skill)
    {
        int skillIndex = (int)skill;
        return skills[skillIndex].CanCast() && !skills[skillIndex].isOnCooldown && skills[skillIndex].HasRessource() && !isCasting && currentTarget != null;
    }

    private Skill GetSkill(Skills skill)
    {
        return skills[(int) skill];
    }
}
