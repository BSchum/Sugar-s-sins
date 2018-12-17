using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChargeSkill : Skill {
    public override IEnumerator Cast()
    {
        float dashStartAt = Time.time;
        float dashTime = 0.2f;
        Motor motor = new Motor(this.gameObject);
        while (Time.time < dashStartAt + dashTime)
        {
            motor.Move(Vector3.forward, Quaternion.identity.eulerAngles, 100);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        
    }

    public override bool HasRessource()
    {
        if (gameObject.GetComponent<TankAttacks>().GetGelatinStacks() >= this.cost)
        {
            gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);
            return true;
        }
        return false;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == Constants.ENEMY_TAG)
        {
            CmdDealChargeDamage(other.gameObject);
        }
    }
    [Command]
    private void CmdDealChargeDamage(GameObject other)
    {
        other.GetComponent<Health>().TakeDamage(10);
    }
}
