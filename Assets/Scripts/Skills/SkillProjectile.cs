using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillProjectile : NetworkBehaviour, IBuffable {

    public float speed, damage, lifeTime;
    [HideInInspector]
    public GameObject source;
    protected List<Buff> buffs = new List<Buff>();

    public void DieAfterLifeTime()
    {
        Destroy(this.gameObject, lifeTime);
    }

    public virtual void Initiate()
    {
    }

    public virtual void Throw ()
    {

    }

    #region Metlable Skills
    //Add base.OnTriggerEnter(collider) pour les projectiles qui utilisent le trigger enter

    private Collider lastCollider = null;
    public void OnTriggerEnter (Collider collider)
    {
        var projectile = collider.GetComponent<SkillProjectile>();
        if (projectile != null && lastCollider == null)
        {
            lastCollider = collider;
            MeltableSkillManager.GetSkillMelting(this.gameObject);
        }
    }
    #endregion

    #region IBuffable Implementation
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
    #endregion
}
