using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject SingleBuffPrefab;
    public Transform BuffsParent;

    public GameObject SingleSkillPrefab;
    public Transform SkillsParent;

    public List<Skill> skills;
    List<GameObject> uiSkills;

    public Slider healthBar;
    public Slider ressourceBar;

    public Stats localPlayerStats;

    
    private void Start()
    {
        instance = this;
    }
    public void AddBuff(BuffForUI buff)
    {
        GameObject singleB = Instantiate(SingleBuffPrefab, BuffsParent);
        singleB.GetComponent<SingleBuff>().buff = buff;
    }

    internal void Subscribe(Stats stats)
    {
        Debug.Log("Je subscribe");
        HealthChanged cb = UpdateHealthBar;
        stats.Subscribe(cb);
    }


    public void UpdateHealthBar(float value, float maxValue)
    {
        healthBar.GetComponentInChildren<TextMeshProUGUI>().text = value + " / " + maxValue;
        healthBar.maxValue = maxValue;
        healthBar.value = value;
    }

    public void UpdateResourceBar(float value, float maxValue)
    {
        ressourceBar.GetComponentInChildren<TextMeshProUGUI>().text = value + " / " + maxValue;
        ressourceBar.maxValue = maxValue;
        ressourceBar.value = value;
    }
    public void AddSkills(List<Skill> skills)
    {
        //TODO Add all skill to UI
        this.skills = skills;
        uiSkills = new List<GameObject>();
        foreach (Skill skill in skills)
        {
            GameObject singleS = Instantiate(SingleSkillPrefab, SkillsParent);
            singleS.GetComponent<SingleSkill>().mainSkill = skill;
            uiSkills.Add(singleS);
        }
    }

    //If a skill has to replace another while he is in cooldown ( tank charge )
    public void AddSkillOverAnother(int skillIndex, Skill skill)
    {
        uiSkills[skillIndex].GetComponent<SingleSkill>().afterUseSkill = skill;
    }

    
}
