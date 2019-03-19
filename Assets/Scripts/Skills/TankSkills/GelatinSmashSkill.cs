using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GelatinSmashSkill : Skill , IThreatable{
    public Collider[] colliders;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        gameObject.GetComponent<TankAttacks>().AddGelatinStack((int)-this.cost);

        for (int i =0; i< colliders.Length; i++)
        {
            Debug.Log("Activation de ce coup : " + colliders[i].name);
            colliders[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            colliders[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.8f);
        }

        if(GetComponent<TankAttacks>().lastActiveTotem != null)
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
        RpcOnSmashHit(target);
        if (GetComponentInParent<TankAttacks>().lastActiveTotem != null)
        {
            GetComponentInParent<TankAttacks>().lastActiveTotem.GetComponent<TotemProjectile>().ChargeLightning(target.gameObject);
        }
    }

    [ClientRpc]
    public void RpcOnSmashHit(GameObject target)
    {
        if(target != null)
            GenerateThreat(target.GetComponent<EnemyController>());
    }

    public void GenerateThreat(EnemyController enemy)
    {
        enemy.AddThreatFor(this.gameObject, threat); 
    }
}
