using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TotemProjectile : SkillProjectile {

    public GameObject source;
    [SerializeField]
    int gelatinStacksAmount;
    [SerializeField]
    float gelatinStacksRate;

    [SerializeField]
    float attackRate;

    [SerializeField]
    float range;

    bool isAttacking;
    // Use this for initialization
    void Start () {
        DieAfterLifeTime();
        StartCoroutine(GiveGelatinStack());
        StartCoroutine(Attack());
    }

    private void Update()
    {
    }

    IEnumerator GiveGelatinStack()
    {
        while (true)
        {
            source.GetComponent<TankAttacks>().AddGelatinStack(gelatinStacksAmount);
            yield return new WaitForSeconds(gelatinStacksRate);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, range);
            IEnumerable<Collider> sortedColls = colliders.Where(c => c != null)
                                                         .Where(c => c.tag == Constants.ENEMY_TAG)
                                                         .OrderBy(c => Vector3.Distance(this.transform.position, c.transform.position));
            if (sortedColls.Count() > 0)
            {
                sortedColls.FirstOrDefault().GetComponent<Health>().TakeDamage(this.GetComponent<Stats>().GetDamage());
            }
            yield return new WaitForSeconds(attackRate);
        }

    }
}
