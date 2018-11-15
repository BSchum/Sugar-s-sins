using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void HealthChanged(float value);
[RequireComponent(typeof(Stats))]
public class Health : NetworkBehaviour {
    Stats stats;
    public void Start()
    {
        stats = GetComponent<Stats>();
    }

    public event HealthChanged OnHealthChanged;

    // Use this for initialization

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
        this.stats.AddHealth(amount);
        OnHealthChanged(this.stats.GetHealth());
        if (this.stats.GetHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Subscribe(HealthChanged callBack)
    {
        OnHealthChanged += callBack;
        callBack(stats.GetHealth());
    }

    public float GetMaxHealth()
    {
        return stats.GetMaxHealth();
    }
}
