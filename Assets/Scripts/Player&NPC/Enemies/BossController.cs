using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossController : EnemyController {
    
    float internalCD = 0;
    [SerializeField]
    Skill[] skills;
    public bool isCasting = false;

    #region Unity's method
    void Start () {
        base.Start();
        UIManager.instance.BossHPSubscribe(this.stats);
        foreach(Skill skill in skills)
        {
            skill.source = this.gameObject;
        }
    }
    
    void Update () {
        base.Update();

        if(currentTarget == null)
        {
            return;
        }
        
        if (skills[0].CanCast() && !skills[0].isOnCooldown && skills[0].HasRessource() && (currentTarget.transform.position - this.transform.position).magnitude <= skills[0].GetRange())
        {
            if(skills[0].useProjectors)
            {
                skills[0].CastProjector();
                StartCoroutine(skills[0].Cast(currentTarget));
            }
            else
            {
                StartCoroutine(skills[0].Cast(currentTarget));
            }
        }

        if (skills[1].CanCast() && !skills[1].isOnCooldown && skills[1].HasRessource())
        {
            if(skills[1].useProjectors)
            {
                skills[1].CastProjector();
                StartCoroutine(skills[1].Cast(currentTarget));
            }
            else
            {
                StartCoroutine(skills[1].Cast(currentTarget));
            }
        }
        canMove = !isCasting;
    }
    #endregion
}
