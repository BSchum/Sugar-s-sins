using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour {

    protected Dictionary<GameObject, float> sources = new Dictionary<GameObject, float>();
    protected GameObject currentTarget;

    protected Stats stats;
    protected Motor motor;
    protected bool canMove;

    public Slider slider; 
    #region Unity's methods
    protected void Start()
    {
        stats = GetComponent<Stats>();
        Slider hpbar = GetComponentInChildren<Slider>();
        if (hpbar != null)
        {
            Debug.Log(stats.GetHealth() * 100 / stats.GetMaxHealth());
            hpbar.maxValue = 100;
            hpbar.value = stats.GetHealth() * 100 / stats.GetMaxHealth();
        }
    }
    protected void Update()
    {
        this.GetComponent<Stats>().ResetBonusStats();
        var sortedSources = sources.OrderByDescending(c => c.Value);
        currentTarget = sortedSources.FirstOrDefault().Key;
        GameObject go = GameObject.Find("ThreathDebug");
        //go.GetComponent<Text>().text = ToString();

        if (!canMove)
        {
            GoToHightestThreat();
        }
        if(slider != null)
        {
            slider.maxValue = 1000;
            slider.value = stats.GetHealth();
        }

        
    }
    #endregion
    #region ThreatSystem
    public void AddThreatFor(GameObject source, float threat)
    {
        if (!sources.ContainsKey(source))
            sources.Add(source, 0);
        sources[source] += threat;
    }

    public void GoToHightestThreat()
    {
        if (currentTarget)
        {
            if(isServer)
                RpcGoToHightestThreat(currentTarget);
        }
    }

    [ClientRpc]
    public void RpcGoToHightestThreat(GameObject target)
    {
        
        transform.LookAt(target.transform.position);
        if((transform.position - target.transform.position).magnitude > 5)
            transform.Translate(Vector3.forward * this.stats.GetCurrentSpeed() * Time.deltaTime);
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

