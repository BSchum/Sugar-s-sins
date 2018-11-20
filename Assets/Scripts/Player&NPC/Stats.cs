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
    //   /!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\
    // Base Stats are stats without any modification , base stats of the character
    // Current stats are stats with stuff modification and other permanent modification
    // Final stats are stats with buff applied from current stats.
    //   /!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\

    [SerializeField]
    float basePower;
    [SerializeField]
    float currentPower;
    [SerializeField]
    public float finalPower { get; set; }

    [SerializeField]
    float baseDefense;
    [SerializeField]
    float currentDefense;
    [SerializeField]
    public float finalDefense { get; set; }

    [SerializeField]
    float baseSpeed;
    [SerializeField]
    float currentSpeed;
    [SerializeField]
    public float finalSpeed { get; set; }

    [SerializeField]
    float baseMaxHealth;
    [SerializeField]
    float currentMaxHealth;
    [SerializeField]
    public float finalMaxHealth { get; set; }

    [SerializeField]
    float baseHealth;
    [SerializeField]
    float currentHealth;
    public float finalHealth { get; set; }

    public float damageReductionInPercent { get; private set; }

    public void Start()
    {
        ResetFinalStats();
    }

    public void ResetFinalStats()
    {
        finalDefense = currentDefense;
        finalPower = currentPower;
        finalSpeed = currentSpeed;
        finalMaxHealth = currentMaxHealth;
    }
    public void SetDamageReductionInPercent(float percentage)
    {
        if(damageReductionInPercent < percentage)
        {
            damageReductionInPercent = percentage;
        }
    }
    public void AddPower(float inscreaseAmount)
    {
        finalPower += inscreaseAmount;
    }
    public float GetPower()
    {
        return currentHealth;
    }

    public void AddDefense(float inscreaseAmount)
    {
        finalDefense += inscreaseAmount;
    }
    public float GetDefense()
    {
        return currentDefense;
    }
    public void AddSpeed(float inscreaseAmount)
    {
        finalSpeed += inscreaseAmount;
    }
    public float GetSpeed()
    {
        return currentSpeed;
    }
    public void AddHealth(float inscreaseAmount)
    {
        if (finalHealth + inscreaseAmount > finalMaxHealth)
        {
            currentHealth = currentMaxHealth;
        }
        else
        {
            currentHealth += inscreaseAmount;
        }
        OnHealthChanged(this.GetHealth());
    }
    public float GetHealth()
    {
        return currentHealth;
    }
    public void AddMaxHealth(float inscreaseAmount)
    {
        finalMaxHealth += inscreaseAmount;
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

