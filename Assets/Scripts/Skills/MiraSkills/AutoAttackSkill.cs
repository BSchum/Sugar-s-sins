using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;


class AutoAttackSkill : Skill
{
    public float range = 3f;
    public float damage = 10f;
    public float castTime = 1f;

    GameObject currentTarget;
    public override IEnumerator Cast(GameObject target)
    {
        source.GetComponent<BossController>().isCasting = true;

        yield return new WaitForSeconds(castTime);

        source.GetComponent<BossController>().isCasting = false;

        projector.gameObject.SetActive(false);

        StartCoroutine(ProcessCoolDown());
        
        if ((target.transform.position - source.transform.position).magnitude <= range)
            target.GetComponent<Health>().TakeDamage(damage);
    }

    public override bool HasRessource()
    {
        return true;
    }

    public override float GetRange()
    {
        return range;
    }
}

