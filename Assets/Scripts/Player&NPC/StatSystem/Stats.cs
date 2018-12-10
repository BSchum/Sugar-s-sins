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
    // Base Stats are stats without any modification , base stats of the character.
    // Current stats are stats with stuff modification and other permanent modification.
    // Final stats are stats with buff applied from current stats, for most of our computation, we will use this.
    //
    // Bonus Stats are stats added to currentStats to make finals stats.
    //   /!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\
    [SerializeField]
    BaseStats baseStats;
    [SerializeField]
    BaseStats currentStats;
    BaseStats finalStats;
    BaseStats bonusStats;

    public float damageReductionInPercent;

    public void Start()
    {
        finalStats = new BaseStats();
        bonusStats = new BaseStats();
        ResetBonusStats();
    }

    public void Update()
    {
        if (name == "Totem(Clone)")
        {
            Debug.Log("Je compute pour " + gameObject.name);
            Debug.Log(currentStats);
            Debug.Log(bonusStats);
        }
        ComputeFinalStats();
    }
    
    public void TakeDamage(float amount)
    {
        if (currentStats.health > 0)
        {
            currentStats.health += amount;
        }
        else if (bonusStats.health > 0)
        {
            bonusStats.health += amount;
        }
        OnHealthChanged(finalStats.health);
    }

    #region GetFinalsStats
    public float GetHealth()
    {
        return finalStats.health;
    }

    public float GetMaxHealth()
    {
        return finalStats.maxHealth;
    }

    public float GetSpeed()
    {
        return finalStats.speed;
    }

    public float GetPower()
    {
        return finalStats.power;
    }

    public float GetDefense()
    {
        return finalStats.defense;
    }

    public float GetDamage()
    {
        return finalStats.damage;
    }

    #endregion

    #region GetCurrentStats
    public float GetCurrentHealth()
    {
        return currentStats.health;
    }

    public float GetCurrentMaxHealth()
    {
        return currentStats.maxHealth;
    }

    public float GetCurrentSpeed()
    {
        return currentStats.speed;
    }

    public float GetCurrentPower()
    {
        return currentStats.power;
    }

    public float GetCurrentDefense()
    {
        return currentStats.defense;
    }

    public float GetCurrentDamage()
    {
        return currentStats.damage;
    }
    #endregion

    #region BuffStats
    public void BuffMaxHealth(float buffAmount)
    {
        bonusStats.maxHealth += buffAmount;
    }

    public void BuffSpeed(float buffAmount)
    {
        bonusStats.speed += buffAmount;
    }

    public void BuffPower(float buffAmount)
    {
        bonusStats.power += buffAmount;
    }

    public void BuffDefense(float buffAmount)
    {
        bonusStats.defense += buffAmount;
    }

    public void BuffDamage(float buffAmount)
    {
        bonusStats.damage += buffAmount;
    }
    #endregion

    #region AddCurrentStats
    public void AddCurrentHealth(float amount)
    {
        currentStats.health += amount;
    }

    public void AddCurrentMaxHealth(float amount)
    {
        currentStats.maxHealth += amount;
    }

    public void AddSpeed(float amount)
    {
        currentStats.speed += amount;
    }

    public void AddPower(float amount)
    {
        currentStats.power += amount;
    }

    public void AddDefense(float amount)
    {
        currentStats.defense += amount;
    }

    public void AddDamage(float amount)
    {
        currentStats.damage += amount;
    }
    public void SetDamageReductionInPercent(float percentage)
    {
        damageReductionInPercent = percentage;
    }
    #endregion

    #region Computation
    public void ResetBonusStats()
    {
        bonusStats.health = 0;
        bonusStats.maxHealth = 0;
        bonusStats.power = 0;
        bonusStats.speed = 0;
        bonusStats.defense = 0;
        bonusStats.damage = 0;
        damageReductionInPercent = 0;
    }

    private void ComputeFinalStats()
    {
        finalStats = currentStats + bonusStats;
    }
    #endregion

    #region Events
    public void Subscribe(HealthChanged callBack)
    {
        OnHealthChanged += callBack;
        callBack(GetHealth());
    }
    #endregion
}

