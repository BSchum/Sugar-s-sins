﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleBuff : MonoBehaviour {

    public BuffForUI buff;
	// Update is called once per frame
	void Update () {
        if(buff.cooldown != 0) {
            float cd = buff.cooldown + buff.lastApply - Time.time;

            if (Time.time > buff.lastApply + buff.cooldown)
            {
                Destroy(this.gameObject);
            }
            GetComponentInChildren<Text>().text = cd.ToString("F1") + " s";
        }

        GetComponentInChildren<Image>().sprite = buff.sprite;



    
    }
}

public class BuffForUI
{
    public float lastApply;
    public Sprite sprite;
    public float cooldown;

    public BuffForUI(float lastApply, Sprite sprite, float cooldown)
    {
        this.lastApply = lastApply;
        this.sprite = sprite;
        this.cooldown = cooldown;
    }
}

