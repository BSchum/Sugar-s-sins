using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillProjectile : NetworkBehaviour, IBuffable {

    public float speed, damage, lifeTime;
    protected List<Buff> buffs = new List<Buff>();

    public void DieAfterLifeTime()
    {
        Destroy(this.gameObject, lifeTime);
    }

    public virtual void Initiate()
    {
        SpawnOnServer();
    }

    public virtual void SpawnOnServer()
    {
        NetworkServer.Spawn(this.gameObject);
    }

    public virtual void ProjectileBehav ()
    {

    }

    public virtual void Throw ()
    {

    }

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
