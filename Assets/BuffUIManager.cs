using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUIManager : MonoBehaviour {

    public static BuffUIManager instance;
    public GameObject SingleBuffPrefab;
    private void Start()
    {
        instance = this;
    }
    public void AddBuff(BuffForUI buff)
    {
        GameObject singleB = Instantiate(SingleBuffPrefab, this.transform);
        singleB.GetComponent<SingleBuff>().buff = buff;
    }
}
