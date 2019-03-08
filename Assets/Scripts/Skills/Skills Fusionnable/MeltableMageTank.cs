using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltableMageTank : MeltableSkill {

    [Header("Temps en seconde avant l'application des dégats")]
    public float timeToAbsorb;
    public float damage;
    public float range;

    public override void Merge()
    {
        Invoke("CastDamage", timeToAbsorb);
    }

    private void CastDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(MeltableSkillManager.skillOne.transform.position, range);
        Debug.Log(colliders[0].name);
        foreach(Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();
            Debug.Log(enemy);
            if (enemy != null)
            {
                Health enemyStats = enemy.GetComponent<Health>();
                enemyStats.TakeDamage(damage);
            }
        }

        if(MeltableSkillManager.skillOne.GetComponent<TornadoProjectile>())
        {
            Destroy(MeltableSkillManager.skillOne);
        }
        else
        {
            Destroy(MeltableSkillManager.skillTwo);
        }
    }

}
