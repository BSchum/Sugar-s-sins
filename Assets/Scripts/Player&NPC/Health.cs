using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Stats))]
public class Health : NetworkBehaviour {
    Stats stats;
    Canvas canvas;
    GameObject floatingDmgPrefab;
    public void Start()
    {
        stats = GetComponent<Stats>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        floatingDmgPrefab = Resources.Load<GameObject>("Prefabs/FloatingDamageCanvas");

    }

    public void TakeDamage(float amount)
    {
        Debug.Log("Jai pris " + amount);
        GameObject instance = Instantiate(floatingDmgPrefab);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        instance.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString("0.00");
        if (amount < 0) {
            instance.GetComponentInChildren<TextMeshProUGUI>().material.color = Color.white;
            instance.GetComponentInChildren<TextMeshProUGUI>().faceColor = Color.green;
        }
        if (!isServer)
            return;
        RpcTakeDamage(amount);
    }

    [ClientRpc]
    public void RpcTakeDamage(float amount)
    {
        //Debug.Log("Je prend -" + amount + "de degats");
        this.stats.TakeDamage(-amount * (1 - (this.stats.damageReductionInPercent / 100)));
        if (this.stats.GetHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
