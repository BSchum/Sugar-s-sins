using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerBox : MonoBehaviour {

    public List<ObjectToSpawn> objectToSpawns;

    public void SetObjects(ObjectSpawnerController osc)
    {
        foreach (ObjectToSpawn obj in objectToSpawns)
        {
            obj.osc = osc;
        }
    }
}