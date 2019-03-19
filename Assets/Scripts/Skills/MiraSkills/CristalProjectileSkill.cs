using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CristalProjectileSkill : Skill {
    public GameObject hand;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        Collider[] colls = Physics.OverlapSphere(this.transform.position, 1000);
        IEnumerable<GameObject> players = colls.Where(coll => coll.tag == Constants.PLAYER_TAG).Select(coll => coll.gameObject).OrderByDescending(player => (this.transform.position - player.transform.position).magnitude);
        Debug.Log(players.Count());
        if(players.Count() > 0)
        {
            source.transform.LookAt(source.transform);
            SkillProjectile projectile = Instantiate(skillProjectile, hand.transform.position, Quaternion.identity);
            projectile.target = players.FirstOrDefault();
            StartCoroutine(ProcessCoolDown());
        }



        yield return true;
    }

    public override bool HasRessource()
    {
        return true;
    }
}
