using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponHolder : NetworkBehaviour {
    [SerializeField]
    GameObject weaponPrefab;

    [SerializeField]
    Transform weaponHolder;
    public GameObject currentWeapon { get; private set; }
    // Use this for initialization
    void Start () {
        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
