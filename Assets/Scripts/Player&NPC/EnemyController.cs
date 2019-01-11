using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour {

    Dictionary<GameObject, float> sources = new Dictionary<GameObject, float>();
    GameObject currentTarget;

    #region Unity's methods
    private void Start()
    {

    }
    private void Update()
    {
        this.GetComponent<Stats>().ResetBonusStats();
        var sortedSources = sources.OrderByDescending(c => c.Value);
        currentTarget = sortedSources.FirstOrDefault().Key;
        GameObject go = GameObject.Find("ThreathDebug");
        //go.GetComponent<Text>().text = ToString();
    }
    #endregion
    #region ThreatSystem
    private void AddSource(GameObject source)
    {
        sources.Add(source, 0);
    }

    public void AddThreatFor(GameObject source, float threat)
    {
        if (!sources.ContainsKey(source))
            AddSource(source);
        sources[source] += threat;
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

