using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class MirrorRaySkill : Skill {

    public GameObject[] mirrors;
    private GameObject mirror;
    private GameObject mirrorRay;

    public override IEnumerator Cast(GameObject target = null)
    {
        bool isNearMirror = false;

        source.GetComponent<BossController>().resource -= cost;
        source.GetComponent<BossController>().isCasting = true;
        isCasting = true;

        mirror = ChooseMirror();
        
        source.transform.LookAt(mirror.transform.position);
        source.GetComponent<BossController>().canMove = false;
        do
        {
            if ((transform.position - mirror.transform.position).magnitude > 2)
            {
                transform.Translate(Vector3.forward * (GetComponent<Stats>().GetCurrentSpeed() * 1.2f) * Time.deltaTime);
            } else
            {
                mirrorRay = mirror.transform.parent.transform.Find("Kamehameha").gameObject;
                SetLayerRecursively(source, 9);
                //setAnimation
                yield return new WaitForSeconds(1);
                mirrorRay.SetActive(true);
                mirrorRay.GetComponentInChildren<ParticleSystem>().Play();
                isNearMirror = true;
            }
            yield return new WaitForEndOfFrame();
        } while (isNearMirror != true);
        
        yield return new WaitForSeconds(5);
        mirrorRay.SetActive(false);
        source.GetComponent<BossController>().canMove = true;
        source.GetComponent<BossController>().isCasting = false;
        SetLayerRecursively(source, 0);

        isCasting = false;
        StartCoroutine(this.ProcessCoolDown());
        yield return true;
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
            return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform) {
            if (null == child)
                continue;

            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private GameObject ChooseMirror()
    {
        return mirrors[UnityEngine.Random.Range(0, mirrors.Length)];
    }


    public override bool HasRessource()
    {
        return source.GetComponent<BossController>().CurrentRessourceValue > cost;
    }
}
