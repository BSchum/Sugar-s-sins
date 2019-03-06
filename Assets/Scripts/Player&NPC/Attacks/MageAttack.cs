using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class MageAttack : PlayerAttack, IRessourcesManipulator
{
    public float increasingPassifPercentage;
    public float burstPassif = 0;
    public int burstMaxPassif = 100;

    void Start()
    {
        base.Start();
        if (isLocalPlayer)
        {
            UIManager.instance.UpdatePlayerResourceBar(CurrentRessourceValue, MaxRessourceValue);
        }
    }
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
                if (skills[1].useProjectors)
                {
                    if(!projector.gameObject.activeSelf || projector.skill != skills[1])
                    {
                        projector.gameObject.SetActive(true);
                        projector.SetProjector(skills[1]);
                    }
                    else
                    {
                        projector.gameObject.SetActive(false);
                    }
                }
                else
                {
                    StartCoroutine(skills[1].Cast());
                }
            }

            if (ih.ThirdSkill() && skills[2].CanCast() && !skills[2].isOnCooldown)
            {
                if (skills[2].useProjectors)
                {
                    if (!projector.gameObject.activeSelf || projector.skill != skills[2])
                    {
                        projector.gameObject.SetActive(true);
                        projector.SetProjector(skills[2]);
                    }
                    else
                    {
                        projector.gameObject.SetActive(false);
                    }
                }
                else
                {
                    StartCoroutine(skills[2].Cast());
                }
            }

            if (ih.Ultimate() && skills[3].CanCast() && !skills[3].isOnCooldown && burstPassif >= burstMaxPassif)
            {
                StartCoroutine(skills[3].Cast());
            }
        }
    }

    [HideInInspector]
    [SyncVar]
    public bool isOnUlt = false;

    public float CurrentRessourceValue
    {
        get
        {
            return burstPassif;
        }
    }

    public float MaxRessourceValue
    {
        get
        {
            return burstMaxPassif;
        }
    }
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
