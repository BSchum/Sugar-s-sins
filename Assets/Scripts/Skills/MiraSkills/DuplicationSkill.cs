using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicationSkill : Skill
{
    public GameObject[] spots;
    public GameObject fakeMiraPrefab;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        
        source.GetComponent<BossController>().canMove = false;
        source.GetComponent<BossController>().isCasting = true;

        source.GetComponent<BossController>().deadFakeMira = 4;
        //Move real boss to fake point
        int realMiraSpotIndex = Random.Range(0, spots.Length);
        source.transform.position = spots[realMiraSpotIndex].transform.position;
        source.GetComponent<BossController>().resource += 100;

        //Spawn fake mira at other points
        for(int i = 0; i < spots.Length; i++)
        {
            if(i != realMiraSpotIndex)
                Instantiate(fakeMiraPrefab, spots[i].transform.position, Quaternion.identity);
        }


        yield return null;
    }

    public override bool HasRessource()
    {
        return true;
    }
}
