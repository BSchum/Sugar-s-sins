using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChargeSkill : Skill, IThreatable {
    public float damage = 100;
    public float speed = 100;

    Motor motor = null;

    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);

        float dashStartAt = Time.time;
        float dashTime = 0.2f;
        motor = new Motor(this.gameObject);
        while (Time.time < dashStartAt + dashTime)
        {
            motor.Move(Vector3.forward, Quaternion.identity.eulerAngles, speed);
            yield return new WaitForEndOfFrame();
        }
        motor = null;
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
        if(motor != null)
        {
            if (other.transform.tag == Constants.ENEMY_TAG || other.transform.tag == Constants.BOSS_TAG)
            {
                CmdDealChargeDamage(other.gameObject);
            }
        }
    }

    [Command]
    private void CmdDealChargeDamage(GameObject other)
    {
        other.GetComponent<Health>().TakeDamage(damage);
        RpcGenerateThreat(other);
    }
    [ClientRpc]
    private void RpcGenerateThreat(GameObject other)
    {
        GenerateThreat(other.GetComponent<EnemyController>());
    }

    public void GenerateThreat(EnemyController enemy)
    {
        enemy.AddThreatFor(this.gameObject, threat);
    }
}
