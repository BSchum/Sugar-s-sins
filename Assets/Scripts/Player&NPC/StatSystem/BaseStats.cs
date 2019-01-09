using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStats {
    public float power;
    public float defense;
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public float lifeSteal;

    public BaseStats(float power, float defense, float speed, float health, float maxHealth, float damage, float lifeSteal)
    {
        this.power = power;
        this.defense = defense;
        this.speed = speed;
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.lifeSteal = lifeSteal;

    }

    public BaseStats()
    {
        this.power = 0;
        this.defense = 0;
        this.speed = 0;
        this.health = 0;
        this.maxHealth = 0;
        this.damage = 0;
        this.lifeSteal = 0;
    }

    public static BaseStats operator +(BaseStats a, BaseStats b)
    {
        return new BaseStats(a.power + b.power, a.defense + b.defense, a.speed + b.speed, a.health + b.health, a.maxHealth + b.maxHealth, a.damage + b.damage, a.lifeSteal + b.lifeSteal);
    }

    public override string ToString()
    {
        return "\nPower :" + power + " \nDefense : " + defense + "  \nSpeed : " + speed + " \nHealth :" + health + " \nMaxHealth : " + maxHealth + " \nDamage : " + damage + " LifeSteal : " + lifeSteal;
    }
}
