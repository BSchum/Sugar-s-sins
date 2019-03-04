using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyRaySkill : Skill {
    public GameObject mirror;
    public LineRenderer bossRay;
    public LineRenderer mirrorRay;
    public GameObject start;
    public GameObject rayCastStart;
    public override IEnumerator Cast(GameObject target = null)
    {
        Debug.Log("Nb de sort lancé");
        isCasting = true;
        bossRay.gameObject.SetActive(true);
        mirrorRay.gameObject.SetActive(true);
        source.GetComponent<BossController>().isCasting = true;
        source.transform.LookAt(mirror.transform);
        bossRay.SetPosition(0, start.transform.position);
        bossRay.SetPosition(1, mirror.transform.position);
        Vector3 direction = mirror.transform.position - rayCastStart.transform.position;
        Ray ray = new Ray(rayCastStart.transform.position, direction);
        RaycastHit[] infos = Physics.RaycastAll(ray, 1000000);

        IEnumerable<RaycastHit> playersHitted = infos.Where( c => c.collider.tag == Constants.ENEMY_TAG);
        Debug.Log(playersHitted.Count());
        foreach(RaycastHit rHit in playersHitted)
        {
            rHit.collider.GetComponent<Health>().TakeDamage(50);
        }
        yield return new WaitForSeconds(5);
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        yield return new WaitForSeconds(1);
        mirrorRay.SetPosition(0, mirror.transform.position);
        mirrorRay.SetPosition(1, pos);

        direction = rayCastStart.transform.position - pos;

        ray = new Ray(mirror.transform.position, direction);
        infos = Physics.RaycastAll(ray, 1000000);

        playersHitted = infos.Where(c => c.collider.tag == Constants.ENEMY_TAG);
        Debug.Log(playersHitted.Count());

        foreach (RaycastHit rHit in playersHitted)
        {
            rHit.collider.GetComponent<Health>().TakeDamage(50);
        }
        yield return new WaitForSeconds(5);
        source.GetComponent<BossController>().isCasting = false;
        bossRay.gameObject.SetActive(false);
        mirrorRay.gameObject.SetActive(false);
        StartCoroutine(this.ProcessCoolDown());
        isCasting = false;
    }

    public override bool HasRessource()
    {
        return true;
    }
}
