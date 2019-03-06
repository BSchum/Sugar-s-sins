using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
public delegate void RessourceChanged(float value, float maxValue);
[RequireComponent(typeof(Stats))]
public class PlayerAttack : PlayerScript, IBuffable {
    protected Weapon weapon;
    protected Stats stats;

    protected Skill[] skills;
    protected List<Buff> buffs = new List<Buff>();
    public RessourceChanged OnRessourceChanged;

    public Projectors projector;

    #region Unity's methods
    public void Start()
    {
        Initialize();
        weapon = GetComponentInChildren<Weapon>();
        stats = GetComponent<Stats>();
        skills = GetComponents<Skill>();
        if (isLocalPlayer)
        {
            UIManager.instance.AddSkills(skills.Take(4).ToList());
            UIManager.instance.Subscribe(this.stats);
        }
    }
    public void Update()
    {
        CmdApplyBuff();
        if (ih.SimpleAttackInput())
        {
            Fire();
        }

    }
    #endregion
    #region Buff system
    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
        UIManager.instance.AddBuff(buff.GetBuffAsUIObject());
    }
    [Command]
    public void CmdApplyBuff()
    {
        RpcApplyBuff();
    }
    [ClientRpc]
    void RpcApplyBuff()
    {
        ApplyBuffs();
        stats = this.GetComponent<Stats>();
        stats.ComputeFinalStats();
        stats.ResetBonusStats();
    }
    public void ApplyBuffs()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].isEnded())
            {
                buffs.Remove(buffs[i]);
            }
            else
            {
                buffs[i].ApplyBuff();
            }
        }
    }
    #endregion
    #region Initialization

    [Command]
    protected void CmdInitializeSkills()
    {
        RpcInitializeSkills();
    }
    [ClientRpc]
    void RpcInitializeSkills()
    {
        this.skills = GetComponents<Skill>();
    }

    #endregion
    #region Basic Attack

    public void Fire()
    {
        Ray r = new Ray(this.transform.position, this.transform.forward);
        RaycastHit rHit;
        if(Physics.Raycast(r, out rHit))
        {
            GameObject target = rHit.transform.gameObject;

            if(target.tag == Constants.ENEMY_TAG)
            {
                CmdAttack(target);
            }
        }
    }

    [Command]
    public void CmdAttack(GameObject target)
    {
        weapon = GetComponentInChildren<Weapon>();
        Health h = target.GetComponent<Health>();
        if(h != null)
            h.TakeDamage(weapon.damage + this.stats.GetDamage());
    }
    #endregion
}
