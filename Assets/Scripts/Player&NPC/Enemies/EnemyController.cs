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
    public bool canMove;

    public Slider slider;
    [SerializeField]
    protected Skill[] skills;

    #region Unity's methods
    protected void Start()
    {
        stats = GetComponent<Stats>();
        stats.ComputeFinalStats();
        Slider hpbar = GetComponentInChildren<Slider>();
        if (hpbar != null)
        {
            Debug.Log("Une hp bar de type qualitative");
            Debug.Log(stats.GetHealth());
            hpbar.maxValue = 100;
            hpbar.value = stats.GetHealth() * 100 / stats.GetMaxHealth();
        }
    }
    protected void Update()
    {
        stats.ComputeFinalStats();
        this.GetComponent<Stats>().ResetBonusStats();
        var sortedSources = sources.OrderByDescending(c => c.Value);
        currentTarget = sortedSources.FirstOrDefault().Key;
        //GameObject go = GameObject.Find("ThreathDebug");
        //go.GetComponent<Text>().text = ToString();

        if (canMove)
        {
            GoToHightestThreat();
        }
        if(slider != null)
        {
            slider.maxValue = stats.GetMaxHealth();
            slider.value = stats.GetHealth();
            slider.GetComponentInChildren<Text>().text = stats.GetHealth() + " / "+ stats.GetMaxHealth();
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
            Vector3 lookAtpos = new Vector3(currentTarget.transform.position.x,this.transform.position.y, currentTarget.transform.position.z);
            if (canMove)
                transform.LookAt(lookAtpos);
            if ((transform.position -lookAtpos).magnitude > 5)
            {
                transform.Translate(Vector3.forward * this.stats.GetCurrentSpeed() * Time.deltaTime);
                GetComponent<BaseAnimation>().Walk();
            }
            else
            {
                GetComponent<BaseAnimation>().Stay();

            }
        }
    }

    [ClientRpc]
    public void RpcGoToHightestThreat(GameObject target)
    {
        if(canMove)
            transform.LookAt(target.transform.position);
        if((transform.position - target.transform.position).magnitude > 5)
        {
            transform.Translate(Vector3.forward * this.stats.GetCurrentSpeed() * Time.deltaTime);
            GetComponent<BaseAnimation>().Walk();
        }
        else
        {
            GetComponent<BaseAnimation>().Stay();

        }
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

