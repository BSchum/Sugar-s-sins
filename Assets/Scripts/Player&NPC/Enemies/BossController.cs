using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BossController : EnemyController, IRessourcesManipulator {

    public float attackSpeed = 5f;
    float internalCD = 0;

    public bool isCasting = false;

    private Skill selectedSkill;
    public float resource = 0;

    float timeBetweenSpells = 10;
    float lastSpell;

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
            //Mirror Beam - When she is above 0 ressources
            if (skills[5].CanCast() && !skills[5].isOnCooldown && skills[5].HasRessource() && currentTarget != null && !isCasting)
            {
                StartCoroutine(skills[5].Cast());
            }

            //Cone de Cristal suivi de Pluie de cristaux - When she is above 0 ressources and current target is in range
            else if (skills[3].CanCast() && !skills[3].isOnCooldown && skills[3].HasRessource() && currentTarget != null && !isCasting)
            {
                StartCoroutine(skills[3].Cast(currentTarget));
                StartCoroutine(skills[4].Cast(currentTarget));
            }
            //Energy ray - When she is above 0 ressources
            else if (skills[1].CanCast() && !skills[1].isOnCooldown && skills[1].HasRessource() && currentTarget != null && !isCasting)
            {
                StartCoroutine(skills[1].Cast(currentTarget));
            }
            //Duplication - When she is at zero ressources
            else if (skills[2].CanCast()
                && !skills[2].isOnCooldown
                && skills[2].HasRessource()
                && resource <= 10
                && currentTarget != null
                && !isCasting)
            {
                StartCoroutine(skills[2].Cast(currentTarget));
            }
        }
        //AutoAttack - Everytime she is not casting, and when she is in range
        if (!isCasting && skills[0].CanCast() && !skills[0].isOnCooldown && skills[0].HasRessource() && currentTarget != null && (currentTarget.transform.position - this.transform.position).magnitude < 10 && !isCasting)
        {
            StartCoroutine(skills[0].Cast(currentTarget));
        }
    }
    #endregion

    private Skill chooseSpell()
    {
        return skills[UnityEngine.Random.Range(0, skills.Length)];
    }
}
