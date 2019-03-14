using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossController : EnemyController, IRessourcesManipulator {

    public float attackSpeed = 5f;
    float internalCD = 0;
    [SerializeField]
    Skill[] skills;
    public bool isCasting = false;

    public int deadFakeMira;

    public float resource = 0;

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

        //Duplication - When she is at zero ressources
        if (skills[2].CanCast()
            && !skills[2].isOnCooldown
            && skills[2].HasRessource()
            && resource <= 0
            && currentTarget != null
            && !isCasting)
        {
            Debug.Log("Duplication");
            StartCoroutine(skills[2].Cast(currentTarget));
        }
        Debug.Log(deadFakeMira);
        //Energy ray - When she is above 0 ressources
        if (skills[1].CanCast() && !skills[1].isOnCooldown && skills[1].HasRessource() && currentTarget != null && !isCasting)
        {
            StartCoroutine(skills[1].Cast(currentTarget));
        }

        //AutoAttack - Everytime she is not casting, and when she is in range
        if (!isCasting && skills[0].CanCast() && !skills[0].isOnCooldown && skills[0].HasRessource() && currentTarget != null && (currentTarget.transform.position - this.transform.position).magnitude < 6 && !isCasting)
        {
            Debug.Log("AutoAttack");
            StartCoroutine(skills[0].Cast(currentTarget));
        }


    }
    #endregion
}
