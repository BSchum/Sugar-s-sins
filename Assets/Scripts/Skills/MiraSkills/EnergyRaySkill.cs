using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EnergyRaySkill : Skill {
    public Transform mirrorsParent;
    private Transform mirror;

    public LineRenderer mirrorRay;
    public LineRenderer energyRay;

    public Transform start;

    public float damage;
    public float range;

    public float firstCastDuration = 4f;
    public float secondeCastDuration = 1.5f;
    public float energyLaserDuration = 0.5f;

    public override IEnumerator Cast(GameObject target)
    {
        source.GetComponent<BossController>().isCasting = true;

        //Nearest mirror
        mirror = null;
        float distance = Mathf.Infinity;
        foreach(Transform m in mirrorsParent.GetComponentInChildren<Transform>())
        {
            float d = (m.position - transform.position).magnitude;
            if (d < distance)
            {
                distance = d;
                mirror = m;
            }
        }

        source.transform.LookAt(mirror.transform);
        isCasting = true;
        
        RpcDrawMirrorRay(start.position, mirror.position);

        //Load laser from mirror
        yield return new WaitForSeconds(firstCastDuration);

        RpcUndrawMirrorRay();

        source.transform.LookAt(target.transform);

        Vector3 oldTargetPos = target.transform.position;

        //Aim target
        yield return new WaitForSeconds(secondeCastDuration);

        //Cast laser
        RpcDrawEnergyRay(start.position, oldTargetPos + source.transform.forward * range);

        Ray ray = new Ray(source.transform.position, source.transform.forward);
        RaycastHit[] hits;

        float castTime = energyLaserDuration;
        while (castTime > 0)
        {
            castTime -= 1 * Time.deltaTime;

            yield return null;
            
            hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if(hit.transform.tag == Constants.PLAYER_TAG)
                {
                    Health health = hit.transform.GetComponent<Health>();
                    if (health != null)
                        health.TakeDamage(damage);
                }
            }
        }

        yield return new WaitForSeconds(energyLaserDuration);

        RpcUndrawEnergyRay();

        source.GetComponent<BossController>().isCasting = false;
        
        StartCoroutine(this.ProcessCoolDown());
        isCasting = false;
    }

    [ClientRpc]
    void RpcDrawMirrorRay(Vector3 startPos, Vector3 mirrorPos)
    {
        mirrorRay.gameObject.SetActive(true);
        mirrorRay.SetPosition(0, startPos);
        mirrorRay.SetPosition(1, mirrorPos);
    }

    [ClientRpc]
    void RpcDrawEnergyRay(Vector3 startPos, Vector3 impactPos)
    {
        energyRay.gameObject.SetActive(true);
        energyRay.SetPosition(0, startPos);
        energyRay.SetPosition(1, impactPos);
    }

    [ClientRpc]
    void RpcUndrawMirrorRay()
    {
        mirrorRay.gameObject.SetActive(false);
    }

    [ClientRpc]
    void RpcUndrawEnergyRay()
    {
        energyRay.gameObject.SetActive(false);
    }

    public override float GetRange()
    {
        return range;
    }

    public override bool HasRessource()
    {
        return true;
    }
}
