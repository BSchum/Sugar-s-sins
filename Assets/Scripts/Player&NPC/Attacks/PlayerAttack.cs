using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[RequireComponent(typeof(Stats))]
public class PlayerAttack : PlayerScript, IBuffable {
    protected Weapon weapon;
    protected Stats stats;

    protected Skill[] skills;
    [SerializeField]
    protected List<Buff> buffs = new List<Buff>();


    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
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
    public void Start () {
        this.gameObject.name = "Tank " + Random.Range(0, 10000);
        Initialize();
        weapon = GetComponentInChildren<Weapon>();
        stats = GetComponent<Stats>();
        skills = GetComponents<Skill>();
    }

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

    public void Update () {
        CmdApplyBuff();
        stats.ResetBonusStats();

        int i = 0;
        foreach (Buff buff in buffs)
        {
            //Debug.Log(gameObject.name+" -- Buff n" + i + " -- Nom : " + buff.GetType());
            i++;
        }

        if (ih.SimpleAttackInput())
        {
            Fire();
        }

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
    }
    public virtual float GetPassifVal ()
    {
        return 0f;
    }

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
        Debug.Log("Jattack "+target.name+" sur le server avec "+weapon.name);
        Health h = target.GetComponent<Health>();
        h.TakeDamage(weapon.damage + this.stats.GetDamage());
    }
}
