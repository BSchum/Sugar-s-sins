using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EnergyRaySkill : Skill {
    public GameObject mirror;
    public LineRenderer bossRay;
    public LineRenderer mirrorRay;
    public GameObject start;
    public GameObject rayCastStart;
    public override IEnumerator Cast(GameObject target = null)
    {
        source.GetComponent<BossController>().isCasting = true;
        source.transform.LookAt(mirror.transform);
        isCasting = true;

        bossRay.gameObject.SetActive(true);
        RpcDrawBossEnergyRay();

        Vector3 direction = mirror.transform.position - rayCastStart.transform.position;
        Ray ray = new Ray(rayCastStart.transform.position, direction);
        RaycastHit[] infos = Physics.RaycastAll(ray, 1000000);

        IEnumerable<RaycastHit> playersHitted = infos.Where( c => c.collider.tag == Constants.PLAYER_TAG);
        foreach(RaycastHit rHit in playersHitted)
        {
            rHit.collider.GetComponent<Health>().TakeDamage(50);
        }
        yield return new WaitForSeconds(1);
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        //Wait until the player can avoid
        yield return new WaitForSeconds(1);
        mirrorRay.gameObject.SetActive(true);
        RpcDrawMirrorEnergyRay(pos);

        direction = pos - mirror.transform.position;

        ray = new Ray(mirror.transform.position, direction);
        infos = Physics.RaycastAll(ray, 1000000);

        playersHitted = infos.Where(c => c.collider.tag == Constants.PLAYER_TAG);

        foreach (RaycastHit rHit in playersHitted)
        {
            rHit.collider.GetComponent<Health>().TakeDamage(50);
        }
        yield return new WaitForSeconds(0.5f);
        source.GetComponent<BossController>().isCasting = false;
        RpcUnDrawRay();
        StartCoroutine(this.ProcessCoolDown());
        isCasting = false;
    }
    [ClientRpc]
    public void RpcDrawBossEnergyRay()
    {
        bossRay.gameObject.SetActive(true);
        bossRay.SetPosition(0, start.transform.position);
        bossRay.SetPosition(1, mirror.transform.position);
    }

    [ClientRpc]
    public void RpcDrawMirrorEnergyRay(Vector3 pos)
    {
        mirrorRay.gameObject.SetActive(true);
        mirrorRay.SetPosition(0, mirror.transform.position);
        mirrorRay.SetPosition(1, pos);
    }

    [ClientRpc]
    public void RpcUnDrawRay()
    {
        bossRay.gameObject.SetActive(false);
        mirrorRay.gameObject.SetActive(false);
    }

    public override bool HasRessource()
    {
        return true;
    }
}
