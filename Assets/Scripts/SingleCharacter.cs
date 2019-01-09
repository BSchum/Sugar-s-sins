using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacter : MonoBehaviour {
    public GameObject prefab;

    public void ChooseCharacter()
    {
        CharaterManager.choosedCharacter = prefab;
        Debug.Log("J'ai choisi : " + CharaterManager.choosedCharacter);
    } 
}
