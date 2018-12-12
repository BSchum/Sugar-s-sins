using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealPack : ObjectToSpawn {

    public GameObject healSphere;
    public float disableTime = 3f;
    
    public void DisableHealSphere ()
    {
        //Cast SetState => protected
        //Lui dit que son event contient une destruction d'objet
        SetState(Destroy);
        //cast une fonction depuis le joueur
        //grace au script ObjectSpawnerController présent sur le joueur
        osc.SpawnObject(this);
        StartCoroutine(Wait());
    }

    IEnumerator Wait ()
    {
        yield return new WaitForSeconds(disableTime);
        //Pareille qu'au dessus, change l'état puis cast depuis le joueur
        SetState(Spawn);
        osc.SpawnObject(this);
    }

    public override void Spawn()
    {
        healSphere.SetActive(true);
    }

    public override void Destroy()
    {
        healSphere.SetActive(false);
    }
}
