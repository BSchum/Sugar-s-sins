using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour {

    public GameObject[] cristals;

    public void SetStateCristals (bool state)
    {
        foreach(GameObject cristal in cristals)
        {
            cristal.SetActive(state);
        }
    }
}
