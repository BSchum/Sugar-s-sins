using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossController : EnemyController {

    public float attackSpeed = 5f;
    float internalCD = 0;
    [SerializeField]
    Skill[] skills;
    public bool isCasting = false;
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
        if(!isCasting)
            GoToHightestThreat();
        //if (skills[0].CanCast() && !skills[0].isOnCooldown && skills[0].HasRessource() && currentTarget != null)
        //{
        //    StartCoroutine(skills[0].Cast(currentTarget));
        //}
        Debug.Log("UD");
        if (skills[1].CanCast() && !skills[1].isOnCooldown && skills[1].HasRessource() && currentTarget != null)
        {
            Debug.Log("Je cast le ray");
            StartCoroutine(skills[1].Cast(currentTarget));
        }
    }
    #endregion
}
