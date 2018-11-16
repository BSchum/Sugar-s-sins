using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Stats))]
public class TankAttacks : PlayerAttack {

    
    [SerializeField]
    int gelatinStack = 10;

    void GelatinBehaviour()
    {

    }
    public void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }



}
