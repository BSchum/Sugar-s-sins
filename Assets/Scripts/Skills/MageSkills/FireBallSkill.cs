using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBallSkill : Skill {

    private float castTime = 0;

    //[SyncVar(hook = "CmdSpawnProjectile")]
    private GameObject fireBall = null;
    public AnimationCurve fireBallScale;
    public AnimationCurve fireBallSpeed;
    public Transform mageHand;

    public override bool CanCast()
    {
        return canCast && !isCasting;
    }

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

        SkillProjectile fireBallProjectile = fireBall.GetComponent<SkillProjectile>();
        fireBallProjectile.speed *= bonusSpeed;
        fireBallProjectile.damage *= bonusSpeed;

        fireBallProjectile.Throw();
        fireBall = null;

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
        if(fireBall != null)
        {
            this.scale = s;
            fireBall.transform.localScale = s;
        }
    }

    [Command]
    void CmdSpawnProjectile ()
    {
        //if(isServer)
            RpcSpawnProjectile();
    }

    [ClientRpc]
    void RpcSpawnProjectile ()
    {
        fireBall = Instantiate(skillProjectile.gameObject, mageHand);
    }

    public override bool HasRessource()
    {
        return castTime > 0;
    }
}
