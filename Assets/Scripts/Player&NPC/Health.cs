using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class Health : NetworkBehaviour {
    Stats stats;
    public void Start()
    {
        stats = GetComponent<Stats>();
    }

    public void TakeDamage(float amount)
    {
        if (!isServer)
            return;
        RpcTakeDamage(amount);
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        Debug.Log("Je prend -" + amount + "de degats");
        this.stats.TakeDamage(-amount * (1 - (this.stats.damageReductionInPercent / 100)));
        if (this.stats.GetHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public float GetMaxHealth()
    {
        return stats.GetMaxHealth();
    }
}
