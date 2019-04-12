using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TotemRay : Skill {

    public float castTime = 0.5f;
    public float totemActiveTime = 6f;
    public GameObject totemPrefab;

    public List<GameObject> totems = new List<GameObject>();

    public override IEnumerator Cast(GameObject currentTarget = null)
    {
        if(!isServer)
        {
            yield return null;
        }
        Vector3 pos = currentTarget.transform.position;

        yield return new WaitForSeconds(castTime);

        GameObject go = Instantiate(totemPrefab, pos, totemPrefab.transform.rotation);
        totems.Add(go);

        StartCoroutine(ProcessCoolDown());
       
        foreach (GameObject totem in totems)
        {
            RpcCastTotems(totem, true);
        }

        yield return new WaitForSeconds(totemActiveTime);
        
        foreach (GameObject totem in totems)
        {
            RpcCastTotems(totem, false);
        }
    }
    
    [ClientRpc]
    public void RpcCastTotems (GameObject totem, bool state)
    {
        totem.GetComponent<Totem>().SetStateCristals(state);
    }
    

    public override bool HasRessource()
    {
        return true;
    }
}
