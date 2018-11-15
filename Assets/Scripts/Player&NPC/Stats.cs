using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;



public class Stats : NetworkBehaviour
{
    [SerializeField]
    float power;
    [SerializeField]
    float defense;
    [SerializeField]
    float speed;
    [SerializeField]
    float maxHealth;
    float health;

    public void Start()
    {
        health = maxHealth;
    }

    public void AddPower(int amount)
    {
        power += amount;
    }   
    public float GetPower()
    {
        return power;
    }
    public void AddDefense(int amount)
    {
        defense += amount;
    }
    public float GetDefense()
    {
        return defense;
    }
    public void AddSpeed(int amount)
    {
        speed += amount;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void AddHealth(int amount)
    {
        if (health + amount > maxHealth){
            health = maxHealth;
        }
        else{
            health += amount;
        }
    }
    public float GetHealth()
    {
        return health;
    }
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

}

