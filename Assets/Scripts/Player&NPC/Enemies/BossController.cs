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

        //if (skills[1].CanCast() && !skills[1].isOnCooldown && skills[1].HasRessource() && currentTarget != null)
        //{
        //    StartCoroutine(skills[1].Cast(currentTarget));
        //}

        if (skills[0].CanCast() && !skills[0].isOnCooldown && skills[0].HasRessource() && currentTarget != null && (currentTarget.transform.position - this.transform.position).magnitude < 4.5)
        {
            Debug.Log("AutoAttack");
            StartCoroutine(skills[0].Cast(currentTarget));
        }


        canMove = isCasting;

    }
    #endregion
}
