using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CristalHordeSkill : Skill
{
    public GameObject cristalPrefab;
    public Transform spawnPrefab;
    public Transform safePos;
    public int amount;
    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        StartCoroutine(ProcessCoolDown());
        for(int i = 0; i < amount; i++)
        {
            Vector3 cristalPos = new Vector3(transform.position.x + Random.Range(0, 3), spawnPrefab.position.y, transform.position.z+Random.Range(0, 3));
            GameObject cristal = Instantiate(cristalPrefab, cristalPos, Quaternion.identity);
            NetworkServer.Spawn(cristal);
            cristal.GetComponent<EnemyController>().AddThreatFor(currentTarget, 100);
            yield return new WaitForSeconds(0.2f);
        }

        //Je me tp en sécurité pendant 5sec
        Vector3 pos = this.transform.position;
        this.transform.position = safePos.position;
        source.GetComponent<BossController>().canMove = false;
        source.GetComponent<BossController>().isCasting = true;
        yield return new WaitForSeconds(2f);
        this.transform.position = pos;
        source.GetComponent<BossController>().canMove = true;
        source.GetComponent<BossController>().isCasting = false;
    }

    public override bool HasRessource()
    {
        return GetComponent<BossController>().CurrentRessourceValue > cost;
    }
}
