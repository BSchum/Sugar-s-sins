using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class Skill : ScriptableObject {

    public float cost;
    
    [HideInInspector]
    protected bool isCasting;
    public float minCastTime;
    protected float castTime;

    public SkillProjectile skillProjectile;

    protected float castStartTime;

    public bool isCooldown;
    public float cooldown;

    
    public abstract bool CanCast(Stats pStats);
    public abstract bool CanCast(PlayerAttack pInfo);
    public abstract IEnumerator Cast();

}
