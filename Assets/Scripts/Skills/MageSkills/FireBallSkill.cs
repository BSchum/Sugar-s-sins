using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBallSkill : Skill {
    
    private float castTime = 0;
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
    public override IEnumerator Cast(GameObject target = null)
    {
        if (!fireBallSpawned)
        {
            fireBallSpawned = true;
            CmdSpawnProjectile();
        }
        else
        {
            CmdIncreaseFireBall();
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
        float maxTime = fireBallScale.keys[fireBallScale.length - 1].time;
        bonusSpeed = fireBallSpeed.Evaluate((cast / maxCastTime) * maxTime);

        if(castedAsUlt)
        {
            SkillProjectile fireBallProjectile = fireBall.GetComponent<SkillProjectile>();
            fireBallProjectile.speed *= bonusSpeed;
            fireBallProjectile.damage *= bonusSpeed;
            fireBallProjectile.source = this.gameObject;

            fireBallProjectile.Throw();
            fireBall = null;
        }
        else
        {
            for(int i=0; i<fireBalls.Length; i++)
            {
                SkillProjectile fireBallProjectile = fireBalls[i].GetComponent<SkillProjectile>();
                fireBallProjectile.speed *= bonusSpeed;
                fireBallProjectile.damage *= bonusSpeed;
                fireBallProjectile.source = this.gameObject;

                fireBallProjectile.Throw();
                fireBalls[i] = null;
            }
        }

        fireBallSpawned = false;
        castTime = 0;
        StartCoroutine(ProcessCoolDown());
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
            fireBalls[0] = Instantiate(skillProjectile.gameObject, spawnFireBallUlt[0]);
            fireBalls[1] = Instantiate(skillProjectile.gameObject, spawnFireBallUlt[1]);
            fireBalls[2] = Instantiate(skillProjectile.gameObject, spawnFireBallUlt[2]);
        }
    }

    public override bool HasRessource()
    {
        return true;
    }
}
