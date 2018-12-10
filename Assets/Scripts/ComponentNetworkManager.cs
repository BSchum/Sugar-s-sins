using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ComponentNetworkManager : MonoBehaviour
{
    Camera cam;
    // Use this for initialization
    void Start()
    {
        Camera cam = GetComponentInChildren<Camera>();
        AudioListener audio = GetComponentInChildren<AudioListener>();
        NetworkIdentity netidentity = GetComponent<NetworkIdentity>();
        PlayerMove movement = GetComponent<PlayerMove>();
        PlayerAttack attack = GetComponent<PlayerAttack>();

        if (!netidentity.isLocalPlayer)
        {
            cam.enabled = false;
            audio.enabled = false;
            movement.enabled = false;
            //attack.enabled = false;
        }
        else
        {
            GameObject.Find("LobbyCam").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
