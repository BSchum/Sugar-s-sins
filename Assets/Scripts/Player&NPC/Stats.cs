using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public delegate void HealthChanged(float value);
public class Stats : NetworkBehaviour
{
    public event HealthChanged OnHealthChanged;

    [SerializeField]
    float basePower;
    [SerializeField]
    float currentPower;

    [SerializeField]
    float baseDefense;
    [SerializeField]
    float currentDefense;

    [SerializeField]
    float baseSpeed;
    [SerializeField]
    float currentSpeed;

    [SerializeField]
    float baseMaxHealth;
    [SerializeField]
    float currentMaxHealth;

    [SerializeField]
    float baseHealth;
    [SerializeField]
    float currentHealth;


    public void Start()
    {
    }

    void Update()
    {
    }

    public void AddPower(float amount)
    {
        currentPower += amount;
    }
    public float GetPower()
    {
        return currentHealth;
    }
    public void AddDefense(float amount)
    {
        currentDefense += amount;
    }
    public float GetDefense()
    {
        return currentDefense;
    }
    public void AddSpeed(float amount)
    {
        currentSpeed += amount;
    }
    public float GetSpeed()
    {
        return currentSpeed;
    }
    public void AddHealth(float amount)
    {
        Debug.Log("Je lance le AddHEalth");
        if (currentHealth + amount > currentMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
        else
        {
            currentHealth += amount;
        }
        OnHealthChanged(this.GetHealth());
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    public void AddMaxHealth(float amount)
    {
        currentMaxHealth += amount;
    }
    public float GetMaxHealth()
    {
        return currentMaxHealth;
    }
    public void Subscribe(HealthChanged callBack)
    {
        OnHealthChanged += callBack;
        callBack(GetHealth());
    }

}

