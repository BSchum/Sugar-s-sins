using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleSkill: MonoBehaviour {

    public Skill mainSkill;
    public Skill afterUseSkill;
    [SerializeField]
    Image artwork;
    [SerializeField]
    Image cooldownMask;
    [SerializeField]
    TextMeshProUGUI cooldown;
	// Update is called once per frame
	void Update () {

        if (mainSkill.internalCD > 0 && afterUseSkill != null)
        {
            artwork.sprite = afterUseSkill.artwork;
            cooldown.SetText(afterUseSkill.internalCD.ToString("F1"));
            cooldownMask.fillAmount = Helpers.ComputeRatio(afterUseSkill.cooldown, 1, afterUseSkill.internalCD);
        }
        else
        {
            artwork.sprite = mainSkill.artwork;
            cooldown.text = mainSkill.internalCD.ToString("F1");
            cooldownMask.fillAmount = Helpers.ComputeRatio(mainSkill.cooldown, 1, mainSkill.internalCD);
        }
    }
}