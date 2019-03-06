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

    
    public Slider playerHealthBar;
    public Slider playerRessourceBar;

    public Slider bossHealthBar;

    public Stats localPlayerStats;

    #region Unity's method
    private void Start()
    {
        instance = this;
    }
    public void AddBuff(BuffForUI buff)
    {
        GameObject singleB = Instantiate(SingleBuffPrefab, BuffsParent);
        singleB.GetComponent<SingleBuff>().buff = buff;
    }
    #endregion
    #region UI Slider Update system
    internal void Subscribe(Stats stats)
    {
        Debug.Log("Je subscribe");
        HealthChanged cb = UpdatePlayerHealthBar;
        stats.Subscribe(cb);
    }

    public void BossHPSubscribe(Stats stats)
    {
        Debug.Log("le boss subscribe");
        HealthChanged cb = UpdateBossHealthBar;
        stats.Subscribe(cb);
    }


    public void UpdatePlayerHealthBar(float value, float maxValue)
    {
        playerHealthBar.GetComponentInChildren<TextMeshProUGUI>().text = value + " / " + maxValue;
        playerHealthBar.maxValue = maxValue;
        playerHealthBar.value = value;
    }

    public void UpdatePlayerResourceBar(float value, float maxValue)
    {
        playerRessourceBar.GetComponentInChildren<TextMeshProUGUI>().text = value + " / " + maxValue;
        playerRessourceBar.maxValue = maxValue;
        playerRessourceBar.value = value;
    }

    public void UpdateBossHealthBar(float value, float maxValue)
    {
        bossHealthBar.GetComponentInChildren<TextMeshProUGUI>().text = value + " / " + maxValue;
        bossHealthBar.maxValue = maxValue;
        bossHealthBar.value = value;
    }
    #endregion
    #region UI Skills system
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
    #endregion

}
