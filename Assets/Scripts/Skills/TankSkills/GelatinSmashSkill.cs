using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GelatinSmashSkill : Skill {
    public Collider[] colliders;
    public override IEnumerator Cast()
    {
        for(int i =0; i< colliders.Length; i++)
        {
            Debug.Log("Activation de ce coup : " + colliders[i].name);
            colliders[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            colliders[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.8f);
        }

        CmdTotemLightAttack();
    }
    [Command]
    void CmdTotemLightAttack() {
        StartCoroutine(GetComponent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>().LightningAttack());
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

    public void OnSmashHit(float damage, Collider target)
    {
        CmdOnSmashHit(damage, target.gameObject);

    }
    [Command]
    public void CmdOnSmashHit(float damage, GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damage);
        GetComponentInParent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>().ChargeLightning(target.gameObject);
    }
}
