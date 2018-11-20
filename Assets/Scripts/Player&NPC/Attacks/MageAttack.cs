﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class MageAttack : PlayerAttack {

    float burstPassif = 0;
    private const int burstMaxPassif = 100;
    
    public List<Skill> skills = new List<Skill>();

    public SkillProjectile skillProjectile;


    void BurstBehaviour(float damageDealt)
    {
        //Ajoute une valeur a burstPassif quand le burst fait des dégats.
        burstPassif += damageDealt * Constants.BURST_PASSIF_MULTIPLICATEUR;

        if (burstPassif > burstMaxPassif)
        {
            burstPassif = burstMaxPassif;
        }
    }

    void Update()
    {
        base.Update();

        if (ih.FirstSkill() && skills[0].CanCast(this))
        {
            StartCoroutine(skills[0].Cast());
        }

        if (ih.SecondeSkill())
        {
            Ray ray = new Ray(transform.position, transform.GetChild(0).transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                
            }

        }
    }

    public virtual float GetPassifVal()
    {
        return burstPassif;
    }

    [Command]
    public void CmdSpawnPrefab(float pushTime)
    {
        GameObject p = Instantiate(skillProjectile.projectilePrefab, transform.position + transform.forward, Quaternion.Euler(transform.forward));
        NetworkServer.Spawn(p);

        var sp = p.GetComponent<SkillProjectile>();
        
        sp.speed = (int)pushTime;
    }


}
