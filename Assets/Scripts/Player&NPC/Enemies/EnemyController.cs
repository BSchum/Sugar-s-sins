using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyController : NetworkBehaviour {

    protected Dictionary<GameObject, float> sources = new Dictionary<GameObject, float>();
    [SyncVar]
    protected GameObject currentTarget;

    protected Stats stats;
    protected Motor motor;
    public bool canMove;
    public bool isCasting = false;

    public Slider slider;
    [SerializeField]
    protected Skill[] skills;

    public Skill autoAttack;
    public float autoAttackRange;

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
        autoAttack.source = this.gameObject;


    }
    protected void Update()
    {
        stats.ComputeFinalStats();
        this.GetComponent<Stats>().ResetBonusStats();
        var sortedSources = sources.OrderByDescending(c => c.Value);
        currentTarget = sortedSources.FirstOrDefault().Key;
        GameObject go = GameObject.Find("ThreathDebug");
        go.GetComponent<Text>().text = ToString();

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
        if (!isCasting && autoAttack != null && autoAttack.CanCast() && autoAttack.HasRessource() && !autoAttack.isOnCooldown && currentTarget != null && (currentTarget.transform.position - this.transform.position).magnitude < autoAttackRange + 2)
        {
            StartCoroutine(autoAttack.Cast(currentTarget));
            if (GetComponent<SimpleEnemyAnimations>() != null)
                GetComponent<SimpleEnemyAnimations>().Attack();
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
            if ((transform.position -lookAtpos).magnitude > autoAttackRange)
            {
                transform.Translate(Vector3.forward * this.stats.GetCurrentSpeed() * Time.deltaTime);
                if (GetComponent<BaseAnimation>() != null)
                {
                    GetComponent<BaseAnimation>().Walk();
                }
            }
            else
            {
                if (GetComponent<BaseAnimation>() != null && !isCasting)
                {
                    GetComponent<BaseAnimation>().Stay();
                }
            }
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

