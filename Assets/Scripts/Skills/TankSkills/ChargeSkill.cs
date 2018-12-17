using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChargeSkill : Skill {
    public override IEnumerator Cast()
    {
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * 50, ForceMode.Impulse);
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        rb.velocity = Vector3.zero;
    }

    public override bool HasRessource()
    {
        return true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("triggered");
        if (other.transform.tag == Constants.ENEMY_TAG)
        {
            CmdDealChargeDamage(other);
        }
    }
    [Command]
    private void CmdDealChargeDamage(Collision other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(10);
    }
}
