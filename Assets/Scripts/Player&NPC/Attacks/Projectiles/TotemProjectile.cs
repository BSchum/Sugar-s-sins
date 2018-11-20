using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemProjectile : SkillProjectile {

    public GameObject source;
    [SerializeField]
    int gelatinStacksAmount;
    [SerializeField]
    float gelatinStacksRate;
    // Use this for initialization
    void Start () {
        DieAfterLifeTime();
        StartCoroutine(GiveGelatinStack());
    }

    private void Update()
    {

    }

    IEnumerator GiveGelatinStack()
    {
        while (true)
        {
            source.GetComponent<TankAttacks>().AddGelatinStack(gelatinStacksAmount);
            yield return new WaitForSeconds(gelatinStacksRate);
        }
    }
}
