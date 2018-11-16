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

    public void TakeDamage(int amount)
    {
        Debug.Log("I took " + amount + "damage");
        if (!isServer)
            return;
        RpcTakeDamage(amount);
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        Debug.Log("Je prend des degats");
        this.stats.AddHealth(-amount);
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
