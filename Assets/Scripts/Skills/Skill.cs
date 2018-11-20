using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public abstract class Skill : MonoBehaviour {

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
    public bool isCooldown = false;
    public float cooldown;


    public abstract bool CanCast();
    public abstract IEnumerator Cast();


    InputHandlerBuilder builder;
    protected InputHandler ih;

    public virtual void Start()
    {
        ih = new InputHandlerBuilder().ChooseInputHandler().Build();
        
        isCasting = false;
    }
}
