using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltableMageTank : MeltableSkill {

    [Header("skillOne => totem, skillTwo => fireWall")]
    public float timeToAbsorb;
    public float damage;
    public float range;

    private GameObject firstSkill;
    private GameObject secondSkill;

    public override void Merge(GameObject firstSkill, GameObject secondSkill)
    {
        this.firstSkill = firstSkill;
        this.secondSkill = secondSkill;
        Invoke("CastDamage", timeToAbsorb);
    }

    private void CastDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(skillOne.transform.position, range);
        foreach(Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            if(enemy != null)
            {
                Health enemyStats = enemy.GetComponent<Health>();
                enemyStats.TakeDamage(damage);
            }
        }

        Destroy(firstSkill.GetComponent<FireWallProjectile>() != null ? firstSkill.gameObject : secondSkill.gameObject);
    }

}
