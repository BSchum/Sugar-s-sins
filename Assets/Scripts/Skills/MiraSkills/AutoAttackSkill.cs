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
        StartCoroutine(ProcessCoolDown());
        target.GetComponent<Health>().TakeDamage(10);
        yield return null;
    }

    public override bool HasRessource()
    {
        return true;
    }
}

