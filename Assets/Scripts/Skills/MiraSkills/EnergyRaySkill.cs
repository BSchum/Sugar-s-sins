using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EnergyRaySkill : Skill {
    public GameObject[] mirrors;
    private GameObject mirror;
    public GameObject bossRay;
    //public LineRenderer mirrorRay;
    private GameObject mirrorRay;
    public GameObject start;
    public GameObject rayCastStart;
    public override IEnumerator Cast(GameObject target = null)
    {
        StartCoroutine(this.ProcessCoolDown());
        source.GetComponent<BossController>().resource -= cost;
        source.GetComponent<BossController>().isCasting = true;
        isCasting = true;

        //select a mirror
        mirror = ChooseMirror();
        source.GetComponent<BossController>().canMove = false;

        //Je regarde le mirroir
        Vector3 lookAtpos = new Vector3(mirror.transform.position.x, this.transform.position.y, mirror.transform.position.z);
        source.transform.LookAt(lookAtpos);

        bossRay.SetActive(true);
        bossRay.GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(2f);
        mirrorRay = mirror.transform.parent.transform.Find("Kamehameha").gameObject;
        mirrorRay.gameObject.SetActive(true);
        mirrorRay.GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(2f);
        bossRay.gameObject.SetActive(false);
        mirrorRay.gameObject.SetActive(false);
        source.GetComponent<BossController>().isCasting = false;
        isCasting = false;
        source.GetComponent<BossController>().canMove = true;

    }

    private GameObject ChooseMirror()
    {
        return mirrors[UnityEngine.Random.Range(0, mirrors.Length)];
    }

    [ClientRpc]
    public void RpcDrawBossEnergyRay()
    {
        bossRay.gameObject.SetActive(true);
    }

    //[ClientRpc]
    //public void RpcDrawMirrorEnergyRay()
    //{
    //    //New kamehameha beam

    //}

    //[ClientRpc]
    //public void RpcUnDrawRay()
    //{
    //    bossRay.gameObject.SetActive(false);
    //    //mirrorRay.gameObject.SetActive(false);
    //}

    public override bool HasRessource()
    {
        return GetComponent<BossController>().CurrentRessourceValue > cost;
    }
}
