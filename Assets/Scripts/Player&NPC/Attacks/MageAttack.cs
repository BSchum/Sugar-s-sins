using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class MageAttack : PlayerAttack {

    public float increasingPassifPercentage;
    public float burstPassif = 0;
    private const int burstMaxPassif = 100;
   
    void BurstBehaviour(float damageDealt)
    {
        burstPassif += damageDealt * Constants.BURST_PASSIF_MULTIPLICATEUR;

        if (burstPassif > burstMaxPassif)
        {
            burstPassif = burstMaxPassif;
        }
    }

    void Update()
    {
        base.Update();

        if(isLocalPlayer)
        {
            CmdInitializeSkills();
            if (ih.FirstSkill() && skills[0].CanCast() && !skills[0].isOnCooldown)
            {
                StartCoroutine(skills[0].Cast());
            }
            if (ih.FirstSkillUp() && skills[0].HasRessource())
            {
                FireBallSkill fbs = (FireBallSkill)skills[0];
                fbs.CmdReleaseFireBall();
            }

            if (ih.SecondSkill() && skills[1].CanCast() && !skills[1].isOnCooldown)
            {
                StartCoroutine(skills[1].Cast());
            }

            if (ih.ThirdSkill() && skills[2].CanCast() && !skills[2].isOnCooldown)
            {
                StartCoroutine(skills[2].Cast());
            }

            if (ih.Ultimate() && skills[3].CanCast() && !skills[3].isOnCooldown && burstPassif >= burstMaxPassif)
            {
                burstPassif = 0;
                StartCoroutine(skills[3].Cast());
            }
        }
    }

    [HideInInspector]
    [SyncVar]
    public bool isOnUlt = false;
    public void AddBurstPassif (float damage)
    {
        if(!isOnUlt)
        {
            burstPassif += damage * increasingPassifPercentage;
            if (burstPassif > burstMaxPassif)
            {
                burstPassif = burstMaxPassif;
            }
        }
    }
}
