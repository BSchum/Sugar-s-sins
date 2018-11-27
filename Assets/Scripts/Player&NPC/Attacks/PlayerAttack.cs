using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[RequireComponent(typeof(Stats))]
public class PlayerAttack : PlayerScript {
    protected Weapon weapon;
    protected Stats stats;

    protected Skill[] skills;
    protected List<Buff> buffs = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
    }

    public void ApplyBuffs()
    {
        foreach (Buff buff in buffs)
        {
            buff.ApplyBuff();
        }
    }
    public void Start () {
        Initialize();
        weapon = GetComponentInChildren<Weapon>();
        stats = GetComponent<Stats>();
        skills = GetComponents<Skill>();
    }

    public void Update () {
        stats.ResetBonusStats();
        ApplyBuffs();
        if (ih.SimpleAttackInput())
        {
            Fire();
        }

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
