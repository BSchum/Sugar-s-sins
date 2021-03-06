﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FireBallSkill : Skill {
    
    private float castTime = 0;
    public float minCastTime = 0;
    public float maxCastTime;
    private GameObject fireBall = null;
    private GameObject[] fireBalls = new GameObject[3];
    public AnimationCurve fireBallScale;
    public AnimationCurve fireBallSpeed;
    public Transform mageHand;
    public Transform[] spawnFireBallUlt;

    public override bool CanCast()
    {
        return canCast && !isCasting;
    }

    bool castedAsUlt = false;
    bool fireBallSpawned = false;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        if (!fireBallSpawned)
        {
            fireBallSpawned = true;
            CmdSpawnProjectile();
            CastBar.singleton.ChangeState(true);
        }
        else
        {
            CmdIncreaseFireBall();
            CastBar.singleton.UpdateCastBar(castTime / maxCastTime);
        }

        CmdIncreaseCastTime();

        yield return null;
    }

    [Command]
    private void CmdIncreaseCastTime ()
    {
        castTime += 1 * Time.deltaTime;

        if (castTime > maxCastTime)
        {
            castTime = maxCastTime;
        }
    }

    [Command]
    public void CmdReleaseFireBall()
    {
        RpcReleaseFireBall(castTime);
    }
    
    float bonusSpeed;
    [ClientRpc]
    public void RpcReleaseFireBall(float cast)
    {
        CastBar.singleton.ChangeState(false);

        float maxTime = fireBallScale.keys[fireBallScale.length - 1].time;
        bonusSpeed = fireBallSpeed.Evaluate((cast / maxCastTime) * maxTime);

        if(castedAsUlt)
        {
            if(fireBall != null)
            {
                SkillProjectile fireBallProjectile = fireBall.GetComponent<SkillProjectile>();
                fireBallProjectile.speed *= bonusSpeed;
                fireBallProjectile.damage *= bonusSpeed;
                fireBallProjectile.source = this.gameObject;

                fireBallProjectile.Throw();
                fireBall = null;
            }
        }
        else
        {
            StartCoroutine(ReleaseMultipleFireBall());
        }

        fireBallSpawned = false;
        castTime = 0;
        StartCoroutine(ProcessCoolDown());
    }

    public IEnumerator ReleaseMultipleFireBall()
    {
        foreach (GameObject fire in fireBalls)
        {
            SkillProjectile fireBallProjectile = fire.GetComponent<SkillProjectile>();
            fireBallProjectile.speed *= bonusSpeed;
            fireBallProjectile.damage *= bonusSpeed;
            fireBallProjectile.source = this.gameObject;

            fireBallProjectile.Throw();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private Vector3 scale;
    [Command]
    void CmdIncreaseFireBall ()
    {
        float maxTime = fireBallScale.keys[fireBallScale.length - 1].time;
        scale = Vector3.one * fireBallScale.Evaluate((castTime / maxCastTime) * maxTime);
        RpcIncreaseFireBall(scale);
    }

    [ClientRpc]
    private void RpcIncreaseFireBall (Vector3 newScale)
    {
        if (castedAsUlt)
        {
            if (fireBall != null)
            {
                fireBall.transform.localScale = newScale;
            }
        }
        else
        {
            if (fireBalls[0] != null && fireBalls[1] != null  && fireBalls[2] != null)
            {
                fireBalls[0].transform.localScale = newScale;
                fireBalls[1].transform.localScale = newScale;
                fireBalls[2].transform.localScale = newScale;
            }
        }
    }

    [Command]
    void CmdSpawnProjectile ()
    {
        RpcSpawnProjectile();
    }

    [ClientRpc]
    void RpcSpawnProjectile ()
    {
        castedAsUlt = !GetComponent<MageAttack>().isOnUlt;
        if (castedAsUlt)
        {
            fireBall = Instantiate(skillProjectile.gameObject, mageHand);
        }
        else
        {
            fireBalls[0] = Instantiate(skillProjectile.gameObject, mageHand);
            fireBalls[1] = Instantiate(skillProjectile.gameObject, mageHand);
            fireBalls[2] = Instantiate(skillProjectile.gameObject, mageHand);
        }
    }

    public override bool HasRessource()
    {
        return true;
    }
}
