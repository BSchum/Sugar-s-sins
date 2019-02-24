using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour {

    protected Dictionary<GameObject, float> sources = new Dictionary<GameObject, float>();
    GameObject currentTarget;

    protected Stats stats;
    protected Motor motor;
    #region Unity's methods
    protected void Start()
    {
        stats = GetComponent<Stats>();

    }
    protected void Update()
    {
        this.GetComponent<Stats>().ResetBonusStats();
        var sortedSources = sources.OrderByDescending(c => c.Value);
        currentTarget = sortedSources.FirstOrDefault().Key;
        GameObject go = GameObject.Find("ThreathDebug");
        go.GetComponent<Text>().text = ToString();

        motor = new Motor(this.gameObject);
    }
    #endregion
    #region ThreatSystem
    public void AddThreatFor(GameObject source, float threat)
    {
        if (!sources.ContainsKey(source))
            sources.Add(source, 0);
        sources[source] += threat;

        Debug.Log(sources[source]);
        
    }

    public void LookHightestThreat()
    {
        var sortedSources = sources.OrderByDescending(c => c.Value);
        currentTarget = sortedSources.FirstOrDefault().Key;
        Debug.Log(this.gameObject.name + "||" + currentTarget);
        if (currentTarget)
        {
            Debug.Log("Je me tourne");
            if(isServer)
                RpcLookHightestThreat(currentTarget);
        }
    }

    [ClientRpc]
    public void RpcLookHightestThreat(GameObject target)
    {
        Debug.Log("Je me tourne");
        this.transform.LookAt(target.transform);
    }
    #endregion


    public override string ToString()
    {
        string text = "";
        foreach (KeyValuePair<GameObject, float> element in sources)
        {
            text += "\n"+element.Key.name + "\nThreat number" + element.Value;
        }
        return text;
    }
}

