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
    public override IEnumerator Cast()
    {
        if(!fireBallSpawned)
        {
            fireBallSpawned = true;
            CmdSpawnProjectile();
        }
        else
        {
            float maxTime = fireBallScale.keys[fireBallScale.length - 1].time;
            Vector3 newScale = Vector3.one * fireBallScale.Evaluate((castTime / maxCastTime) * maxTime);
            CmdIncreaseFireBall(newScale);
        }

        castTime += 1 * Time.deltaTime;
        
        if(castTime > maxCastTime)
        {
            castTime = maxCastTime;
        }

        yield return null;
    }

    [Command]
    public void CmdReleaseFireBall()
    {
        RpcReleaseFireBall();
    }

    [ClientRpc]
    public void RpcReleaseFireBall()
    {
        float maxTime = fireBallScale.keys[fireBallScale.length - 1].time;
        float bonusSpeed = fireBallSpeed.Evaluate((castTime / maxCastTime) * maxTime);

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
    }

    [SyncVar(hook = "IncreaseFireBall")] private Vector3 scale;
    [Command]
    void CmdIncreaseFireBall (Vector3 newScale)
    {
        scale = newScale;
    }

    private void IncreaseFireBall (Vector3 s)
    {
        if(castedAsUlt)
        {
            if (fireBall != null)
            {
                this.scale = s;
                fireBall.transform.localScale = s;
            }
        }
        else
        {
            if (fireBalls[0] != null && fireBalls[1] != null  && fireBalls[2] != null)
            {
                this.scale = s;
                fireBalls[0].transform.localScale = s;
                fireBalls[1].transform.localScale = s;
                fireBalls[2].transform.localScale = s;
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
        return castTime > 0;
    }
}
