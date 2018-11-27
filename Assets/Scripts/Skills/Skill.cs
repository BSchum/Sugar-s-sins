using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class Skill : NetworkBehaviour {

    public float cost;

    [HideInInspector]
    public bool canCast = true;
    [SerializeField]
    protected bool isCasting = false;
    public float minCastTime = 0;
    public float maxCastTime;

    public SkillProjectile skillProjectile;

    protected float castStartTime;

    [HideInInspector]
    public bool isOnCooldown = false;
    public float cooldown;
    [HideInInspector]
    public GameObject source;

    public virtual bool CanCast()
    {
        return canCast && !isCasting;
    }
    public abstract IEnumerator Cast();
    public abstract bool HasRessource();
    public IEnumerator ProcessCoolDown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;

    }


    InputHandlerBuilder builder;
    protected InputHandler ih;
}
