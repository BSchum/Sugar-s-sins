using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalConeSkill : Skill
{
    public GameObject cone;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        source.GetComponent<BossController>().isCasting = true;
        source.GetComponent<BossController>().canMove = false;

        //Créer un projector pendant 1seconde en code devant elle
        yield return new WaitForSeconds(1);
        //A la fin de cette seconde, j'inflige des dégats a chaque personnage et je les root
        cone.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        cone.SetActive(false);
        source.GetComponent<BossController>().isCasting = false;
        source.GetComponent<BossController>().canMove = true;
    }

    public override bool HasRessource()
    {
        return true;
    }
}
