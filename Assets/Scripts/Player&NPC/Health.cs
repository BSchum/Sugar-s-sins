using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public delegate void HealthChanged(int value);

public class Health : NetworkBehaviour {
    [SerializeField]
    int maxHealth;

    int currentHealth;

    public event HealthChanged OnHealthChanged;
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
    void Update()
    {
        Debug.Log(currentHealth);
    }
    public void TakeDamage(int amount)
    {
        Debug.Log("I took " + amount + "damage");

        if (!isServer)
            return;
        //this.currentHealth -= amount;
        RpcTakeDamage(amount);

    }
    [ClientRpc]
    public void RpcTakeDamage(int amount)
    {
        this.currentHealth -= amount;
        OnHealthChanged(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Subscribe(HealthChanged callBack)
    {
        OnHealthChanged += callBack;
        callBack(currentHealth);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
