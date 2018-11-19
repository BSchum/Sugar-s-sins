using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class MageAttack : PlayerAttack {

    float burstPassif = 0;
    private const int burstMaxPassif = 100;

    [SerializeField]
    public List<Skill> skills = new List<Skill>();
    //public List<string> skills_Name = new List<string>();

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

        if (ih.FirstSkill() && skills[0].CanCast(this))
        {
            StartCoroutine(skills[0].Cast());
        }

        if (ih.SecondeSkill())
        {
            Ray ray = new Ray(transform.position, transform.GetChild(0).transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                //var newTornade = Instantiate(skills[1].skillPrefab, hit.point, Quaternion.identity);
                //StartCoroutine(newTornade.GetComponent<SkillProjectile>().DieAfterSecond());
            }

        }
    }

    public virtual float GetPassifVal()
    {
        return burstPassif;
    }

    [Command]
    public void CmdSpawnPrefab(float pushTime)
    {
        GameObject p = Instantiate(skillProjectile.projectilePrefab, transform.position + transform.forward, Quaternion.Euler(transform.forward));
        NetworkServer.Spawn(p);

        var sp = p.GetComponent<SkillProjectile>();
        //Debug.Log((int)pushTime);
        sp.speed = (int)pushTime;
        //sp.Initiate();
    }


}
