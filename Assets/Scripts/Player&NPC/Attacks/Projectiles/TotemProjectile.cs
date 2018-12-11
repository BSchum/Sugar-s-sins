﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TotemProjectile : SkillProjectile, IBuffable {
    
    public GameObject source;
    [SerializeField]
    int gelatinStacksAmount;
    [SerializeField]
    float gelatinStacksRate;

    [SerializeField]
    float attackRate;

    [SerializeField]
    float range;

    bool isAttacking;
    // Use this for initialization
    #region Unity's methods
    void Start () {
        DieAfterLifeTime();
        StartCoroutine(GiveGelatinStack());
        StartCoroutine(Attack());
    }

    private void Update()
    {
        this.GetComponent<Stats>().ResetBonusStats();
        ApplyBuffs();
        int i = 0;
        if (buffs.Count <= 0)
            Debug.Log("No Buffs");
        foreach (Buff buff in buffs)
        {
            Debug.Log(gameObject.name+" -- Buff n" + i + " -- Nom : " + buff.GetType());
            i++;
        }
        
    }
    #endregion
    #region Projectile Behaviour
    IEnumerator GiveGelatinStack()
    {
        while (true)
        {
            source.GetComponent<TankAttacks>().AddGelatinStack(gelatinStacksAmount);
            yield return new WaitForSeconds(gelatinStacksRate);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
            IEnumerable<Collider> sortedColls = colliders.Where(c => c != null)
                                                         .Where(c => c.tag == Constants.ENEMY_TAG)
                                                         .OrderBy(c => Vector3.Distance(this.transform.position, c.transform.position));
            if (sortedColls.Count() > 0)
            {
                Debug.Log("Attaque du totem pour" + this.GetComponent<Stats>().GetDamage());
                sortedColls.FirstOrDefault().GetComponent<Health>().TakeDamage(this.GetComponent<Stats>().GetDamage());
            }
            yield return new WaitForSeconds(attackRate);
        }

    }
    #endregion
}
