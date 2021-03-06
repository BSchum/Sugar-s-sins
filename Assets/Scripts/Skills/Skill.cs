﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[System.Serializable]
public abstract class Skill : NetworkBehaviour {
    public Sprite artwork;
    public float cost;
    public float threat;
    public float damage;

    [HideInInspector]
    public bool canCast = true;
    [SerializeField]
    protected bool isCasting = false;

    public bool useProjectors;
    public Material area;
    [HideInInspector]
    public bool hasProjected;
    public SkillProjectile skillProjectile;

    protected float castStartTime;

    [HideInInspector]
    public bool isOnCooldown = false;
    public float cooldown;
    public float internalCD;
    [HideInInspector]
    public GameObject source;

    float startProcessCoolDown;
    public virtual bool CanCast()
    {
        return canCast && !isCasting;
    }
    public abstract IEnumerator Cast(GameObject currentTarget = null);
    public abstract bool HasRessource();

    public virtual IEnumerator ProcessCoolDown()
    {
        isOnCooldown = true;
        for (internalCD = cooldown; internalCD > 0; internalCD -= Time.deltaTime)
            yield return null;
        internalCD = 0;
        isOnCooldown = false;

    }

    InputHandlerBuilder builder;
    protected InputHandler ih;

    public override string ToString()
    {
        
        string t = "\nCurrent Cooldown = "+internalCD;
        
        return "\n\n"+this.GetType() + "\n\nCooldown :" + cooldown +"\n Cost: "+this.cost+"\n OnCD :"+isOnCooldown + t;
    }
}
