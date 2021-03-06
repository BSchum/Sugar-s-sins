﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChargeSkill : Skill, IThreatable {
    bool isDashing;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);

        float dashStartAt = Time.time;
        float dashTime = 0.2f;
        Motor motor = new Motor(this.gameObject);
        while (Time.time < dashStartAt + dashTime)
        {
            isDashing = true;
            motor.Move(Vector3.forward, Quaternion.identity.eulerAngles, 100);
            yield return new WaitForEndOfFrame();
        }
        isDashing = false;
        yield return null;
        
    }

    public override bool HasRessource()
    {
        if (gameObject.GetComponent<TankAttacks>().GetGelatinStacks() >= this.cost)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == Constants.ENEMY_TAG || other.transform.tag == Constants.BOSS_TAG && isDashing)
        {
            Debug.Log("Colliding with "+ other.gameObject.name);
            CmdDealChargeDamage(other.gameObject);
        }
    }

    [Command]
    private void CmdDealChargeDamage(GameObject other)
    {
        other.GetComponent<Health>().TakeDamage(100);
        RpcGenerateThreat(other);
    }
    [ClientRpc]
    private void RpcGenerateThreat(GameObject other)
    {
        if(other != null)
            GenerateThreat(other.GetComponent<EnemyController>());
    }

    public void GenerateThreat(EnemyController enemy)
    {
        enemy.AddThreatFor(this.gameObject, threat);
    }
}
