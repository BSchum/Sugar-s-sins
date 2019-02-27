using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeltableSkillManager : NetworkBehaviour {
    
    public MeltableSkill[] meltableSkills;

    public static MeltableSkillManager instance;

    public static GameObject skillOne = null;
    public static GameObject skillTwo = null;

    private void Start()
    {
        instance = this;
    }

    public static void GetSkillMelting(GameObject skill)
    {
        skillTwo = skillOne == null ? null : skill;
        skillOne = skillOne == null ? skill : skillOne;

        if(skillTwo != null)
        {
            MeltSkills();
        }
    }

    public static void MeltSkills()
    {
        foreach(MeltableSkill meltableSkill in instance.meltableSkills)
        {
            if((skillOne || skillTwo) == meltableSkill.skillTwo && (skillOne || skillTwo) == meltableSkill.skillTwo)
            {
                meltableSkill.Merge(skillOne, skillTwo);
            }
        }
    } 
}
