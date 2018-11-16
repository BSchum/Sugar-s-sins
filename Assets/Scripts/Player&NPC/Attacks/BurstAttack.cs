using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class BurstAttack : PlayerAttack {


    float burstPassif = 0;
    private const int burstMaxPassif = 100;

    public List<Skill> skills = new List<Skill>();
    public SkillProjectile skillProjectile;

    void BurstBehaviour(float damageDealt)
    {
        //Ajoute une valeur a burstPassif quand le burst fait des dégats.
        burstPassif += damageDealt * Constants.BURST_PASSIF_MULTIPLICATEUR;

        if (burstPassif > burstMaxPassif)
        {
            burstPassif = burstMaxPassif;
        }
    }

    void Update()
    {
        base.Update();

        if (ih.FirstSkill() && !skills.isCasting)
        {
            StartCoroutine(Casting());
        }
        if (ih.SecondeSkill() && )
        {
            StartCoroutine(Casting());
        }
    }

    IEnumerator Casting()
    {
        Debug.Log("yo");

        skills.castStartTime = Time.time;

        skills.isCasting = true;

        yield return new WaitUntil(() => !ih.FirstSkill());

        //Release the key
        var pushTime = Time.time - skills.castStartTime + 1; //+1 => évite d'avoir un résultat = 0
        pushTime = pushTime > 5 ? 5 : pushTime; //Si supérieur à 5, alors égal à 5

        //Debug.Log(pushTime);
        CmdSpawnPrefab(pushTime);

        skills.isCasting = false;
    }

    [Command]
    public void CmdSpawnPrefab(float pushTime)
    {
        GameObject p = Instantiate(skillProjectile.projectilePrefab, transform.position + transform.forward, Quaternion.Euler(transform.forward));
        NetworkServer.Spawn(p);

        var sp = p.GetComponent<SkillProjectile>();
        Debug.Log((int)pushTime);
        sp.projectileSpeed = (int)pushTime;
        sp.Initiate();
    }


}
