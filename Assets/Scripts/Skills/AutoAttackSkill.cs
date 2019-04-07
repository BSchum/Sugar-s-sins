using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;


class AutoAttackSkill : Skill
{
    GameObject currentTarget;
    public override IEnumerator Cast(GameObject target)
    {
        source.GetComponent<EnemyController>().isCasting = true;
        StartCoroutine(ProcessCoolDown());
        target.GetComponent<Health>().TakeDamage(damage);
        yield return new WaitForSeconds(1f);
        source.GetComponent<EnemyController>().isCasting = false;

    }

    public override bool HasRessource()
    {
        return true;
    }
}

