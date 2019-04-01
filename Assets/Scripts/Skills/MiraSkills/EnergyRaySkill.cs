using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class EnergyRaySkill : Skill {
    public GameObject[] mirrors;
    private GameObject mirror;
    public LineRenderer bossRay;
    //public LineRenderer mirrorRay;
    private GameObject mirrorRay;
    public GameObject start;
    public GameObject rayCastStart;
    public override IEnumerator Cast(GameObject target = null)
    {
        source.GetComponent<BossController>().resource -= cost;
        source.GetComponent<BossController>().isCasting = true;
        isCasting = true;
        //select a mirror
        mirror = ChooseMirror();
        source.GetComponent<BossController>().canMove = false;

        //make boss shoot at mirror
        source.transform.LookAt(mirror.transform);
        bossRay.gameObject.SetActive(true);
        RpcDrawBossEnergyRay();
        //display the first ray
        Vector3 direction = mirror.transform.position - rayCastStart.transform.position;
        Ray ray = new Ray(rayCastStart.transform.position, direction);
        RaycastHit[] infos = Physics.RaycastAll(ray, 1000000);
        //check if some players are hitted
        IEnumerable<RaycastHit> playersHitted = infos.Where( c => c.collider.tag == Constants.PLAYER_TAG);
        foreach(RaycastHit rHit in playersHitted)
        {
            rHit.collider.GetComponent<Health>().TakeDamage(30);
        }


        //Wait until the player can avoid
        yield return new WaitForSeconds(2);

        /* Old raycast
        //target highest threat
        Vector3 pos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        mirrorRay.gameObject.SetActive(true);

        RpcDrawMirrorEnergyRay(pos);
        direction = pos - mirror.transform.position;
        ray = new Ray(mirror.transform.position, direction);
        infos = Physics.RaycastAll(ray, 1000000);
        //
        playersHitted = infos.Where(c => c.collider.tag == Constants.PLAYER_TAG);
        foreach (RaycastHit rHit in playersHitted)
        {
            rHit.collider.GetComponent<Health>().TakeDamage(50);
        }*/

        //New kamehameha beam
        mirrorRay = mirror.transform.parent.transform.Find("Kamehameha").gameObject;
        mirrorRay.SetActive(true);
        mirrorRay.GetComponentInChildren<ParticleSystem>().Play();
        

        source.GetComponent<BossController>().isCasting = false;
        RpcUnDrawRay();
        isCasting = false;
        source.GetComponent<BossController>().canMove = true;
        yield return new WaitForSeconds(3f);
        StartCoroutine(this.ProcessCoolDown());
        mirrorRay.SetActive(false);

    }

    private GameObject ChooseMirror()
    {
        return mirrors[UnityEngine.Random.Range(0, mirrors.Length)];
    }

    [ClientRpc]
    public void RpcDrawBossEnergyRay()
    {
        bossRay.gameObject.SetActive(true);
        bossRay.SetPosition(0, start.transform.position);
        bossRay.SetPosition(1, mirror.transform.position);
    }

    /*[ClientRpc]
    public void RpcDrawMirrorEnergyRay(Vector3 pos)
    {
        mirrorRay.gameObject.SetActive(true);
        mirrorRay.SetPosition(0, mirror.transform.position);
        mirrorRay.SetPosition(1, pos);
    }*/

    [ClientRpc]
    public void RpcUnDrawRay()
    {
        bossRay.gameObject.SetActive(false);
        mirrorRay.gameObject.SetActive(false);
    }

    public override bool HasRessource()
    {
        return GetComponent<BossController>().CurrentRessourceValue > cost;
    }
}
