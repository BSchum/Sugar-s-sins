using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SingleCharacter : MonoBehaviour {
    public GameObject prefab;

    public void ChooseCharacter()
    {
        CharaterManager.choosedCharacter = prefab;
        Debug.Log("J'ai choisi : " + CharaterManager.choosedCharacter);
    } 
   
}
